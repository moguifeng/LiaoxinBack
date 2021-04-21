using AllLottery.Model;
using System.Collections.Generic;
using System.Linq;
using Zzb;

namespace AllLottery.Business.Generate
{
    public abstract class BaseGenerate : BaseService
    {
        public abstract string RandomGenerate();

        private static Dictionary<int, BaseGenerate> _baseGenerates = new Dictionary<int, BaseGenerate>();

        protected LotteryType Type { get; set; }

        public static BaseGenerate CreateGenerate(LotteryClassifyType type)
        {
            switch (type)
            {
                case LotteryClassifyType.Xuan5:
                    return new ShiYiXuanWu();
                case LotteryClassifyType.Kuai3:
                    return new KuaiSan();
                case LotteryClassifyType.SaiChe:
                    return new PkShiSaiChe();
                case LotteryClassifyType.Ssc:
                    return new ShiShiCai();
                case LotteryClassifyType.PaiLie3D:
                    return new PaiLie3D();
                case LotteryClassifyType.LiuHeCai:
                    return new LiuHeCai();
            }
            return null;
        }

        private static BaseGenerate CreateGenerate(int id)
        {
            lock (_baseGenerates)
            {
                using (var context = LotteryContext.CreateContext())
                {
                    var lottery = (from t in context.LotteryTypes where t.LotteryTypeId == id select t).FirstOrDefault();
                    if (lottery == null)
                    {
                        throw new ZzbException("无法找到对应彩种");
                    }
                    if (!_baseGenerates.ContainsKey(id))
                    {
                        _baseGenerates.Add(id, CreateGenerate(lottery.LotteryClassify.Type));
                    }

                    _baseGenerates[id].Type = lottery;
                    return _baseGenerates[id];
                }
            }
        }

        public string Generate(int id, string number = null)
        {
            var g = CreateGenerate(id);
            //if (g.Type.IsKill)
            //{
            //    return g.KillGenerate(number);
            //}
            //else
            {
                return g.RandomGenerate();
            }
        }

        public static bool CheckData(int id, string data)
        {
            return CreateGenerate(id).CheckDatas(data);
        }

        public abstract bool CheckDatas(string data);

        public object CreatTrend(string datas)
        {
            List<int> list = new List<int>();
            var numbers = datas.Split(',');
            foreach (string number in numbers)
            {
                list.Add(int.Parse(number));
            }
            return CreatTrend(list.ToArray());
        }

        public abstract object CreatTrend(int[] numbers);
    }
}