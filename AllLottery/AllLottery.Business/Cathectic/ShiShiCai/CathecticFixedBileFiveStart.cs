using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.Common;

namespace AllLottery.Business.Cathectic.ShiShiCai
{
    /// <summary>
    /// 五星定位胆
    /// </summary>
    public class CathecticFixedBileFiveStart : BaseCathectic
    {

        public override int Cathectic(string datas)
        {
            return AnyAndFixTypeCathctic(datas);
        }

        /// <summary>
        /// 任选系列与定位胆系列的计算注数
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected int AnyAndFixTypeCathctic(string datas)
        {
            var lisSum = CathecticFixBiles(datas);
            lisSum = lisSum.FindAll(c => !c.Contains("-"));
            return lisSum.Count;
        }

        public override bool Valid(string datas)
        {
            return ValidFixBiles(datas, 5, 1, 10, 1);
        }

        public override string[] SplitDatas(string datas)
        {
            return CathecticFixBiles(datas).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            return CatheticFixBilesIsWin(data, openDatas) ? 1 : 0;
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

        public override string CreateRandomNumber()
        {
            StringBuilder sb = new StringBuilder();

            int index = RandomHelper.Next(1, 6);

            for (int j = 1; j <= 5; j++)
            {
                if (index == j)
                {
                    //创建list
                    List<int> list = CreateSscBetIntList(5);

                    StringBuilder nsb = new StringBuilder();
                    nsb.Append("_");
                    var eachNumer = list.ToList();
                    if (eachNumer.Count == 0)
                    {
                        nsb.Append("-");
                    }
                    else
                    {
                        foreach (int i in eachNumer)
                        {
                            nsb.Append(i % 10 + ",");
                        }
                    }

                    sb.Append(nsb.ToString().Trim(','));
                }
                else
                {
                    sb.Append("_-");
                }

            }

            return sb.ToString().Trim('_');
        }
    }
}