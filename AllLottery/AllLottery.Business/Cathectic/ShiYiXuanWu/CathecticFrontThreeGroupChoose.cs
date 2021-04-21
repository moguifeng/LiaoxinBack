using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Cathectic.ShiYiXuanWu
{
    /// <summary>
    /// 前三组选
    /// </summary>
    public class CathecticFrontThreeGroupChoose : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 3, 11) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 2));
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 3, ",");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            string[] datas = SplitByDatas(data);
            var intArray = Array.ConvertAll<string, int>(datas, s => int.Parse(s));
            List<int> lisData = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                lisData.Add(openDatas[i]);
            }
            int index = 0;
            for (int i = 0; i < lisData.Count; i++)
            {
                if (intArray.Contains(lisData[i]))
                {
                    index++;
                }
            }
            if (index == 3)
            {
                return 1;
            }
            return 0;
        }
    }
}