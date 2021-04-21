using System;
using System.Collections.Generic;
using Zzb.Common;

namespace AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSix
{
    public abstract class BaseCathecticGroupThreeSix : BaseCathectic
    {
        protected abstract int Number { get; }

        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 3, 10) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 1));
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 3);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            return WinThreeGroupSix(Number, openDatas, data) ? 1 : 0;
        }

        /// <summary>
        /// 组六
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="openDatas"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool WinThreeGroupSix(int startIndex, int[] openDatas, string data)
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

        public override string CreateRandomNumber()
        {
            return string.Join(",", CreateSscBetIntList(RandomHelper.Next(8, 10)));
        }
    }
}