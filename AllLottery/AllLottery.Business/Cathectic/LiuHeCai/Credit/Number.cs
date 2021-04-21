using System.Collections.Generic;
using AllLottery.Model;

namespace AllLottery.Business.Cathectic.LiuHeCai.Credit
{
    public class Number : BaseLiuHeCaiCredit
    {
        public Number(string helpKey) : base(helpKey)
        {
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (openDatas[Index] == int.Parse(data))
            {
                return 490000;
            }

            return 0;
        }

        public override string[] Values
        {
            get
            {
                List<string> list = new List<string>();
                for (int i = 1; i < 50; i++)
                {
                    list.Add(i.ToString());
                }
                return list.ToArray();
            }
        }

        public override string NewKey => "号码";

        public override decimal MaxBetMoney(string value)
        {
            return 10000;
        }
    }
}