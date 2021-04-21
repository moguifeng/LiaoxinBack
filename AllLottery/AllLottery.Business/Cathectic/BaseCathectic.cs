using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Zzb;
using Zzb.Common;

namespace AllLottery.Business.Cathectic
{
    public abstract class BaseCathectic
    {
        private static Dictionary<string, BaseCathectic> _cathecticDic = new Dictionary<string, BaseCathectic>();

        public virtual void CheckBetMoney(int playerId, int detailId, string issuseNo, decimal betMoney, string value, int betCount)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var detail = (from d in context.LotteryPlayDetails where d.LotteryPlayDetailId == detailId select d).FirstOrDefault();

                if (detail == null)
                {
                    throw new ZzbException($"玩法[{detailId }]不存在，请检查参数");
                }

                decimal existBetMoney = 0;
                decimal existBetCount = 0;
                var sql = from b in context.Bets
                          where b.PlayerId == playerId && b.LotteryPlayDetailId == detailId &&
                                b.LotteryIssuseNo == issuseNo && b.Status == BetStatusEnum.Wait
                          select b;
                if (sql.Any())
                {
                    existBetCount = sql.Sum(t => t.BetCount);
                    existBetMoney = sql.Sum(t => t.BetMoney);
                }
                if (existBetMoney + betMoney > detail.MaxBetMoney)
                {
                    throw new ZzbException($"[{detail.LotteryPlayType.LotteryType.Name}]的[{detail.LotteryPlayType.Name}-{detail.Name}]不能超过每期最大投注额[{detail.MaxBetMoney.ToDecimalString()}]");
                }
                if (detail.MaxBetCount != null && existBetCount + betCount > detail.MaxBetCount)
                {
                    throw new ZzbException($"[{detail.LotteryPlayType.LotteryType.Name}]的[{detail.LotteryPlayType.Name}-{detail.Name}]不能超过每期最大投注注数[{detail.MaxBetCount}]");
                }
            }
        }

        public static BaseCathectic CreateCathectic(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ZzbException("该玩法还没定义算法");
            }
            lock (_cathecticDic)
            {
                if (!_cathecticDic.ContainsKey(className))
                {
                    Assembly assembly = typeof(BaseCathectic).Assembly;
                    Type typeInfo = assembly.GetType(className);
                    if (typeInfo == null)
                    {
                        throw new ZzbException($"反射出错：不存在类！[{className}]");
                    }

                    if (!(Activator.CreateInstance(typeInfo) is BaseCathectic cathectic))
                    {
                        throw new ZzbException($"反射出错！未找到匹配算法[{className}]");
                    }
                    _cathecticDic.Add(className, cathectic);
                }
                return _cathecticDic[className];
            }
        }

        public virtual string CreateRandomNumber()
        {
            throw new ZzbException("未实现该方法");
        }

        public int CalculateBetCount(string datas)
        {
            try
            {
                if (Valid(datas))
                {
                    return Cathectic(datas);
                }
                else
                {
                    throw new ZzbException($"算法[{this.GetType().Name}]出错，参数校验失败，参数datas：[{datas}]");
                }
            }
            catch (Exception e)
            {
                throw new ZzbException($"算法[{this.GetType().Name}]出错，参数datas：[{datas}]", e);
            }
        }

        public virtual int Cathectic(string datas)
        {
            return SplitDatas(datas).Length;
        }

        public abstract bool Valid(string datas);

        public abstract string[] SplitDatas(string datas);

        public abstract long IsWinEach(string data, int[] openDatas);

        protected Dictionary<string, long> WinCache = new Dictionary<string, long>();

        public virtual long IsWin(string datas, int[] openDatas)
        {
            if (Valid(datas))
            {
                long count = 0;
                foreach (string data in SplitDatas(datas))
                {
                    lock (WinCache)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (int i in openDatas)
                        {
                            sb.Append(i + ",");
                        }

                        string cache = data + "-" + sb.ToString().Trim(',');
                        if (!WinCache.ContainsKey(cache))
                        {
                            WinCache.Add(cache, IsWinEach(data, openDatas));
                        }
                        if (WinCache[cache] > 0)
                        {
                            count += WinCache[cache];
                        }
                    }
                }

                return count;
            }
            else
            {
                throw new ZzbException($"算法[{this.GetType().Name}]出错，参数校验失败，参数datas：[{datas}]");
            }
        }

        public virtual decimal CalculateWinMoney(Bet bet, decimal maxRate, int[] openDatas)
        {
            return bet.BetMode.Money * bet.WinBetCount * bet.Times * bet.CalculateOdds(maxRate);
            //return modeMoney * winCount * times * odds;
        }

        public static int[] ConvertoNumbers(string data)
        {
            var numberStr = data.Split(',');
            if (numberStr.Length == 0)
            {
                throw new ZzbException(
                    "转换失败");
            }

            int[] numbers = new int[numberStr.Length];
            for (int i = 0; i < numberStr.Length; i++)
            {
                int n;
                if (!int.TryParse(numberStr[i], out n))
                {
                    throw new ZzbException(
                        "转换失败");
                }

                numbers[i] = n;
            }

            return numbers;
        }

        #region 辅助算法

        /// <summary>
        /// datas分割后的count是否大于最大值或者count最小值
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="minRange"></param>
        /// <param name="maxRange"></param>
        /// <returns></returns>
        protected bool CheckChoiceOfMaxMinRange(string datas, int minRange, int maxRange, string splitStr = ",")
        {
            try
            {
                var arr = SplitByDatas(datas, splitStr).ToList();
                return (arr.Count >= minRange && arr.Count <= maxRange);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查每个单选要多少位合理.
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        protected bool CheckEachChoiceCount(string datas, int dimension, string splitStr = ",")
        {

            string[] arr = SplitByDatas(datas, splitStr);
            foreach (var data in arr)
            {
                if (data.Length != dimension)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查每个单选最多要多少位合理.
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        protected bool CheckEachChoiceAtMaxCount(string datas, int dimension, string splitStr = ",")
        {

            string[] arr = SplitByDatas(datas, splitStr);
            foreach (var data in arr)
            {
                if (data.Length > dimension)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 根据组数,判断数据是否全部都是数字
        /// </summary>
        /// <param name="datas">数据</param>
        /// <param name="groups">组数</param>
        /// <returns></returns>
        protected bool CheckAllNumber(string datas, int groups)
        {
            string[] datasGroups = GroupByDatas(datas);
            if (datasGroups.Length != groups)
            {
                return false;
            }

            foreach (var datasOfGroup in datasGroups)
            {
                var res = CheckAllNumber(datasOfGroup);
                if (!res)
                {
                    return false;
                }
            }
            return true;
        }

        protected bool CheckAllNumber(string datas, string splitStr = "_")
        {
            var arr = datas.Split(new string[] { splitStr }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var cArr in arr)
            {
                if (!int.TryParse(cArr, out _))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 分组
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected string[] GroupByDatas(string datas)
        {
            return datas.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 判断数据是否全部都是数字
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected bool CheckAllNumber(string datas)
        {
            var arr = SplitByDatas(datas);
            foreach (var cArr in arr)
            {
                if (!int.TryParse(cArr, out _))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 字符串分隔.
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected string[] SplitByDatas(string datas)
        {
            return datas.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }


        /// <summary>
        /// 检查数据是否重复.
        /// </summary>
        /// <param name="datas">检查的数据</param>
        /// <param name="group">组数</param>
        /// <returns></returns>
        protected bool CheckRepeatData(string datas, int group)
        {
            string[] datasGroups = GroupByDatas(datas);
            if (datasGroups.Length != group)
            {
                return false;
            }
            int sumCount = 0;

            foreach (var dataStr in datasGroups)
            {
                sumCount += PrivateCheckRepeatData(dataStr);
            }
            return sumCount == 0;
        }

        protected int PrivateCheckRepeatData(string datas, string splitStr = null)
        {
            string[] arr = null;
            if (splitStr != null)
            {
                arr = SplitByDatas(datas, splitStr);
            }
            else
            {
                arr = SplitByDatas(datas);
            }

            var duplicate = arr.GroupBy(i => i)
                .Where(g => g.Count() > 1)
                .Select(g => g.ElementAt(0));
            int cnt = duplicate.Count();
            return cnt;
        }

        /// <summary>
        /// 字符串分隔.
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected string[] SplitByDatas(string datas, string splitStr)
        {
            return datas.Split(new string[] { splitStr }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 矩阵
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected List<string> GetMartrixAllCombination(string datas)
        {
            try
            {
                string[] datasGroups = datas.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                //33_1,4,5,6

                string joinStr = string.Empty;
                List<List<string>> lis = new List<List<string>>();

                for (int i = 0; i < datasGroups.Length; i++)
                {
                    var tmpDataGroup = datasGroups[i];
                    if (tmpDataGroup.Length < 1)
                    {
                        throw new ZzbException("矩阵系列,注数被篡改.");
                    }
                    var tGroups = SplitByDatas(tmpDataGroup);

                    List<string> lisStr = new List<string>();
                    for (int j = 0; j < tGroups.Length; j++)
                    {
                        lisStr.Add(tGroups[j]);
                    }
                    lis.Add(lisStr);
                }
                return GetMatrixAllCombination(lis);
            }
            catch (Exception ex)
            {
                throw new ZzbException("内部算法错误", ex);
            }
        }

        /// <summary>
        /// 矩阵组合.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected List<string> GetMatrixAllCombination(List<List<string>> list)
        {
            var array = list.Aggregate((m, n) => m.SelectMany(t1 => n.Select(t2 => t1 + t2).ToList()).ToList()).ToList();
            return array;
        }

        /// <summary>
        /// 检查数据是否重复.
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected bool CheckRepeatData(string datas, string splitStr = null)
        {
            return PrivateCheckRepeatData(datas, splitStr) == 0;
        }

        public string[] GroupThreePermutation(string datas)
        {
            string[] arr = HandlerPermutationStringArray(datas, 2);
            List<string> lisArr = new List<string>();
            foreach (var arrData in arr)
            {
                var lisChar = arrData.ToList();
                lisChar.Sort();
                string strJoin = string.Empty;
                lisChar.ForEach(c =>
                {
                    strJoin += c.ToString();
                });
                if (!lisArr.Contains(strJoin))
                {
                    lisArr.Add(strJoin);
                }
            }
            return lisArr.ToArray();
        }

        /// <summary>
        /// 处理排列返回的数组.
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="dimension"></param>
        /// <returns>new string[]{12,23,45}</returns>
        protected string[] HandlerPermutationStringArray(string datas, int dimension, string splitStr = "")
        {
            List<string[]> lis = GetPermutationByDimension(datas, 2);
            List<string> lisRes = new List<string>();
            lis.ForEach(c =>
            {
                string[] tmp = c;
                string str = string.Join(splitStr, tmp);
                lisRes.Add(str);
            });
            return lisRes.ToArray();
        }

        /// <summary>
        /// 排列算法,dimension为粒度.例如粒度为3(1,2,3/1,3,2/2,1,3....)
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public List<string[]> GetPermutationByDimension(string datas, int dimension)
        {
            string[] datasNum = SplitByDatas(datas);
            if (datasNum.Length < dimension)
            {
                throw new ZzbException("注数被篡改");
            }
            return AlgorithmSumarry<string>.GetPermutation(datasNum, dimension);
        }

        public bool GroupThreeIsEachWin(int startIndex, string data, int[] openDatas)
        {

            int[] newDatas = new int[3];
            for (int i = startIndex; i < startIndex + 3; i++)
            {
                newDatas[i - startIndex] = openDatas[i];
            }

            if (newDatas[0] == newDatas[1] && newDatas[1] == newDatas[2] && newDatas[0] == newDatas[2])
            {
                return false;
            }

            int cnt = CheckRepeatDataCount(newDatas);
            if (cnt != 1)
            {
                return false;
            }

            Array.Sort(newDatas);
            int flag = 0;
            foreach (var num in newDatas)
            {
                if (data.Contains(num.ToString()))
                {
                    flag++;
                }
            }
            if (flag != 3)
            {
                return false;
            }
            return true;
        }

        public int CheckRepeatDataCount(int[] datas)
        {
            var duplicate = datas.GroupBy(i => i)
                .Where(g => g.Count() > 1)
                .Select(g => g.ElementAt(0));
            int cnt = duplicate.Count();
            return cnt;
        }

        /// <summary>
        /// 处理组合返回的数组.
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="dimension"></param>
        /// <returns>new string[]{12,23,45}</returns>
        protected string[] HandlerCombinationStringArray(string datas, int dimension, string splitStr = "")
        {
            List<string[]> lis = GetCombinationByDimension(datas, dimension);
            List<string> lisRes = new List<string>();
            lis.ForEach(c =>
            {
                string[] tmp = c;
                string str = string.Join(splitStr, tmp);
                lisRes.Add(str);
            });
            return lisRes.ToArray();
        }

        /// <summary>
        /// 组合算法.dimension为粒度.例如粒度为3(1,2,3....)
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        protected List<string[]> GetCombinationByDimension(string datas, int dimension)
        {
            try
            {
                string[] datasNum = SplitByDatas(datas);
                if (datasNum.Length < dimension)
                {
                    throw new ZzbException("注数被篡改");
                }
                return AlgorithmSumarry<string>.GetCombination(datasNum, dimension);

            }
            catch (Exception ex)
            {
                throw new ZzbException("内部算法错误", ex);
            }
        }

        /// <summary>
        /// *星单式
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="digitCapacity">位数,五星就是5,四星就是4</param>
        /// <returns></returns>
        protected bool AnyStartSingleValid(string datas, int digitCapacity)
        {
            //1,2,3,4,5_3,4,2,3,5_1,2,8,6,3
            string[] splitDatas = GroupByDatas(datas);
            bool res = false;
            foreach (var data in splitDatas)
            {
                if (SplitByDatas(data).Length != digitCapacity)
                {
                    return false;
                }
                res = (CheckEachChoiceCount(data, 1) && CheckAllNumber(data)) ? true : false;
                if (!res)
                {
                    return false;
                }
            }

            return res;
        }

        /// <summary>
        /// 两组数据进行组合.
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected List<string> TwoGroupCombination(string datas)
        {
            try
            {
                string[] datasGroups = datas.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                if (datasGroups.Length != 2)
                {
                    throw new ZzbException("注数被篡改.");
                }
                var datasNumOne = datasGroups[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var datasNumTwo = datasGroups[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                return AlgorithmSumarry<string>.GetTwoGroupCombination(datasNumOne, datasNumTwo);
            }
            catch (Exception ex)
            {
                throw new ZzbException("内部算法错误", ex);
            }
        }

        /// <summary>
        /// 三组数据进行组合
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected List<string> ThreeGroupCombination(string datas)
        {
            try
            {
                string[] datasGroups = datas.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                if (datasGroups.Length != 3)
                {
                    throw new ZzbException("注数被篡改.");
                }
                var datasNumOne = datasGroups[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var datasNumTwo = datasGroups[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var datasNumThree = datasGroups[2].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                return AlgorithmSumarry<string>.GetTreeGroupCombination(datasNumOne, datasNumTwo, datasNumThree);

            }
            catch (Exception ex)
            {
                throw new ZzbException("内部算法错误", ex);
            }
        }

        protected List<int> CreateSscBetIntList(int? count = null)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }
            int remove = RandomHelper.Next(11);
            if (count != null)
            {
                remove = 10 - count.Value;
            }
            for (int i = 0; i < remove; i++)
            {
                list.RemoveAt(RandomHelper.Next(list.Count));
            }

            return list;
        }

        protected bool ValidDaXiaoDanShuang(string datas, int group)
        {
            var res = false;
            string[] datasOfGroup = GroupByDatas(datas);
            foreach (var data in datasOfGroup)
            {
                res = (
                    CheckEachChoiceCount(data, 1) &&
                    CheckChoiceOfMaxMinRange(data, 1, 4)) ? true : false;
            }
            if (res)
            {
                return CheckRepeatData(datas, group);
            }
            return res;
        }

        /// <summary>
        /// 判断一个数字是否为大小单双.
        /// </summary>
        /// <returns></returns>
        protected string GetResultStr(string resouceData, int openData)
        {
            int remainder = openData % 2;
            string dataFlag = string.Empty;
            if (resouceData == "大" || resouceData == "小")
            {
                if (openData >= 5 && openData <= 9)
                {
                    dataFlag = "大";
                }

                else if (openData >= 0 && openData <= 4)
                {
                    dataFlag = "小";
                }
            }
            else if (resouceData == "单" || resouceData == "双")
            {
                if (remainder == 0)
                {
                    dataFlag = "双";
                }
                else
                {
                    dataFlag = "单";
                }
            }
            return dataFlag;
        }

        /// <summary>
        /// 计算定位胆是否合理.
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="groupCnt"></param>
        /// <param name="minRange"></param>
        /// <param name="maxRange"></param>
        /// <param name="eachNumberCount"></param>
        /// <returns></returns>
        protected bool ValidFixBiles(string datas, int groupCnt, int minRange, int maxRange, int eachNumberCount)
        {

            var groupsData = SplitByDatas(datas, "_");
            if (groupsData.Length != groupCnt)
            {
                return false;
            }
            var res = false;
            for (int i = 0; i < groupCnt; i++)
            {
                var groupData = groupsData[i];

                if (groupData == "-")
                {
                    continue;
                }
                res = (CheckAllNumber(groupData) &&
                       CheckChoiceOfMaxMinRange(groupData, minRange, maxRange) &&
                       CheckRepeatData(groupData) && CheckEachChoiceCount(groupData, eachNumberCount)) ? true : false;
                if (!res)
                {
                    return false;
                }

            }
            return res;

        }

        /// <summary>
        /// 计算定位胆所有注数的可能性.
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected List<string> CathecticFixBiles(string datas)
        {

            List<string> lisSplitDatas = new List<string>();
            var datasGroup = GroupByDatas(datas);
            for (int i = 0; i < datasGroup.Length; i++)
            {
                var dataGroup = datasGroup[i];
                if (dataGroup == "-")
                {
                    lisSplitDatas.Add(i + ":-");
                }
                else
                {
                    string[] realDatas = SplitByDatas(dataGroup);
                    foreach (var realData in realDatas)
                    {
                        lisSplitDatas.Add(i + ":" + realData);
                    }
                }
            }
            return lisSplitDatas;
        }

        protected bool CatheticFixBilesIsWin(string data, int[] openDatas)
        {
            string[] realData = SplitByDatas(data, ":");
            if (realData[1] != "-")
            {
                int index = Int32.Parse(realData[0]);
                return (int.Parse(realData[1]) == openDatas[index]) ? true : false;
            }
            return false;
        }

        /// <summary>
        /// 组六
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="openDatas"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected bool WinThreeGroupSix(int startIndex, int[] openDatas, string data)
        {
            string openDataStr = string.Join(string.Empty, openDatas).Substring(startIndex, 3);

            List<int> lisOpenData = new List<int>();
            foreach (var cStr in openDataStr)
            {
                lisOpenData.Add(Int32.Parse(cStr.ToString()));
            }
            lisOpenData.Sort();
            //同一格式  just in case
            data = string.Join(string.Empty, data);

            List<int> lisDatas = new List<int>();
            foreach (var cStr in data)
            {
                lisDatas.Add(Int32.Parse(cStr.ToString()));
            }
            lisDatas.Sort();
            for (int i = 0; i < lisDatas.Count; i++)
            {
                if (lisDatas[i] != lisOpenData[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 两组数据进行组合,假如firstDimensionIndex为true时那么第一组数据加上维度度,比如第一组的数据的维度为3(第一组:1,2,4,这三个数字与第二组进行组合)
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="dimension">维度,假如维度为3,那么"维度组"就必须要三个或者三个number以上</param>
        /// <param name="firstDimensionIndex">判断第一组是否为维度组</param>
        /// <returns></returns>
        protected List<string> TwoGroupCombinationByDimension(string datas, int dimension, bool firstDimensionIndex = true)
        {
            try
            {
                var groupDatas = datas.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                if (groupDatas.Length != 2)
                {
                    throw new Exception("注数被篡改.");
                }
                var datasDimensionGroup = groupDatas[firstDimensionIndex ? 0 : 1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var datasOneGroup = groupDatas[firstDimensionIndex ? 1 : 0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (datasDimensionGroup.Length < dimension)
                {
                    throw new Exception("注数被篡改.");
                }
                return AlgorithmSumarry<string>.GetTwoGroupCombinationByDimension(datasDimensionGroup, datasOneGroup, dimension);
            }
            catch (Exception ex)
            {
                throw new Exception("内部算法错误", ex);
            }

        }
        #endregion
    }

    public class AutoCreateParameter
    {
        public string BetNo { get; set; }

        public string ReflectClass { get; set; }

        public int Share { get; set; }
    }

    public class AlgorithmSumarry<T>
    {

        /// <summary>
        /// 交换两个变量
        /// </summary>
        /// <param name="a">变量1</param>
        /// <param name="b">变量2</param>
        public static void Swap(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// 递归算法求排列(私有成员)
        /// </summary>
        /// <param name="list">返回的列表</param>
        /// <param name="t">所求数组</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        private static void GetPermutation(ref List<T[]> list, T[] t, int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
            {
                if (list == null)
                {
                    list = new List<T[]>();
                }
                T[] temp = new T[t.Length];
                t.CopyTo(temp, 0);
                list.Add(temp);
            }
            else
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    Swap(ref t[startIndex], ref t[i]);
                    GetPermutation(ref list, t, startIndex + 1, endIndex);
                    Swap(ref t[startIndex], ref t[i]);
                }
            }
        }

        /// <summary>
        /// 求从起始标号到结束标号的排列，其余元素不变
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        /// <returns>从起始标号到结束标号排列的范型</returns>
        public static List<T[]> GetPermutation(T[] t, int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex > t.Length - 1)
            {
                return null;
            }
            List<T[]> list = new List<T[]>();
            GetPermutation(ref list, t, startIndex, endIndex);
            return list;
        }

        /// <summary>
        /// 返回数组所有元素的全排列
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <returns>全排列的范型</returns>
        public static List<T[]> GetPermutation(T[] t)
        {
            return GetPermutation(t, 0, t.Length - 1);
        }

        /// <summary>
        /// 求数组中n个元素的排列
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <param name="n">元素个数</param>
        /// <returns>数组中n个元素的排列</returns>
        public static List<T[]> GetPermutation(T[] t, int n)
        {
            if (n > t.Length)
            {
                return null;
            }
            List<T[]> list = new List<T[]>();
            List<T[]> c = GetCombination(t, n);
            for (int i = 0; i < c.Count; i++)
            {
                List<T[]> l = new List<T[]>();
                GetPermutation(ref l, c[i], 0, n - 1);
                list.AddRange(l);
            }
            return list;
        }

        /// <summary>
        /// 递归算法求数组的组合(私有成员)
        /// </summary>
        /// <param name="list">返回的范型</param>
        /// <param name="t">所求数组</param>
        /// <param name="n">辅助变量</param>
        /// <param name="m">辅助变量</param>
        /// <param name="b">辅助数组</param>
        /// <param name="M">辅助变量M</param>
        private static void GetCombination(ref List<T[]> list, T[] t, int n, int m, int[] b, int M)
        {
            for (int i = n; i >= m; i--)
            {
                b[m - 1] = i - 1;
                if (m > 1)
                {
                    GetCombination(ref list, t, i - 1, m - 1, b, M);
                }
                else
                {
                    if (list == null)
                    {
                        list = new List<T[]>();
                    }
                    T[] temp = new T[M];
                    for (int j = 0; j < b.Length; j++)
                    {
                        temp[j] = t[b[j]];
                    }
                    list.Add(temp);
                }
            }
        }

        /// <summary>
        /// 求数组中n个元素的组合
        /// </summary>
        /// <param name="t">所求数组</param>
        /// <param name="n">元素个数</param>
        /// <returns>数组中n个元素的组合的范型</returns>
        public static List<T[]> GetCombination(T[] t, int n)
        {
            if (t.Length < n)
            {
                return null;
            }
            int[] temp = new int[n];
            List<T[]> list = new List<T[]>();
            GetCombination(ref list, t, t.Length, n, temp, n);
            return list;
        }


        /// <summary>
        /// 普通组合
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> GetNormal(T[] t)
        {
            return t.ToList<T>();

        }

        /// <summary>
        /// 矩阵组合.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<string> GetMatrixAllCombination(List<List<string>> list)
        {
            var array = list.Aggregate((m, n) => m.SelectMany(t1 => n.Select(t2 => t1 + t2).ToList()).ToList()).ToList();
            return array;
        }


        public static List<string> GetTwoGroupCombination(string[] groupOne, string[] groupTwo)
        {
            List<string> lis = new List<string>();
            string tempGroupOne = string.Empty;
            string tempGroupTwo = string.Empty;
            for (int i = 0; i < groupOne.Length; i++)
            {
                tempGroupOne = groupOne[i];
                for (int j = 0; j < groupTwo.Length; j++)
                {
                    tempGroupTwo = groupTwo[j];
                    if (tempGroupOne.Equals(tempGroupTwo))
                    {
                        continue;
                    }
                    lis.Add(tempGroupOne + "," + tempGroupTwo);
                }
            }
            return lis;
        }


        public static List<string> GetTreeGroupCombination(string[] groupOne, string[] groupTwo, string[] groupThree)
        {
            List<string> lis = new List<string>();
            string tempGroupOne = string.Empty;
            string tempGroupTwo = string.Empty;
            string tempGroupThree = string.Empty;
            for (int i = 0; i < groupOne.Length; i++)
            {
                tempGroupOne = groupOne[i];
                for (int j = 0; j < groupTwo.Length; j++)
                {
                    tempGroupTwo = groupTwo[j];
                    if (tempGroupOne.Equals(tempGroupTwo))
                    {
                        continue;
                    }
                    for (int k = 0; k < groupThree.Length; k++)
                    {
                        tempGroupThree = groupThree[k];
                        if (tempGroupOne.Equals(tempGroupThree) || tempGroupTwo.Equals(tempGroupThree))
                        {
                            continue;
                        }
                        lis.Add(tempGroupOne + "," + tempGroupTwo + "," + tempGroupThree);
                    }
                }
            }
            return lis;
        }


        /// <summary>
        /// 根据粒度组合的数组与单纯的数组进行组合.
        /// </summary>
        /// <param name="DimensionGroupDistinctNumber">粒度组合数组</param>
        /// <param name="groupOneNumber">单纯的数组</param>
        /// <param name="dimension">粒度</param>
        /// <returns></returns>
        public static List<string> GetTwoGroupCombinationByDimension(string[] DimensionGroupDistinctNumber, string[] groupOneNumber, int dimension)
        {
            List<string> lis = new List<string>();

            List<string[]> lisCombination = AlgorithmSumarry<string>.GetCombination(DimensionGroupDistinctNumber, dimension);

            foreach (var strs in lisCombination)
            {
                bool isRepeat = false;
                foreach (var groupOne in groupOneNumber)
                {
                    for (int i = 0; i < strs.Length; i++)
                    {
                        isRepeat = false;
                        if (groupOne == strs[i])
                        {
                            isRepeat = true;
                            break;
                        }
                        string.Join(",", strs);
                    }
                    if (isRepeat == false)
                    {
                        lis.Add(string.Join(",", strs) + "," + groupOne);
                    }
                }
            }
            return lis;
        }


    }

}