namespace AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticSingle
{
    public abstract class BaseCathecticSingle : BaseCathectic
    {
        protected abstract int Number { get; }

        public override bool Valid(string datas)
        {
            //01,02_01,13
            var res = (CheckEachChoiceCount(datas, Number, "_")) ? true : false;
            if (res)
            {
                if (!CheckSingleChooseIsAllNumber(datas))
                {
                    return false;
                }
            }
            return res;
        }

        public override string[] SplitDatas(string datas)
        {
            return GroupByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            string[] datas = SplitByDatas(data);
            if (CheckTrue(datas, openDatas))
            {
                return 1;
            }
            return 0;
        }

        protected abstract bool CheckTrue(string[] datas, int[] openDatas);

        /// <summary>
        /// 单式系列判断
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected bool CheckSingleChooseIsAllNumber(string datas)
        {
            string[] datasGroup = SplitByDatas(datas, "_");
            for (int i = 0; i < datasGroup.Length; i++)
            {
                if (!CheckAllNumber(datasGroup[i], ","))
                {
                    return false;
                }
            }
            return true;
        }
    }
}