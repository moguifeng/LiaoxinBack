namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticGroupFourChoose4 : BaseCathectic
    {
        public override int Cathectic(string datas)
        {
            return TwoGroupCombination(datas).Count;
        }
        public override bool Valid(string datas)
        {
            var res = (
                CheckAllNumber(datas, 2) &&
                CheckRepeatData(datas, 2)) ? true : false;
            var arr = GroupByDatas(datas);

            if (res)
            {
                foreach (var data in arr)
                {
                    if (!(CheckEachChoiceCount(data, 1) && CheckChoiceOfMaxMinRange(data, 1, 10)))
                    {
                        return false;
                    }
                }
            }
            return res;

        }

        public override string[] SplitDatas(string datas)
        {
            return TwoGroupCombination(datas).ToArray();
        }

        //选择1个三重号码和1个单号号码组成一注，在开奖号码后四位中，所选单号号码与开奖号码相同，且所选三重号码出现了3次，即为中奖。
        //投注方案：3,1  开奖号码：5,3,3,3,1  即中组选4
        public override long IsWinEach(string data, int[] openDatas)
        {
            data = data.Replace(",", string.Empty);
            int threeFoldNum = int.Parse(data[0].ToString());
            int singleNum = int.Parse(data[1].ToString());

            int threeIndex = 0;

            for (int i = 1; i < openDatas.Length; i++)
            {
                if (openDatas[i] == threeFoldNum)
                {
                    threeIndex++;
                }
            }
            if (threeIndex == 3)
            {
                for (int i = 1; i < openDatas.Length; i++)
                {
                    if (singleNum == openDatas[i])
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }
    }
}