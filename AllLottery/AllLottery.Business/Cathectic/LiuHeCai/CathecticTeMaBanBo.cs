using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Cathectic.LiuHeCai
{
    public class CathecticTeMaBanBo : BaseCathecticLiuHeCai
    {
        private readonly string[] _valid = { "红大", "红小", "红单", "红双", "红合单", "红合双", "绿大", "绿小", "绿单", "绿双", "绿合单", "绿合双", "蓝大", "蓝小", "蓝单", "蓝双", "蓝合单", "蓝合双" };

        public override bool Valid(string data)
        {
            var datas = data.Split(',');
            List<string> list = new List<string>();
            foreach (string s in datas)
            {
                if (list.Contains(s) || !_valid.Contains(s))
                {
                    return false;
                }
                list.Add(s);
            }
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return datas.Split(',');
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            int number = openDatas[6];
            switch (data)
            {
                case "红大":
                    if (GetColorNumber("红").Contains(number) && number >= 25 && number < 49)
                    {
                        return 360;
                    }
                    break;
                case "红小":
                    if (GetColorNumber("红").Contains(number) && number <= 24)
                    {
                        return 252;
                    }
                    break;
                case "红单":
                    if (GetColorNumber("红").Contains(number) && number % 2 == 1 && number < 49)
                    {
                        return 315;
                    }
                    break;
                case "红双":
                    if (GetColorNumber("红").Contains(number) && number % 2 == 0 && number < 49)
                    {
                        return 280;
                    }
                    break;
                case "红合单":
                    if (GetColorNumber("红").Contains(number) && (number / 10 + number % 10) % 2 == 1 && number < 49)
                    {
                        return 280;
                    }
                    break;
                case "红合双":
                    if (GetColorNumber("红").Contains(number) && (number / 10 + number % 10) % 2 == 0 && number < 49)
                    {
                        return 315;
                    }
                    break;
                case "绿大":
                    if (GetColorNumber("绿").Contains(number) && number >= 25 && number < 49)
                    {
                        return 315;
                    }
                    break;
                case "绿小":
                    if (GetColorNumber("绿").Contains(number) && number <= 24)
                    {
                        return 360;
                    }
                    break;
                case "绿单":
                    if (GetColorNumber("绿").Contains(number) && number % 2 == 1 && number < 49)
                    {
                        return 315;
                    }
                    break;
                case "绿双":
                    if (GetColorNumber("绿").Contains(number) && number % 2 == 0 && number < 49)
                    {
                        return 360;
                    }
                    break;
                case "绿合单":
                    if (GetColorNumber("绿").Contains(number) && (number / 10 + number % 10) % 2 == 1 && number < 49)
                    {
                        return 360;
                    }
                    break;
                case "绿合双":
                    if (GetColorNumber("绿").Contains(number) && (number / 10 + number % 10) % 2 == 0 && number < 49)
                    {
                        return 315;
                    }
                    break;
                case "蓝大":
                    if (GetColorNumber("蓝").Contains(number) && number >= 25 && number < 49)
                    {
                        return 280;
                    }
                    break;
                case "蓝小":
                    if (GetColorNumber("蓝").Contains(number) && number <= 24)
                    {
                        return 360;
                    }
                    break;
                case "蓝单":
                    if (GetColorNumber("蓝").Contains(number) && number % 2 == 1 && number < 49)
                    {
                        return 315;
                    }
                    break;
                case "蓝双":
                    if (GetColorNumber("蓝").Contains(number) && number % 2 == 0 && number < 49)
                    {
                        return 315;
                    }
                    break;
                case "蓝合单":
                    if (GetColorNumber("蓝").Contains(number) && (number / 10 + number % 10) % 2 == 1 && number < 49)
                    {
                        return 315;
                    }
                    break;
                case "蓝合双":
                    if (GetColorNumber("蓝").Contains(number) && (number / 10 + number % 10) % 2 == 0 && number < 49)
                    {
                        return 315;
                    }
                    break;
                default:
                    break;
            }
            return 0;
        }
    }
}