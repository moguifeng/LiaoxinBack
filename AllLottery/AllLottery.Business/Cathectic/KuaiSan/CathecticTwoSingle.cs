using System;
using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.KuaiSan
{
    public class CathecticTwoSingle : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            if (!CheckAllNumber(datas, 2))
            {
                return false;
            }
            var groupDatas = this.GroupByDatas(datas);
            foreach (string data in groupDatas)
            {
                if (!CheckChoiceOfMaxMinRange(data, 1, 5))
                {
                    return false;
                }
            }
            if (!(CheckEachChoiceCount(groupDatas[0], 2) && CheckEachChoiceCount(groupDatas[1], 1)))
            {
                return false;
            }

            string[] sData = this.SplitByDatas(groupDatas[0]);
            string[] sData1 = SplitByDatas(groupDatas[1]);
            foreach (var data in sData)
            {
                if (data[0] != data[1])
                {
                    return false;
                }
                foreach (var s in sData1)
                {
                    if (data[0] == s[0])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            List<string> lis = this.GetMartrixAllCombination(datas);
            return lis.ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            int[] numbers = new int[3];
            numbers[0] = int.Parse(data[0].ToString());
            numbers[1] = int.Parse(data[1].ToString());
            numbers[2] = int.Parse(data[2].ToString());
            Array.Sort(numbers);

            if (openDatas[0] == numbers[0] && openDatas[1] == numbers[1] && openDatas[2] == numbers[2])
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}