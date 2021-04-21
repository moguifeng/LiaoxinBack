namespace AllLottery.Business.Cathectic.PkShi.Credit
{
    public abstract class BaseLongHuDou : BaseCredit
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            if (data == "龙" && openDatas[First] > openDatas[Second])
            {
                return 1;
            }

            if (data == "虎" && openDatas[First] < openDatas[Second])
            {
                return 1;
            }

            return 0;
        }

        public override string[] Values => new[] { "龙", "虎" };

        public override string Key => $"第{FirstStr}球VS第{SecondStr}球";

        public abstract string FirstStr { get; }

        public abstract string SecondStr { get; }

        public abstract int First { get; }

        public abstract int Second { get; }

        public override decimal MaxBetMoney(string value)
        {
            return 20000;
        }
    }
}