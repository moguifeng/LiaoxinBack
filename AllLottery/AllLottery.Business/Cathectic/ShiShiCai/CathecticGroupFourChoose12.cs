namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticGroupFourChoose12 : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            var res = (
            CheckAllNumber(datas, 2) &&
            CheckRepeatData(datas, 2));
            if (!res)
            {
                return false;
            }

            var arr = GroupByDatas(datas);

            //第二组为单号
            res = (CheckChoiceOfMaxMinRange(arr[0], 2, 10) &&
           //第一组为二重号
           CheckChoiceOfMaxMinRange(arr[1], 1, 10)) ? true : false;

            foreach (var data in arr)
            {
                if (!(CheckEachChoiceCount(data, 1)))
                {
                    return false;
                }
            }
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return TwoGroupCombinationByDimension(datas, 2, false).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            int[] newDatas = new int[4];
            for (int i = 1; i < openDatas.Length; i++)
            {
                newDatas[i - 1] = openDatas[i];
            }
            if (CheckRepeatDataCount(newDatas) != 1)
            {
                return 0;
            }
            data = data.Replace(",", string.Empty);
            //最后一位为二重号
            int twoFoldNum = int.Parse(data[data.Length - 1].ToString());
            //单号
            string singleNums = data.Substring(0, data.Length - 1);

            int twoIndex = 0;
            for (int i = 1; i < openDatas.Length; i++)
            {
                if (openDatas[i] == twoFoldNum)
                {
                    twoIndex++;
                }
            }
            int singleIndex = 0;
            if (twoIndex == 2)
            {
                for (int i = 1; i < openDatas.Length; i++)
                {
                    if (singleNums.Contains(openDatas[i].ToString()))
                    {
                        singleIndex++;
                    }
                }
            }
            if (singleIndex == 2)
            {
                return 1;
            }
            return 0;

        }
    }
}