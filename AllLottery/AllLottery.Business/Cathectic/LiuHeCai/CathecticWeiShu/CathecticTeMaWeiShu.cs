using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.LiuHeCai.CathecticWeiShu
{
    public class CathecticTeMaWeiShu : BaseCathectic
    {
        public override bool Valid(string data)
        {
            var t = data.Split(",");
            List<string> list = new List<string>();
            foreach (string datas in t)
            {
                if (list.Contains(datas))
                {
                    return false;
                }
                if (datas[1] == '头')
                {
                    if (!int.TryParse(datas[0].ToString(), out var i))
                    {
                        return false;
                    }

                    if (i < 0 || i > 4)
                    {
                        return false;
                    }
                }
                else if (datas[1] == '尾')
                {
                    if (!int.TryParse(datas[0].ToString(), out var i))
                    {
                        return false;
                    }

                    if (i < 0 || i > 9)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                list.Add(datas);
            }
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return datas.Split(",");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (data[1] == '头')
            {
                if (int.Parse(data[0].ToString()) == openDatas[6] / 10)
                {
                    if (openDatas[6] / 10 == 0)
                    {
                        return 20;
                    }
                    else
                    {
                        return 18;
                    }
                }
            }
            if (data[1] == '尾')
            {
                if (int.Parse(data[0].ToString()) == openDatas[6] % 10)
                {
                    if (openDatas[6] % 10 == 0)
                    {
                        return 45;
                    }
                    else
                    {
                        return 36;
                    }
                }
            }
            return 0;
        }
    }
}