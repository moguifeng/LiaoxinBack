using Zzb;

namespace AllLottery.Business.Cathectic.KuaiSan
{
    public class CathecticTwoDifferent : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckRepeatData(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 2, 6) && CheckEachChoiceCount(datas, 1));
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 2);
        }

        public override long IsWin(string datas, int[] openDatas)
        {
            if (Valid(datas))
            {
                int res = 0;
                string[] myDatas = this.SplitDatas(datas);

                string openDataStr = string.Join(string.Empty, openDatas);

                foreach (string myData in myDatas)
                {

                    string dataOne = myData[0].ToString();
                    string dataTwo = myData[1].ToString();

                    if (openDataStr.Contains(dataOne) && openDataStr.Contains(dataTwo))
                    {
                        res++;
                    }

                }
                return res;



            }
            else
            {
                throw new ZzbException($"算法[{this.GetType().Name}]出错，参数校验失败，参数datas：[{datas}]");
            }
        }


        public override long IsWinEach(string data, int[] openDatas)
        {
            string tOdata = string.Join(string.Empty, openDatas);
            if (tOdata[0].ToString() + tOdata[1].ToString() == data)
            {
                return 1;
            }
            return 0;
        }
    }
}