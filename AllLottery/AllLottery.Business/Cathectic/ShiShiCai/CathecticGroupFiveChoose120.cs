namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticGroupFiveChoose120 : BaseCathectic
    {
        public override int Cathectic(string datas)
        {
            return GetCombinationByDimension(datas, 5).Count;
        }
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 5, 10) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 1)) ? true : false;
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 5);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            data = data.Replace(",", string.Empty);
            var cnt = CheckRepeatDataCount(openDatas);
            if (cnt != 0)
            {
                return 0;
            }

            int fiveIndex = 0;
            for (int i = 0; i < openDatas.Length; i++)
            {
                if (data.Contains(openDatas[i].ToString()))
                {
                    fiveIndex++;
                }
            }
            if (fiveIndex == 5)
            {
                return 1;
            }
            return 0;


        }
    }
}