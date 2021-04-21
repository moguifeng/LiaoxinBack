using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.Common;

namespace AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeDuplex
{
    public abstract class BaseCathecticThreeDuplex : BaseCathectic
    {
        protected abstract int JianGe { get; }

        public override bool Valid(string datas)
        {
            if (!datas.Contains('_'))
            {
                return false;
            }
            if (CheckAllNumber(datas, 3) && CheckRepeatData(datas, 3))
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
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return GetMartrixAllCombination(datas).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            data = string.Join(string.Empty, data);
            string openDataStr = string.Join(string.Empty, openDatas);
            for (int i = 0; i < 3; i++)
            {
                if (openDataStr[i + JianGe] != data[i])
                {
                    return 0;
                }
            }
            return 1;
        }

        public override string CreateRandomNumber()
        {
            StringBuilder sb = new StringBuilder();

            for (int j = 1; j <= 3; j++)
            {
                //创建list
                List<int> list = CreateSscBetIntList(RandomHelper.Next(7, 9));

                StringBuilder nsb = new StringBuilder();
                nsb.Append("_");
                var eachNumer = list.ToList();
                if (eachNumer.Count == 0)
                {
                    nsb.Append("-");
                }
                else
                {
                    foreach (int i in eachNumer)
                    {
                        nsb.Append(i % 10 + ",");
                    }
                }

                sb.Append(nsb.ToString().Trim(','));
            }

            return sb.ToString().Trim('_');
        }
    }
}