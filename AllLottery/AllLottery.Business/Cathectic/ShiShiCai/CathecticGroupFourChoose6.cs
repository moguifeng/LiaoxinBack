namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticGroupFourChoose6 : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 2, 10) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 1)) ? true : false;
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 2);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            int[] newOpens = new int[4];
            for (int i = 1; i < openDatas.Length; i++)
            {
                newOpens[i - 1] = openDatas[i];
            }
            if (CheckRepeatDataCount(newOpens) != 2)
            {
                return 0;
            }
            data = data.Replace(",", string.Empty);
            int firstTwoFoldNum = int.Parse(data[0].ToString());
            int secondTwoFoldNum = int.Parse(data[1].ToString());

            int twoIndex = 0;
            for (int i = 1; i < openDatas.Length; i++)
            {
                if (openDatas[i] == firstTwoFoldNum)
                {
                    twoIndex++;
                }
                if (openDatas[i] == secondTwoFoldNum)
                {
                    twoIndex++;
                }
            }
            if (twoIndex == 4)
            {
                return 1;
            }
            return 0;
        }
    }
}