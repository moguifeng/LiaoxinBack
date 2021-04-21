using System.Collections.Generic;
using System.Linq;
using Zzb;

namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticFiveStartDuplex : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            var res = (CheckAllNumber(datas, 5) &&
                       CheckRepeatData(datas, 5)) ? true : false;
            if (res)
            {
                var datasGroup = GroupByDatas(datas);
                foreach (string data in datasGroup)
                {
                    if (!(CheckChoiceOfMaxMinRange(data, 1, 10) && CheckEachChoiceCount(data, 1)))
                    {
                        return false;
                    }
                }
            }
            return res;
        }

        public override string[] SplitDatas(string datas)
        {
            return GetMartrixAllCombination(datas).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            data = string.Join(string.Empty, data);
            if (string.Join(string.Empty, openDatas) == data)
            {
                return 1;
            }
            return 0;
        }

        public override long IsWin(string datas, int[] openDatas)
        {
            string[] numbers = datas.Split('_');
            if (numbers.Length != 5)
            {
                throw new ZzbException("五星复式分解错误，分解后组数不等于5");
            }
            List<int[]> list = new List<int[]>();
            foreach (string number in numbers)
            {
                List<int> temp = new List<int>();
                foreach (string s in number.Split(','))
                {
                    temp.Add(int.Parse(s));
                }
                list.Add(temp.ToArray());
            }
            var isWin = list[0].Contains(openDatas[0]) && list[1].Contains(openDatas[1]) &&
                        list[2].Contains(openDatas[2]) && list[3].Contains(openDatas[3]) &&
                        list[4].Contains(openDatas[4]);
            return isWin ? 1 : 0;
        }
    }
}