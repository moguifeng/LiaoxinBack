namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticGroupFourChoose24 : BaseCathectic
    {
        public override int Cathectic(string datas)
        {
            return GetCombinationByDimension(datas, 4).Count;
        }
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 4, 10) &&
                    CheckRepeatData(datas) &&
                    CheckEachChoiceCount(datas, 1)) ? true : false;
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 4);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            data = data.Replace(",", string.Empty);
            string openDataStr = string.Join(string.Empty, openDatas);
            int indexFour = 0;
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 1; j < openDataStr.Length; j++)
                {
                    if (data[i] == openDataStr[j])
                    {
                        indexFour++;
                        break;
                    }
                }
            }
            if (indexFour == 4)
            {
                return 1;
            }
            return 0;
        }
    }
}