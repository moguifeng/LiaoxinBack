using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.KuaiSan.Credit
{
    public class SanJun : BaseKuaiSanCredit
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            int number = int.Parse(data);
            if (openDatas[0] == number || openDatas[1] == number || openDatas[2] == number)
            {
                return 23736;
            }

            return 0;
        }

        public override string[] Values
        {
            get
            {
                List<string> values = new List<string>();
                for (int i = 1; i < 7; i++)
                {
                    values.Add(i.ToString());
                }

                return values.ToArray();
            }
        }

        public override string Key => "三军";

        public override decimal MaxBetMoney(string value)
        {
            return 20000;
        }
    }
}