namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticFourBehindDuplex : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            var res = (CheckAllNumber(datas, 4) &&
                       CheckRepeatData(datas, 4)) ? true : false;
            if (res)
            {
                var datasGroup = GroupByDatas(datas);
                foreach (string data in datasGroup)
                {
                    if (!(CheckChoiceOfMaxMinRange(data, 1, 10) && CheckEachChoiceCount(data, 1)))
                    {
                        return false;
                    }
                }
            }
            return res;
        }

        public override string[] SplitDatas(string datas)
        {
            return GetMartrixAllCombination(datas).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            data = string.Join(string.Empty, data);
            string openDataStr = string.Join(string.Empty, openDatas);
            for (int i = 1; i < 5; i++)
            {
                if (openDataStr[i] != data[i - 1])
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}