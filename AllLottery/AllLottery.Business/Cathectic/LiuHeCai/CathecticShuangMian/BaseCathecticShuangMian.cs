using AllLottery.Business.Config;
using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Cathectic.LiuHeCai.CathecticShuangMian
{
    public abstract class BaseCathecticShuangMian : BaseCathecticLiuHeCai
    {
        public abstract int Index { get; }

        private readonly string[] _valid = { "大", "小", "单", "双", "大单", "大双", "小单", "小双", "合大", "合小", "合单", "合双", "尾大", "尾小", "家禽", "野兽", "红波", "绿波", "蓝波" };

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
            if (BaseConfig.HasValue(SystemConfigEnum.IsBoYue))
            {
                if (new List<string>() { "大", "小", "单", "双", "大单", "大双", "小单", "小双" }.Contains(data) && openDatas[Index] == 49)
                {
                    return 1;
                }
            }
            switch (data)
            {
                case "大":
                    if (openDatas[Index] >= 25 && openDatas[Index] < 49)
                    {
                        return 41650;
                    }
                    break;
                case "小":
                    if (openDatas[Index] <= 24)
                    {
                        return 41650;
                    }
                    break;
                case "单":
                    if (openDatas[Index] % 2 == 1 && openDatas[Index] < 49)
                    {
                        return 41650;
                    }
                    break;
                case "双":
                    if (openDatas[Index] % 2 == 0 && openDatas[Index] < 49)
                    {
                        return 41650;
                    }
                    break;
                case "大单":
                    if (openDatas[Index] % 2 == 1 && openDatas[Index] >= 25 && openDatas[Index] < 49)
                    {
                        return 83300;
                    }
                    break;
                case "大双":
                    if (openDatas[Index] % 2 == 0 && openDatas[Index] >= 25 && openDatas[Index] < 49)
                    {
                        return 83300;
                    }
                    break;
                case "小单":
                    if (openDatas[Index] % 2 == 1 && openDatas[Index] <= 24)
                    {
                        return 83300;
                    }
                    break;
                case "小双":
                    if (openDatas[Index] % 2 == 0 && openDatas[Index] <= 24)
                    {
                        return 83300;
                    }
                    break;
                case "合大":
                    if (openDatas[Index] / 10 + openDatas[Index] % 10 > 6 && openDatas[Index] < 49)
                    {
                        return 41650;
                    }
                    break;
                case "合小":
                    if (openDatas[Index] / 10 + openDatas[Index] % 10 <= 6 && openDatas[Index] < 49)
                    {
                        return 41650;
                    }
                    break;
                case "合单":
                    if ((openDatas[Index] / 10 + openDatas[Index] % 10) % 2 == 1 && openDatas[Index] < 49)
                    {
                        return 41650;
                    }
                    break;
                case "合双":
                    if ((openDatas[Index] / 10 + openDatas[Index] % 10) % 2 == 0 && openDatas[Index] < 49)
                    {
                        return 41650;
                    }
                    break;
                case "尾大":
                    if (openDatas[Index] % 10 > 4 && openDatas[Index] < 49)
                    {
                        return 41650;
                    }
                    break;
                case "尾小":
                    if (openDatas[Index] % 10 <= 4 && openDatas[Index] < 49)
                    {
                        return 41650;
                    }
                    break;
                case "家禽":
                    if (GetAnimalNumber("牛").Contains(openDatas[Index]) || GetAnimalNumber("马").Contains(openDatas[Index]) || GetAnimalNumber("羊").Contains(openDatas[Index]) || GetAnimalNumber("鸡").Contains(openDatas[Index]) || GetAnimalNumber("狗").Contains(openDatas[Index]) || GetAnimalNumber("猪").Contains(openDatas[Index]))
                    {
                        //牛、马、羊、鸡、狗、猪
                        if (new string[] { "牛", "马", "羊", "鸡", "狗", "猪" }.Contains(ToyearShuXiang()))
                        {
                            return 39984;
                        }
                        else
                        {
                            return 41650;
                        }
                    }
                    break;
                case "野兽":
                    if (GetAnimalNumber("鼠").Contains(openDatas[Index]) || GetAnimalNumber("虎").Contains(openDatas[Index]) || GetAnimalNumber("兔").Contains(openDatas[Index]) || GetAnimalNumber("龙").Contains(openDatas[Index]) || GetAnimalNumber("蛇").Contains(openDatas[Index]) || GetAnimalNumber("猴").Contains(openDatas[Index]))
                    {
                        //鼠、虎、兔、龙、蛇、猴
                        if (new string[] { "鼠", "虎", "兔", "龙", "蛇", "猴" }.Contains(ToyearShuXiang()))
                        {
                            return 39984;
                        }
                        else
                        {
                            return 41650;
                        }
                    }
                    break;
                case "红波":
                    if (GetColorNumber("红").Contains(openDatas[Index]))
                    {
                        return 58800;
                    }
                    break;
                case "绿波":
                    if (GetColorNumber("绿").Contains(openDatas[Index]))
                    {
                        return 62475;
                    }
                    break;
                case "蓝波":
                    if (GetColorNumber("蓝").Contains(openDatas[Index]))
                    {
                        return 62475;
                    }
                    break;
                default:
                    break;
            }
            return 0;
        }

        public override decimal CalculateWinMoney(Bet bet, decimal maxRate, int[] openDatas)
        {
            if (BaseConfig.HasValue(SystemConfigEnum.IsBoYue))
            {
                var wincount = bet.WinBetCount;
                var sts = bet.BetNo.Split(",");
                decimal sum = 0;
                foreach (var st in sts)
                {
                    if (new List<string>() { "大", "小", "单", "双", "大单", "大双", "小单", "小双" }.Contains(st) && IsWinEach(st, openDatas) == 1)
                    {
                        sum += bet.BetMode.Money * bet.Times;
                        bet.WinBetCount--;
                    }
                }
                var money = sum + base.CalculateWinMoney(bet, maxRate, openDatas);
                bet.WinBetCount = wincount;
                return money;
            }
            return base.CalculateWinMoney(bet, maxRate, openDatas);
        }
    }
}