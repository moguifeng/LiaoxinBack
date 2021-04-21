using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.ZzbLog;

namespace AllLottery.Business.Report
{
    public abstract class BaseReport
    {
        protected BaseReport(int id)
        {
            Id = id;
        }

        protected int Id { get; }

        private static DateTime lastUpdateTime = DateTime.Now;

        protected static readonly Dictionary<int, List<int>> PlayerIdsCache = new Dictionary<int, List<int>>();

        protected static readonly Dictionary<int, int> ParentPlayerIdsCache = new Dictionary<int, int>();

        private static Dictionary<Type, Dictionary<int, ReportListModel>> DataCache = new Dictionary<Type, Dictionary<int, ReportListModel>>();

        private static Dictionary<Type, Dictionary<int, decimal>> PlayersValue = new Dictionary<Type, Dictionary<int, decimal>>();

        public virtual void Add(decimal value)
        {
            DoAdd(Id, value);
        }

        protected void DoAdd(int id, decimal value)
        {
            lock (PlayersValue)
            {
                if (!PlayersValue.ContainsKey(GetType()) || !PlayersValue[GetType()].ContainsKey(id))
                {
                    return;
                }

                PlayersValue[GetType()][id] += value;
            }
        }

        protected void TeamAdd(int id, decimal value)
        {
            DoAdd(id, value);
            if (ParentPlayerIdsCache.ContainsKey(id))
            {
                TeamAdd(ParentPlayerIdsCache[id], value);
            }
        }

        private ReportListModel CurrentList
        {
            get
            {
                lock (DataCache)
                {
                    if (DateTime.Now > lastUpdateTime)
                    {
                        lastUpdateTime = lastUpdateTime.AddHours(3);
                        DataCache = new Dictionary<Type, Dictionary<int, ReportListModel>>();
                        lock (PlayersValue)
                        {
                            PlayersValue = new Dictionary<Type, Dictionary<int, decimal>>();
                        }
                    }
                    if (!DataCache.ContainsKey(GetType()))
                    {
                        DataCache.Add(GetType(), new Dictionary<int, ReportListModel>());
                    }
                    if (!DataCache[GetType()].ContainsKey(Id))
                    {
                        var model = new ReportListModel(0, new DateTime(2017, 1, 1));
                        var report = GetNewReport();
                        if (report != null)
                        {
                            model.Next = new ReportListModel(report.Value, report.QueryTime);
                        }
                        DataCache[GetType()].Add(Id, model);
                    }
                    return DataCache[GetType()][Id];
                }
            }
        }

        private ReportCache GetNewReport()
        {
            using (var context = LotteryContext.CreateContext())
            {
                string type = GetType().FullName;
                return (from r in context.ReportCaches
                        where r.Type == type && r.PlayerId == Id
                        orderby r.QueryTime descending
                        select r).FirstOrDefault();
            }

        }

        public virtual void Clear(LotteryContext context)
        {
            lock (DataCache)
            {
                DataCache.Remove(GetType());
                string type = GetType().FullName;
                context.ReportCaches.RemoveRange(from r in context.ReportCaches
                                                 where r.Type == type
                                                 select r);
            }
        }


        public static List<int> GetTeamPlayerIdsWhitoutSelf(int id)
        {
            List<int> list;
            lock (PlayerIdsCache)
            {
                using (var context = LotteryContext.CreateContext())
                {
                    if (PlayerIdsCache.ContainsKey(id))
                    {
                        list = new List<int>(PlayerIdsCache[id].ToArray());
                    }
                    else
                    {
                        list = (from p in context.Players
                                from s in p.Players
                                where p.PlayerId == id
                                select s.PlayerId).ToList();
                        PlayerIdsCache.Add(id, new List<int>(list.ToArray()));
                        foreach (int guid in list)
                        {
                            if (!ParentPlayerIdsCache.ContainsKey(guid))
                            {
                                ParentPlayerIdsCache.Add(guid, id);
                            }
                        }
                    }
                }

            }
            List<int> temp = new List<int>();
            foreach (int guid in list)
            {
                temp.AddRange(GetTeamPlayerIdsWhitoutSelf(guid));
            }
            list.AddRange(temp);
            return list;
        }

        public static void RemovePlayerCacheById(int id)
        {
            lock (PlayerIdsCache)
            {
                if (PlayerIdsCache.ContainsKey(id))
                {
                    PlayerIdsCache.Remove(id);
                }
            }
        }

        protected virtual List<int> GetTeamPlayerIds(int id)
        {
            return GetTeamPlayerIdsWhitoutSelf(id);
        }

        public decimal GetReportData()
        {
            return GetReportData(new DateTime(2019, 1, 1), DateTime.Now);
        }

        public decimal GetEndReportData(DateTime end)
        {
            return GetReportData(new DateTime(2019, 1, 1), end);
        }

        public decimal GetReportData(DateTime begin, DateTime end)
        {
            if (begin > end)
            {
                return 0;
            }
            lock (CurrentList)
            {
                return -GetCacheData(begin) + GetCacheData(end);
            }
        }

        private decimal GetSqlTodayData()
        {
            using (var context = LotteryContext.CreateContext())
            {
                var list = GetTeamPlayerIds(Id);
                var sql = GetTodayData(list, context);
                if (sql.Any())
                {
                    return sql.Sum();
                }
                return 0;
            }
        }

        protected abstract IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context);

        private decimal GetCacheData(DateTime time)
        {
            var last = GetLastList(time);
            if (last == null)
            {
                return 0;
            }

            if (last.Time == time)
            {
                return last.Value;
            }

            //时间超过最后容忍时间
            if (time > DateTime.Now.Date)
            {
                return GetCacheData(DateTime.Today) + GetSqlTodayData();
            }

            var report = GetRepoerCache(time);

            ReportListModel newList;
            if (report == null)
            {
                var value = GetSqlData(last.Time, time);
                SetReportCache(time, last.Value + value);
                newList = new ReportListModel(last.Value + value, time);
            }
            else
            {
                newList = new ReportListModel(report.Value, time);
            }
            newList.Next = last.Next;
            last.Next = newList;
            return newList.Value;
        }

        private ReportCache GetRepoerCache(DateTime time)
        {
            if (time > DateTime.Now.Date || time.TimeOfDay != new TimeSpan(0))
            {
                return null;
            }
            using (var context = LotteryContext.CreateContext())
            {
                string type = GetType().FullName;
                return (from r in context.ReportCaches
                        where r.PlayerId == Id && r.QueryTime == time && r.Type == type
                        select r).FirstOrDefault();
            }
        }

        private void SetReportCache(DateTime time, decimal value)
        {
            if (time > DateTime.Now.Date || time.TimeOfDay != new TimeSpan(0))
            {
                return;
            }

            try
            {
                using (var context = LotteryContext.CreateContext())
                {
                    context.ReportCaches.Add(new ReportCache(Id, time, value, GetType().FullName));
                    context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                LogHelper.Error("无法添加缓存数据", e);
            }
        }

        private decimal GetSqlData(DateTime begin, DateTime end)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var list = GetTeamPlayerIds(Id);
                var sql = Sql(list, begin, end, context);
                if (sql.Any())
                {
                    return sql.Sum();
                }
                return 0;
            }
        }

        private ReportListModel GetLastList(DateTime time)
        {
            if (time <= new DateTime(2017, 1, 1))
            {
                return null;
            }

            ReportListModel temp = CurrentList;
            while (temp.Next != null && temp.Next.Time <= time)
            {
                temp = temp.Next;
            }

            return temp;
        }



        protected abstract IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context);
    }

    public class ReportListModel
    {
        public ReportListModel(decimal value, DateTime time)
        {
            Value = value;
            Time = time;
        }

        public decimal Value { get; set; }

        public DateTime Time { get; set; }

        public ReportListModel Next { get; set; }
    }
}