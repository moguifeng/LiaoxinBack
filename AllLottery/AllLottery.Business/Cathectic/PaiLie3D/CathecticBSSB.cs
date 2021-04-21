namespace AllLottery.Business.Cathectic.PaiLie3D
{
    public class CathecticBSSB : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return base.ValidDaXiaoDanShuang(datas, 2);
        }

        public override string[] SplitDatas(string datas)
        {
            return GetMartrixAllCombination(datas).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            string dataOneFlag = GetResultStr(data[0].ToString(), openDatas[1]);
            string dataTwoFlag = GetResultStr(data[1].ToString(), openDatas[2]);
            if ((dataOneFlag + dataTwoFlag) == data)
            {
                return 1;
            }
            return 0;
        }
    }
}