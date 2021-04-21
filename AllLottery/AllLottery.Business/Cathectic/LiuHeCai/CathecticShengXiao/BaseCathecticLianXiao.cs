using System.Linq;

namespace AllLottery.Business.Cathectic.LiuHeCai.CathecticShengXiao
{
    public abstract class BaseCathecticLianXiao : BaseCathecticShengXiao
    {
        protected abstract int Index { get; }

        protected abstract int BenMing { get; }

        protected abstract int NoBenMing { get; }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, Index, ",");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            var datas = data.Split(",");
            for (int i = 0; i < Index; i++)
            {
                var list = GetAnimalNumber(datas[i]);
                //list.AddRange(GetAnimalNumber(datas[i]));
                bool b = false;
                foreach (int d in openDatas)
                {
                    if (list.Contains(d))
                    {
                        b = true;
                        break;
                    }
                }

                if (!b)
                {
                    return 0;
                }
            }

            if (datas.Contains(ToyearShuXiang()))
            {
                return BenMing;
            }

            return NoBenMing;

        }
    }
}