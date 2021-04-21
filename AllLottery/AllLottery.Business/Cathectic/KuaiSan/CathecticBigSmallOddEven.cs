using System.Linq;

namespace AllLottery.Business.Cathectic.KuaiSan
{
    public class CathecticBigSmallOddEven : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            int sum = openDatas.Sum();
            switch (data)
            {
                case "大":
                    return (sum > 10 && sum <= 18) ? 1 : 0;
                case "小":
                    return (sum >= 3 && sum <= 10) ? 1 : 0;
                case "单":
                    return sum % 2 != 0 ? 1 : 0;
                case "双":
                    return sum % 2 == 0 ? 1 : 0;
                default:
                    return 0;

            }
        }
    }
}