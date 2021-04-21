namespace AllLottery.Business.Cathectic.PaiLie3D
{
    public class CathecticTwoBehindDuplex : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            var res = (CheckAllNumber(datas, 2) &&
                       CheckRepeatData(datas, 2));
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
            for (int i = 0; i < 2; i++)
            {
                if (openDataStr[i + 1] != data[i])
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}