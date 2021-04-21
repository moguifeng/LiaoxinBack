using System.Collections;

namespace AllLottery.Business.Cathectic.LiuHeCai.CathecticBuZhong
{
    public abstract class BaseCathecticBuZhong : BaseCathecticValid49
    {
        protected abstract int Index { get; }

        protected override bool ExCheckError(string[] datas)
        {
            return datas.Length < Index;
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, Index, ",");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            var datas = data.Split(",");
            foreach (var s in datas)
            {
                if (((IList)openDatas).Contains(int.Parse(s)))
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}