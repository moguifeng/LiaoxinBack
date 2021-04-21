namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticGroupFiveChoose30 : BaseCathectic
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

            //第一组为二重号
            res = (CheckChoiceOfMaxMinRange(arr[0], 2, 10) &&
           //第二组为单号
           CheckChoiceOfMaxMinRange(arr[1], 1, 10)) ? true : false;
            if (res)
            {
                foreach (var data in arr)
                {
                    if (!(CheckEachChoiceCount(data, 1)))
                    {
                        return false;
                    }
                }
            }
            return res;
        }

        public override string[] SplitDatas(string datas)
        {
            return TwoGroupCombinationByDimension(datas, 2).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {

            if (CheckRepeatDataCount(openDatas) != 2)
            {
                return 0;
            }

            data = data.Replace(",", string.Empty);
            //第一个二重号
            int firstTwoFoldNum = int.Parse(data[0].ToString());
            //第二个二重号
            int secondTwoFoldNum = int.Parse(data[1].ToString());

            //单号
            int singleFoldNum = int.Parse(data[2].ToString());

            string openDatasStr = string.Join(string.Empty, openDatas);

            int firstTwoFoldNumIndex = 0;
            for (int i = 0; i < openDatas.Length; i++)
            {
                if (openDatas[i] == firstTwoFoldNum)
                {
                    //匹配到第一个二重号,然后匹配第二个.
                    firstTwoFoldNumIndex++;
                }
            }

            int secondTwoFoldNumIndex = 0;

            if (firstTwoFoldNumIndex == 2)
            {
                for (int i = 0; i < openDatas.Length; i++)
                {

                    if (openDatas[i] == secondTwoFoldNum)
                    {
                        secondTwoFoldNumIndex++;
                    }
                }
            }
            if (secondTwoFoldNumIndex == 2)
            {
                for (int i = 0; i < openDatas.Length; i++)
                {

                    if (openDatas[i] == singleFoldNum)
                    {
                        return 1;
                    }
                }

            }
            return 0;
        }
    }
}