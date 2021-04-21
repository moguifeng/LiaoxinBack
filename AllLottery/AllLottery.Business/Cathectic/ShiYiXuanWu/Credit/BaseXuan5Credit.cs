using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.ShiYiXuanWu.Credit
{
    public abstract class BaseXuan5Credit : BaseCredit
    {
        public abstract int Index { get; }

        public override long IsWinEach(string data, int[] openDatas)
        {
            int number = int.Parse(data);
            if (openDatas[Index] == number)
            {
                return 110000;
            }
            return 0;
        }

        public override string[] Values
        {
            get
            {
                List<string> list = new List<string>();
                for (int i = 1; i < 12; i++)
                {
                    list.Add(i.ToString());
                }
                return list.ToArray();
            }
        }

        public override decimal MaxBetMoney(string value)
        {
            return 10000;
        }
    }
}