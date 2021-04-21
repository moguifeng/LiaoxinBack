namespace AllLottery.Business.Cathectic.KuaiSan
{
    public class CathecticTwoTogether : BaseCathectic
    {

        public override bool Valid(string datas)
        {
            var res = (
                CheckAllNumber(datas) &&
                CheckChoiceOfMaxMinRange(datas, 1, 6) &&
                CheckEachChoiceCount(datas, 2)
            ) ? true : false;
            if (res)
            {
                string[] splitDatas = this.SplitByDatas(datas);
                foreach (var data in splitDatas)
                {
                    if (data[0] != data[1])
                    {
                        return false;
                    }
                }
            }
            return res;
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            int[] numbers = new int[2];
            numbers[0] = int.Parse(data[0].ToString());
            numbers[1] = int.Parse(data[1].ToString());

            if ((openDatas[0] == numbers[0] && openDatas[1] == numbers[1]) ||
                openDatas[1] == numbers[0] && openDatas[2] == numbers[1])
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}