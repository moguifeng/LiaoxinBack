using System;

namespace AllLottery.Business.Cathectic.ShiYiXuanWu
{
    /// <summary>
    /// 前二组选
    /// </summary>
    public class CathecticFrontTwoGroupChoose : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            var res = (CheckChoiceOfMaxMinRange(datas, 2, 11) &&
                       CheckRepeatData(datas) && CheckEachChoiceCount(datas, 2)) ? true : false;
            return res;
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 2, ",");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (openDatas[0] == openDatas[1])
            {
                return 0;
            }
            string[] datas = SplitByDatas(data);

            int index = 0;
            var arrInt = Array.ConvertAll<string, int>(datas, s => (Int32.Parse(s)));
            for (int i = 0; i < arrInt.Length; i++)
            {
                if (arrInt[i] == openDatas[0] || arrInt[i] == openDatas[1])
                {
                    index++;
                }
            }
            if (index == 2)
            {
                return 1;
            }
            return 0;
        }
    }
}