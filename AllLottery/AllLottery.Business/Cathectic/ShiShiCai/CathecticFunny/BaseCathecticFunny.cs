using System.Linq;

namespace AllLottery.Business.Cathectic.ShiShiCai.CathecticFunny
{
    public abstract class BaseCathecticFunny : BaseCathectic
    {
        protected abstract int Number { get; }

        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 1, 10) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 1)) ? true : false;
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            return FunnyEachWinByGreatherEqual(data, openDatas, Number) ? 1 : 0;
        }

        /// <summary>
        /// 趣味系列->一个数大于等于开奖数据的多少位就赢.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="openDatas"></param>
        /// <param name="dimension">多少位</param>
        /// <returns></returns>
        protected bool FunnyEachWinByGreatherEqual(string data, int[] openDatas, int dimension)
        {
            int dataNum = int.Parse(data);
            var lis = openDatas.ToList();
            if (lis.FindAll(c => c == dataNum).Count >= dimension)
            {
                return true;
            }
            return false;
        }
    }
}