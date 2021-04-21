namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticGroupFiveChoose20 : BaseCathectic
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
            res = (CheckChoiceOfMaxMinRange(arr[1], 2, 10) &&
           //第一组为三重号
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
            return TwoGroupCombinationByDimension(datas, 2, false).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            data = data.Replace(",", string.Empty);
            //三重号号码
            int threeFoldNum = int.Parse(data[data.Length - 1].ToString());

            //单号
            string singleFoldStr = data.Substring(0, data.Length - 1);

            int threeIndex = 0;
            for (int i = 0; i < openDatas.Length; i++)
            {

                if (openDatas[i] == threeFoldNum)
                {
                    threeIndex++;
                }

            }
            int singleIndex = 0;
            if (threeIndex == 3)
            {
                for (int i = 0; i < openDatas.Length; i++)
                {
                    if (singleFoldStr.Contains(openDatas[i].ToString()))
                    {
                        singleIndex++;
                    }

                }
                if (singleIndex == 2)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}