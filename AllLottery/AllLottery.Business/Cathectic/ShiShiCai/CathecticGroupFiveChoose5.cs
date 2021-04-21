namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticGroupFiveChoose5 : BaseCathectic
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
            //第一个约定为四重号,第二个为单号
            data = data.Replace(",", string.Empty);
            int fourFoldNumber = int.Parse(data[0].ToString());
            int oneFoldNumber = int.Parse(data[1].ToString());

            int fourFoldFlagIndex = 0;
            for (int i = 0; i < openDatas.Length; i++)
            {
                if (openDatas[i] == fourFoldNumber)
                {
                    fourFoldFlagIndex++;
                }
            }

            if (fourFoldFlagIndex == 4)
            {
                for (int i = 0; i < openDatas.Length; i++)
                {
                    if (openDatas[i] == oneFoldNumber)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }
    }
}