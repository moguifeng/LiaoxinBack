using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.PkShi.Credit
{
    public class FirstSecondGroup : BaseCredit
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            var datas = data.Split('-');
            int one = int.Parse(datas[0]);
            int two = int.Parse(datas[1]);
            if ((openDatas[0] == one && openDatas[1] == two) || (openDatas[0] == two && openDatas[1] == one))
            {
                return 1;
            }

            return 0;
        }

        private static object _that = new object();

        private static string[] _values;

        public override string[] Values
        {
            get
            {
                lock (_that)
                {
                    if (_values == null)
                    {
                        List<string> list = new List<string>();
                        for (int i = 1; i < 10; i++)
                        {
                            for (int j = i + 1; j < 11; j++)
                            {
                                list.Add(i.ToString().PadLeft(2, '0') + "-" + j.ToString().PadLeft(2, '0'));
                            }
                        }

                        _values = list.ToArray();
                    }
                    return _values;
                }
            }
        }

        public override string Key => "冠亚组合";

        public override decimal MaxBetMoney(string value)
        {
            return 3000;
        }
    }
}