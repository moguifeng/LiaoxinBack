using System;
using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticAnyHit
{
    public abstract class BaseCathecticAnyHit : BaseCathectic
    {
        protected abstract int Number { get; }

        protected virtual int Split => Number;

        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, Number, 11) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 2));
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, Split);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            return HitSummary(openDatas, data, Number) ? 1 : 0;
        }

        protected bool HitSummary(int[] openDatas, string data, int dimension)
        {
            Array.Sort(openDatas);


            List<int> lisData = new List<int>();

            for (int i = 0; i < data.Length / 2; i++)
            {
                lisData.Add(Int32.Parse(data.Substring(i * 2, 2)));
                //0102030504

            }
            lisData.Sort();
            int currentDimension = 0;
            for (int i = 0; i < openDatas.Length; i++)
            {
                if (lisData.Contains(openDatas[i]))
                {
                    currentDimension++;
                }
            }
            if (currentDimension == dimension)
            {
                return true;
            }
            return false;
        }

    }
}