namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticGroupFiveChoose10 : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            var res = false;
            res = (
                CheckAllNumber(datas, 2) &&
                CheckRepeatData(datas, 2)) ? true : false;
            if (!res)
            {
                return false;
            }
            var arr = GroupByDatas(datas);


            foreach (var data in arr)
            {
                if (!(CheckEachChoiceCount(data, 1) && CheckChoiceOfMaxMinRange(data, 1, 10)))
                {
                    return false;
                }
            }
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return TwoGroupCombination(datas).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            //第一个约定为三重号,第二个为二重号
            data = data.Replace(",", string.Empty);
            int threeFoldNumber = int.Parse(data[0].ToString());

            int twoFoldNumber = int.Parse(data[1].ToString());

            int threeFoldNumberIndex = 0;
            foreach (var num in openDatas)
            {
                if (threeFoldNumber == num)
                {
                    threeFoldNumberIndex++;
                }
            }
            int twoFoldNumberIndex = 0;
            if (threeFoldNumberIndex == 3)
            {
                for (int i = 0; i < openDatas.Length; i++)
                {
                    if (twoFoldNumber == openDatas[i])
                    {
                        twoFoldNumberIndex++;
                        break;
                    }
                }
            }
            if (twoFoldNumberIndex == 2)
            {
                return 1;
            }
            return 0;
        }
    }
}