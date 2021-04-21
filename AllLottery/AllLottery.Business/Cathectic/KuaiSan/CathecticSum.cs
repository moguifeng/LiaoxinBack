using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.Common;

namespace AllLottery.Business.Cathectic.KuaiSan
{
    public class CathecticSum : BaseCathectic
    {
        private string[] _keys = new string[] { "大", "小", "单", "双" };

        public override string CreateRandomNumber()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 3; i < 19; i++)
            {
                if (sb.Length >= 16)
                {
                    break;
                }
                if (RandomHelper.NextDouble() > 0.5)
                {
                    sb.Append(i + ",");
                }
            }
            if (string.IsNullOrEmpty(sb.ToString()))
            {
                return CreateRandomNumber();
            }
            return sb.ToString().Trim(',');
        }

        public override bool Valid(string datas)
        {
            try
            {
                List<string> list = new List<string>();
                var numbers = SplitDatas(datas);
                if (numbers.Length > 22)
                {
                    return false;
                }
                foreach (string number in numbers)
                {
                    if (list.Contains(number))
                    {
                        return false;
                    }
                    list.Add(number);
                    if (_keys.Contains(number))
                    {
                        continue;
                    }
                    if (!int.TryParse(number, out var i))
                    {
                        return false;
                    }
                    if (i < 3 || i > 18)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            var sum = openDatas.Sum();
            if (_keys.Contains(data))
            {
                switch (data)
                {
                    case "小":
                        if (sum >= 3 && sum <= 10)
                        {
                            return 20000;
                        }
                        break;
                    case "大":
                        if (sum >= 11 && sum <= 18)
                        {
                            return 20000;
                        }
                        break;
                    case "单":
                        if (sum % 2 == 1)
                        {
                            return 20000;
                        }
                        break;
                    case "双":
                        if (sum % 2 == 0)
                        {
                            return 20000;
                        }
                        break;
                    default:
                        break;
                }

                return 0;
            }
            if (int.Parse(data) == sum)
            {
                switch (sum)
                {
                    case 3:
                        return 2160000;
                    case 4:
                        return 720000;
                    case 5:
                        return 360000;
                    case 6:
                        return 216000;
                    case 7:
                        return 144000;
                    case 8:
                        return 102857;
                    case 9:
                        return 86400;
                    case 10:
                        return 80000;
                    case 11:
                        return 80000;
                    case 12:
                        return 86400;
                    case 13:
                        return 102857;
                    case 14:
                        return 144000;
                    case 15:
                        return 216000;
                    case 16:
                        return 360000;
                    case 17:
                        return 720000;
                    case 18:
                        return 2160000;
                    default:
                        return 0;
                }
            }
            return 0;
        }
    }
}