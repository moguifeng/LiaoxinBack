namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticGroupFiveChoose60 : BaseCathectic
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

            //第二组为单号
            res = (CheckChoiceOfMaxMinRange(arr[1], 3, 10) &&

           //第一组为二重号
           CheckChoiceOfMaxMinRange(arr[0], 1, 10)) ? true : false;
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
            return TwoGroupCombinationByDimension(datas, 3, false).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            string openDatasStr = string.Join(",", openDatas);

            int cnt = CheckRepeatDataCount(openDatas);
            if (cnt != 1)
            {
                return 0;
            }

            data = data.Replace(",", string.Empty);

            //二重号号码
            int twoFoldNum = int.Parse(data[data.Length - 1].ToString());

            //单号
            string singleFoldNum = data.Substring(0, data.Length - 1);

            int twoFoldNumberIndex = 0;

            for (int i = 0; i < openDatas.Length; i++)
            {
                if (openDatas[i] == twoFoldNum)
                {
                    twoFoldNumberIndex++;
                }
            }
            int singleIndex = 0;

            if (twoFoldNumberIndex == 2)
            {
                for (int i = 0; i < openDatas.Length; i++)
                {
                    if (singleFoldNum.Contains(openDatas[i].ToString()))
                    {
                        singleIndex++;
                    }
                }

                if (singleIndex == 3)
                {
                    return 1;
                }

            }
            return 0;
        }
    }
}