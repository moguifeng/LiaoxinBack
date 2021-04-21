using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.Common;

namespace AllLottery.Business.Cathectic.ShiYiXuanWu
{
    /// <summary>
    /// 定位胆
    /// </summary>
    public class CathecticFixedBilePosition : BaseCathectic
    {
        public override string CreateRandomNumber()
        {
            StringBuilder sb = new StringBuilder();

            for (int j = 1; j <= 3; j++)
            {
                //创建list
                List<int> list = new List<int>();
                for (int i = 0; i < 12; i++)
                {
                    if (list.Count < 5)
                    {
                        if (RandomHelper.NextDouble() >= 0.5)
                        {
                            list.Add(i);
                        }
                    }
                }

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
                        nsb.Append(i.ToString().PadLeft(2, '0') + ",");
                    }
                }

                sb.Append(nsb.ToString().Trim(','));
            }

            return sb.ToString().Trim('_');
        }

        public override bool Valid(string datas)
        {
            //11,12_18,17_-
            var res =
                CheckRepeatData(datas, 3) ? true : false;
            if (res)
            {

                var datasGroup = GroupByDatas(datas);
                if (datasGroup[0] == datasGroup[1] && datasGroup[2] == "-" && datasGroup[1] == datasGroup[2])
                {
                    return false;
                }
                foreach (string data in datasGroup)
                {

                    if (data != "-" && !(CheckChoiceOfMaxMinRange(data, 1, 11) && CheckEachChoiceAtMaxCount(data, 2)))
                    {
                        return false;
                    }
                }
            }
            return res;
        }

        string[] SelfSplitDatas(string datas)
        {
            List<string> lisSplitDatas = new List<string>();
            var datasGroup = GroupByDatas(datas);

            string[] strFlag = new string[] { "first", "second", "third" };
            for (int i = 0; i < strFlag.Length; i++)
            {
                var firstData = datasGroup[i];
                if (CheckAllNumber(firstData))
                {
                    var firstDatas = SplitByDatas(firstData);
                    foreach (var data in firstDatas)
                    {
                        lisSplitDatas.Add(strFlag[i] + ":" + data);
                    }
                }

            }
            return lisSplitDatas.ToArray();
        }

        public override string[] SplitDatas(string datas)
        {
            return SelfSplitDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            string[] realData = SplitByDatas(data, ":");
            switch (realData[0])
            {
                case "first":
                    return (Int32.Parse(realData[1]) == openDatas[0]) ? 1 : 0;
                case "second":
                    return (Int32.Parse(realData[1]) == openDatas[1]) ? 1 : 0;
                case "third":
                    return (Int32.Parse(realData[1]) == openDatas[2]) ? 1 : 0;
                default:
                    return 0;
            }
        }
    }
}