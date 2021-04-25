using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AllLottery.Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = LotteryContext.CreateContext())
            {
                //foreach (Model.Player player in context.Players)
                //{
                //    player.UpdateReportDate();
                //    player.BetMoney = GetMoney(from b in context.Bets
                //                               where b.IsEnable && (b.Status == BetStatusEnum.Win || b.Status == BetStatusEnum.Lose) &&
                //                                     b.PlayerId == player.PlayerId && b.UpdateTime > DateTime.Today
                //                               select b.BetMoney);
                //}
                //foreach (LotteryType type in context.LotteryTypes)
                //{
                //    var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
                //    type.UpdateReportDate();
                //    type.BetMoney = GetMoney(from b in context.Bets
                //                             where b.LotteryPlayDetail.LotteryPlayType.LotteryTypeId == type.LotteryTypeId &&
                //                                   b.UpdateTime > DateTime.Today &&
                //                                   (b.Status == BetStatusEnum.Win || b.Status == BetStatusEnum.Lose)
                //                                   && b.IsEnable && !ids.Contains(b.PlayerId)
                //                             select b.BetMoney);
                //    type.WinMoney = GetMoney(from b in context.Bets
                //                             where b.LotteryPlayDetail.LotteryPlayType.LotteryTypeId == type.LotteryTypeId &&
                //                                   b.UpdateTime > DateTime.Today
                //                                   && b.IsEnable && !ids.Contains(b.PlayerId)
                //                             select b.WinMoney);
                //}
                //context.SaveChanges();]


                //for (int i = 1; i < 11; i++)
                //{
                //    for (int j = i + 1; j < 11; j++)
                //    {
                //        for (int k = j + 1; k < 11; k++)
                //        {
                //            Dictionary<int, int> dic = new Dictionary<int, int>();
                //            var iis = new int[] { i, j, k };
                //            var hei = new int[10];
                //            var max = 0;
                //            foreach (LotteryData data in context.LotteryDatas.Where(t => t.LotteryTypeId == 2))
                //            {
                //                var datas = data.Data.Split(",");
                //                List<int> numbers = new List<int>();
                //                foreach (string s in datas)
                //                {
                //                    numbers.Add(int.Parse(s));
                //                }

                //                for (int jj = 0; jj < hei.Length; jj++)
                //                {
                //                    if (iis.Contains(numbers[jj]))
                //                    {
                //                        hei[jj]++;
                //                    }
                //                    else
                //                    {
                //                        if (hei[jj] > 0)
                //                        {
                //                            if (!dic.ContainsKey(hei[jj]))
                //                            {
                //                                dic.Add(hei[jj], 0);
                //                            }

                //                            dic[hei[jj]]++;
                //                        }
                //                        max = Math.Max(hei[jj], max);
                //                        hei[jj] = 0;
                //                    }
                //                }
                //            }
                //            StringBuilder sb = new StringBuilder();
                //            foreach (int i1 in dic.Keys.OrderBy(t => t))
                //            {
                //                sb.Append(i1 + ":" + dic[i1] + ",");
                //            }
                //            Console.WriteLine($"{i},{j},{k}三个数字最多连续出现了{max}次,{sb.ToString().Trim(',')}");
                //        }
                //    }
                //}
                //for (int i = 0; i < 10; i++)
                //{
                //    for (int j = i + 1; j < 10; j++)
                //    {
                //        for (int k = j + 1; k < 10; k++)
                //        {
                //            Dictionary<int, int> dic = new Dictionary<int, int>();
                //            var iis = new int[] { i, j, k };
                //            var hei = new int[5];
                //            var max = 0;
                //            decimal betMoney = 0, winMoney = 0;
                //            decimal[] bets = new[] { 4M, 15, 60, 210, 710 };
                //            foreach (LotteryData data in context.LotteryDatas.Where(t => t.LotteryTypeId == 41))
                //            {
                //                var datas = data.Data.Split(",");
                //                List<int> numbers = new List<int>();
                //                foreach (string s in datas)
                //                {
                //                    numbers.Add(int.Parse(s));
                //                }

                //                for (int jj = 0; jj < hei.Length; jj++)
                //                {
                //                    if (iis.Contains(numbers[jj]))
                //                    {
                //                        hei[jj]++;
                //                    }
                //                    else
                //                    {
                //                        if (hei[jj] > 1)
                //                        {
                //                            if (!dic.ContainsKey(hei[jj]))
                //                            {
                //                                dic.Add(hei[jj], 0);
                //                            }

                //                            dic[hei[jj]]++;

                //                            if (hei[jj] > 6)
                //                            {

                //                            }

                //                        }
                //                        max = Math.Max(hei[jj], max);
                //                        hei[jj] = 0;
                //                    }
                //                }
                //            }
                //            StringBuilder sb = new StringBuilder();
                //            foreach (int i1 in dic.Keys.OrderBy(t => t))
                //            {
                //                sb.Append(i1 + ":" + dic[i1] + ",");
                //                if (i1 >= 7)
                //                {
                //                    betMoney += bets.Sum() * 7 * dic[i1];
                //                }
                //                else
                //                {
                //                    for (int l = 2; l <= i1; l++)
                //                    {
                //                        betMoney += bets[l - 2] * 7 * dic[i1];
                //                    }

                //                    winMoney += bets[i1 - 2] * 10 * 0.995M * dic[i1];
                //                }


                //            }
                //            Console.WriteLine($"{i},{j},{k}三个数字投注金额是{betMoney},中奖金额是{winMoney},盈利金额是{winMoney - betMoney}");
                //        }
                //    }
                //}
            }
            Console.WriteLine("更新完成");
            Console.ReadKey();
        }

        public static List<LotteryPlayType> CreateSsc(bool isNotTengXun = true)
        {
            return new List<LotteryPlayType>()
            {
                new LotteryPlayType()
                {
                    Name = "五星",
                    SortIndex = -2,
                    IsEnable = isNotTengXun,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "五星复式",
                            MinOdds = 90000,
                            MaxOdds = 100000,
                            SortIndex = 0,
                            Description = "每位至少选择一个号码，竞猜开奖号码的全部五位，号码和位置都对应即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticFiveStartDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "五星单式",
                            MinOdds = 90000,
                            MaxOdds = 100000,
                            SortIndex = 1,
                            Description = "每位至少选择一个号码，竞猜开奖号码的全部五位，号码和位置都对应即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticFiveStartSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选120",
                            MinOdds = 750,
                            MaxOdds = 833.333333m,
                            SortIndex = 2,
                            Description = "至少选择五个号码投注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose120"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选60",
                            MinOdds = 1500,
                            MaxOdds = 1666.666666M,
                            SortIndex = 3,
                            Description = "至少选择1个二重号码和3个单号号码组成一注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose60"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选30",
                            MinOdds = 3000,
                            MaxOdds = 3333.333333M,
                            SortIndex = 4,
                            Description = "至少选择2个二重号码和1个单号号码组成一注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose30"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选20",
                            MinOdds = 4500,
                            MaxOdds = 5000,
                            SortIndex = 5,
                            Description = "至少选择1个三重号码和2个单号号码组成一注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose20"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选10",
                            MinOdds = 9000,
                            MaxOdds = 10000,
                            SortIndex = 6,
                            Description = "至少选择1个三重号码和1个二重号码组成一注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose10"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选5",
                            MinOdds = 18000,
                            MaxOdds = 20000,
                            SortIndex = 7,
                            Description = "至少选择1个四重号码和1个单号号码组成一注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose5"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "四星",
                    SortIndex = -1,
                    IsEnable = isNotTengXun,
                    LotteryPlayDetails=new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "复式",
                            MinOdds = 9000,
                            MaxOdds = 10000,
                            SortIndex = 1,
                            Description = "每位至少选择一个号码，竞猜开奖号码的后四位，号码和位置都对应即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticFourBehindDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "单式",
                            MinOdds = 9000,
                            MaxOdds = 10000,
                            SortIndex = 2,
                            Description = "每位至少选择一个号码，竞猜开奖号码的后四位，号码和位置都对应即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticFourBehindSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选24",
                            MinOdds = 375,
                            MaxOdds = 416.666666M,
                            SortIndex = 3,
                            Description = "至少选择4个号码投注，竞猜开奖号码的后4位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFourChoose24"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选12",
                            MinOdds = 750,
                            MaxOdds = 833.333333M,
                            SortIndex = 4,
                            Description = "至少选择1个二重号码和2个单号号码，竞猜开奖号码的后四位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFourChoose24"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选6",
                            MinOdds = 1500,
                            MaxOdds = 1666.666666M,
                            SortIndex = 5,
                            Description = "至少选择2个二重号码，竞猜开奖号码的后四位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFourChoose24"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选4",
                            MinOdds = 2250,
                            MaxOdds = 2500,
                            SortIndex = 6,
                            Description = "至少选择1个三重号码和1个单号号码，竞猜开奖号码的后四位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFourChoose4"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "前三",
                    SortIndex = 0,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 0,
                            Description = "每位各选1个或多个号码，选号与奖号前三位一一对应，中奖[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeDuplex.CathecticThreeFrontDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组三",
                            MinOdds = 300,
                            MaxOdds = 333.333333M,
                            SortIndex = 1,
                            Description = "组三是指开奖号码前三位任意两位号码相同，如188。至少选2个号码投注，开奖号前三位为组三号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree.CathecticGroupThreeFrontThree"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组六",
                            MinOdds = 150,
                            MaxOdds = 166.666666m,
                            SortIndex = 2,
                            Description = "组六是指开奖号码前三位三个号码各不相同，如135。至少选3个号码投注，开奖号前三位为组六号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSix.CathecticGroupThreeFrontSix"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选和值",
                            MinOdds = 129,
                            MaxOdds = 143.333333m,
                            SortIndex = 3,
                            Description = "所选和值与开奖号码前三位和值一致即为中奖。奖金[jj]元；组三奖金两倍；豹子奖金三倍。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSum.CathecticGroupThreeFrontSum"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "单式上传",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 4,
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeSingle.CathecticThreeFrontSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "中三",
                    SortIndex = 1,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 0,
                            Description = "每位各选1个或多个号码，选号与奖号中三位一一对应，中奖[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeDuplex.CathecticThreeMiddleDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组三",
                            MinOdds = 300,
                            MaxOdds = 333.333333M,
                            SortIndex = 1,
                            Description = "组三是指开奖号码中三位任意两位号码相同，如188。至少选2个号码投注，开奖号中三位为组三号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree.CathecticGroupThreeMiddleThree"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组六",
                            MinOdds = 150,
                            MaxOdds = 163.5m,
                            SortIndex = 2,
                            Description = "组六是指开奖号码中三位三个号码各不相同，如135。至少选3个号码投注，开奖号中三位为组六号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSix.CathecticGroupThreeMiddleSix"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选和值",
                            MinOdds = 129,
                            MaxOdds = 143.333333m,
                            SortIndex = 3,
                            Description = "所选和值与开奖号码中三位和值一致即为中奖。奖金[jj]元；组三奖金两倍；豹子奖金三倍。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSum.CathecticGroupThreeMiddleSum"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "单式上传",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 4,
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeSingle.CathecticThreeMiddleSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "后三",
                    SortIndex = 2,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 0,
                            Description = "每位各选1个或多个号码，选号与奖号后三位一一对应，中奖[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeDuplex.CathecticThreeBehindDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组三",
                            MinOdds = 300,
                            MaxOdds = 333.333333m,
                            Description = "组三是指开奖号码后三位任意两位号码相同，如188。至少选2个号码投注，开奖号后三位为组三号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree.CathecticGroupThreeBehindThree"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组六",
                            MinOdds = 150,
                            MaxOdds = 166.666666m,
                            SortIndex = 2,
                            Description = "组六是指开奖号码后三位三个号码各不相同，如135。至少选3个号码投注，开奖号后三位为组六号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSix.CathecticGroupThreeBehindSix"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选和值",
                            MinOdds = 129,
                            MaxOdds = 143.333333m,
                            SortIndex = 3,
                            Description = "所选和值与开奖号码后三位和值一致即为中奖。奖金[jj]元；组三奖金两倍；豹子奖金三倍。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSum.CathecticGroupThreeBehindSum"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "单式上传",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 4,
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeSingle.CathecticThreeBehindSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "二星",
                    SortIndex = 3,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 90,
                            MaxOdds = 100,
                            SortIndex = 0,
                            Description = "每位各选1个或多个号，所选号与开奖号后两位相同（且顺序一致），即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticTwoBehindDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选",
                            MinOdds = 45,
                            MaxOdds = 50,
                            SortIndex = 1,
                            Description = "从0～9中选2个或多个号码，选号与奖号后二位相同（顺序不限，不含对子号），即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupTwoBehindDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选单式",
                            MinOdds = 45,
                            MaxOdds = 50,
                            SortIndex = 2,
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupTwoBehindSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "定位胆",
                    SortIndex = 4,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "五星定位胆",
                            MinOdds = 9,
                            MaxOdds = 10,
                            Description = "在万位、千位、百位、十位、个位任意位置上任意选择1个或1个以上号码即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticFixedBileFiveStart"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "趣味",
                    SortIndex = 5,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "一帆风顺",
                            MinOdds = 2.197748m,
                            MaxOdds = 2.441942m,
                            SortIndex = 0,
                            Description = "至少出现一个号码，即中一帆风顺，中奖可得[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticFunny.CathecticFunnyYFFS"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "好事成双",
                            MinOdds = 11.048367m,
                            MaxOdds = 12.275963m,
                            SortIndex = 1,
                            Description = "至少出现两个相同号码，即中好事成双，中奖可得[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticFunny.CathecticFunnyHSCS"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "三星报喜",
                            MinOdds = 105.140186m,
                            MaxOdds = 116.822429m,
                            SortIndex = 2,
                            Description = "至少出现三个相同号码，即中三星报喜，中奖可得[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticFunny.CathecticFunnySXBX"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "四季发财",
                            MinOdds = 1956.521739m,
                            MaxOdds = 2173.913043m,
                            SortIndex = 3,
                            Description = "至少出现四个相同号码，即中四季发财，中奖可得[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticFunny.CathecticFunnySJFC"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "龙虎",
                    SortIndex = 6,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "龙",
                            MinOdds = 2,
                            MaxOdds = 2.222222m,
                            SortIndex = 0,
                            Description = "从对应两个位上选择一个形态组成一注，前者大于后者为“龙”，反之为“虎”，相等为“和”，中奖可得[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticDT.CathecticDTDragon"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "虎",
                            MinOdds = 2,
                            MaxOdds = 2.222222m,
                            SortIndex = 1,
                            Description = "从对应两个位上选择一个形态组成一注，前者大于后者为“龙”，反之为“虎”，相等为“和”中奖可得[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticDT.CathecticDTTiger"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "和",
                            MinOdds = 9,
                            MaxOdds = 10,
                            SortIndex = 2,
                            Description = "从对应两个位上选择一个形态组成一注，前者大于后者为“龙”，反之为“虎”，相等为“和”中奖可得[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticDT.CathecticDTSum"
                        },
                    }
                },
                new LotteryPlayType()
                {
                    Name = "信用玩法",
                    SortIndex = 7,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "整合",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 0,
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.XinYongPan.ShuangMianPan"
                        }
                    }
                }
            };
        }



        //private static List<Model.Player> GetUnderPlayers(Guid playerPlayerId, MsContext context)
        //{
        //    List<Model.Player> list = new List<Model.Player>();
        //    var sql = from p in context.Players where p.ParentPlayerId == playerPlayerId select p;
        //    if (sql.Any())
        //    {
        //        foreach (var player in sql.ToArray())
        //        {
        //            list.Add(new Model.Player()
        //            {
        //                Name = player.Name,
        //                Rebate = 0.075M,
        //                Coin = player.Coin,
        //                FCoin = player.FCoin,
        //                Phone = player.Phone,
        //                QQ = player.QQ,
        //                WeChat = player.WeChat,
        //                Type = player.Type == 1 ? PlayerTypeEnum.Proxy : PlayerTypeEnum.Member,
        //                Players = GetUnderPlayers(player.PlayerId, context),
        //                IsEnable = player.IsEnable,
        //                CreateTime = player.CreateTime,
        //                UpdateTime = player.UpdateTime
        //            });
        //        }
        //        return list;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //private static decimal GetMoney(IQueryable<decimal> sql)
        //{
        //    if (sql.Any())
        //    {
        //        return sql.Sum();
        //    }
        //    return 0;
        //}

        //public static List<LotteryOpenTime> GetLotteryOpenTimes(params IntiLottertTime[] times)
        //{
        //    if (times == null)
        //    {
        //        return null;
        //    }
        //    List<LotteryOpenTime> list = new List<LotteryOpenTime>();
        //    foreach (IntiLottertTime time in times)
        //    {
        //        for (TimeSpan i = time.BeginTime; i <= time.EndTime; i = i + time.Interval)
        //        {
        //            LotteryOpenTime openTime = new LotteryOpenTime();
        //            openTime.OpenNumber = list.Count + 1;
        //            if (i >= new TimeSpan(1, 0, 0, 0))
        //            {
        //                openTime.OpenTime = i - new TimeSpan(1, 0, 0, 0);
        //                openTime.IsTomorrow = true;
        //            }
        //            else
        //            {
        //                openTime.OpenTime = i;
        //            }
        //            list.Add(openTime);
        //        }
        //    }

        //    return list;
        //}

        //public static List<LotteryPlayType> Create3D()
        //{
        //    return new List<LotteryPlayType>()
        //    {
        //        new LotteryPlayType()
        //        {
        //            Name = "三星",
        //            SortIndex = 0,
        //            LotteryPlayDetails = new List<LotteryPlayDetail>()
        //            {
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "直选",
        //                    MinOdds = 900,
        //                    MaxOdds = 1000,
        //                    SortIndex = 0,
        //                    Description = "每位各选1个或多个号码，选号与奖号一一对应，中奖[jj]元。",
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticThreeDuplex"
        //                },
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "组三",
        //                    MinOdds = 300,
        //                    MaxOdds = 333.333333m,
        //                    SortIndex = 1,
        //                    Description = "组三是指开奖号码任意两位号码相同，如188。至少选2个号码投注，开奖号为组三号且包含在投注号码中，即中[jj]元。",
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticGroupThreeThree"
        //                },
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "组六",
        //                    MinOdds = 150,
        //                    MaxOdds = 166.666666m,
        //                    SortIndex = 2,
        //                    Description = "组六是指开奖号码三个号码各不相同，如135。至少选3个号码投注，开奖号为组六号且包含在投注号码中，即中[jj]元。",
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticGroupSix"
        //                },
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "组选和值",
        //                    MinOdds = 129,
        //                    MaxOdds = 143.333333m,
        //                    SortIndex = 3,
        //                    Description = "所选和值与开奖号码和值一致即为中奖。开奖号码为豹子奖金1800元；组三奖金600元；组六奖金300元。",
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticGroupSum"
        //                },
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "单式上传",
        //                    MinOdds = 900,
        //                    MaxOdds = 1000,
        //                    SortIndex = 4,
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticThreeSingle"
        //                }
        //            }
        //        },
        //        new LotteryPlayType()
        //        {
        //            Name = "二星",
        //            SortIndex = 3,
        //            LotteryPlayDetails = new List<LotteryPlayDetail>()
        //            {
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "直选",
        //                    MinOdds = 90,
        //                    MaxOdds = 100,
        //                    SortIndex = 0,
        //                    Description = "每位各选1个或多个号，所选号与开奖号后两位相同（且顺序一致），即中奖[jj]元。",
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticTwoBehindDuplex"
        //                },
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "组选",
        //                    MinOdds = 45,
        //                    MaxOdds = 50,
        //                    SortIndex = 1,
        //                    Description = "从0～9中选2个或多个号码，选号与奖号后二位相同（顺序不限，不含对子号），即中奖[jj]元。",
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticGroupTwoBehindDuplex"
        //                },
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "单式上传",
        //                    MinOdds = 45,
        //                    MaxOdds = 50,
        //                    SortIndex = 2,
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticGroupTwoBehindSingle"
        //                }
        //            }
        //        },
        //        new LotteryPlayType()
        //        {
        //            Name = "定位胆",
        //            SortIndex = 4,
        //            LotteryPlayDetails = new List<LotteryPlayDetail>()
        //            {
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "定位胆",
        //                    MinOdds = 9,
        //                    MaxOdds = 10,
        //                    Description = "在万位、千位、百位、十位、个位任意位置上任意选择1个或1个以上号码。",
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticFixedBileThreeStart"
        //                }
        //            }
        //        },
        //        new LotteryPlayType()
        //        {
        //            Name = "不定胆",
        //            SortIndex = 5,
        //            LotteryPlayDetails = new List<LotteryPlayDetail>()
        //            {
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "不定胆",
        //                    MinOdds = 3.321033M,
        //                    MaxOdds = 3.690036M,
        //                    Description = "开奖号码中，至少出现一个你所选择号码，即中[jj]元。",
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticUnFixed"
        //                }
        //            }
        //        },
        //        new LotteryPlayType()
        //        {
        //            Name = "大小单双",
        //            SortIndex = 6,
        //            LotteryPlayDetails = new List<LotteryPlayDetail>()
        //            {
        //                new LotteryPlayDetail()
        //                {
        //                    Name = "大小单双",
        //                    MinOdds = 3.6M,
        //                    MaxOdds = 4,
        //                    Description = "大（56789）小（01234）、单（13579）双（02468）形态进行购买，所选号码的形态与开奖号码的位置、形态相同，即中[jj]元。",
        //                    ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticBSSB"
        //                }
        //            }
        //        },
        //    };
        //}

    }
}
