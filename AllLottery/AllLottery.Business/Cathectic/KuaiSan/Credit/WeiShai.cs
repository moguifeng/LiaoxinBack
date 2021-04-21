namespace AllLottery.Business.Cathectic.KuaiSan.Credit
{
    public class WeiShai : BaseKuaiSanCredit
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            if (data == "全骰")
            {
                if (openDatas[0] == openDatas[1] && openDatas[1] == openDatas[2])
                {
                    return 360000;
                }
            }
            else
            {
                int number = int.Parse(data);
                if (openDatas[0] == number && openDatas[1] == number && openDatas[2] == number)
                {
                    return 2160000;
                }
            }

            return 0;


        }

        public override string[] Values => new[] { "全骰", "1", "2", "3", "4", "5" };
        public override string Key => "围骰";

        public override decimal MaxBetMoney(string value)
        {
            return 100;
        }
    }
}