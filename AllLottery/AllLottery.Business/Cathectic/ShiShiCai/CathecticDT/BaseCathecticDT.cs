using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.ShiShiCai.CathecticDT
{
    public abstract class BaseCathecticDT : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return DTValid(datas);
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            int firstIndex = 0;
            int secondIndex = 0;
            DTWin(data, openDatas, out firstIndex, out secondIndex);
            //if (firstIndex > secondIndex)
            //{
            //    return true;
            //}
            //return false;
            return Check(firstIndex, secondIndex) ? 1 : 0;
        }

        protected abstract bool Check(int firstIndex, int secondIndex);

        protected void DTWin(string data, int[] openDatas, out int firstIndex, out int secondIndex)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("万", 0);
            dic.Add("千", 1);
            dic.Add("百", 2);
            dic.Add("十", 3);
            dic.Add("个", 4);

            string dataOne = data[0].ToString();
            string dataTwo = data[1].ToString();
            firstIndex = openDatas[dic[dataOne]];
            secondIndex = openDatas[dic[dataTwo]];
        }

        protected bool DTValid(string datas)
        {
            List<string> lisValidStr = new List<string>();
            lisValidStr.Add("万");
            lisValidStr.Add("千");
            lisValidStr.Add("百");
            lisValidStr.Add("十");
            lisValidStr.Add("个");
            var res = (
                CheckRepeatData(datas) &&
                CheckChoiceOfMaxMinRange(datas, 1, 10) &&
                CheckEachChoiceCount(datas, 2)) ? true : false;
            if (res)
            {
                if (datas[0] == datas[1])
                {
                    return false;
                }
                if (!(lisValidStr.Contains(datas[0].ToString()) && lisValidStr.Contains(datas[1].ToString())))
                {
                    return false;
                }
            }
            return res;
        }


    }
}