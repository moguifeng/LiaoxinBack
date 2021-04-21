using AllLottery.Business.Config;
using AllLottery.Model;
using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.ShiShiCai.XinYongPan
{
    public abstract class BaseSpecial : BaseIndexOpenData
    {
        public override string[] Values => new[] { "豹子", "顺子", "对子", "半顺", "杂六" };

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (data == WinningValue(openDatas, Index))
            {
                for (int i = 0; i < Values.Length; i++)
                {
                    if (data == Values[i])
                    {
                        if (i == 0)
                        {
                            if (BaseConfig.HasValue(SystemConfigEnum.IsBoYue))
                            {
                                return 800000;
                            }
                            return 1000000;
                        }

                        if (i == 1)
                        {
                            if (BaseConfig.HasValue(SystemConfigEnum.IsBoYue))
                            {
                                return 150000;
                            }
                            if (BaseConfig.HasValue(SystemConfigEnum.IsZiji))
                            {
                                return 185185;
                            }
                            return 166666;
                        }

                        if (i == 2)
                        {
                            return 37037;
                        }

                        if (i == 3)
                        {
                            return 27322;
                        }

                        if (i == 4)
                        {
                            return 33333;
                        }
                    }
                }
            }
            return 0;
        }

        public static string WinningValue(int[] openDatas, int index)
        {
            int one = openDatas[index];
            int two = openDatas[index + 1];
            int three = openDatas[index + 2];
            List<int> list = new List<int>() { one, two, three };
            list.Sort();

            if ((list[2] - list[1] == 1 && list[1] - list[0] == 1) || (list[0] == 0 && list[1] == 8 && list[2] == 9) || (list[0] == 0 && list[1] == 1 && list[2] == 2) || (list[0] == 0 && list[1] == 1 && list[2] == 9))
            {
                return "顺子";
            }
            if (one == two && two == three)
            {
                return "豹子";
            }
            if (one == two || two == three || one == three)
            {
                return "对子";
            }
            if (list[2] - list[1] == 1 || list[1] - list[0] == 1 || (list[0] == 0 && list[2] == 9))
            {
                return "半顺";
            }

            return "杂六";
        }

        public override decimal MaxBetMoney(string value)
        {
            if (value == "豹子")
            {
                return 1000;
            }

            if (value == "顺子")
            {
                return 5000;
            }

            return 20000;
        }
    }
}