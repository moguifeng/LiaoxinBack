using System;
using System.Collections.Generic;
using Zzb;

namespace AllLottery.Business.Cathectic.PaiLie3D
{
    public class CathecticGroupSum : BaseCathectic
    {
        public override int Cathectic(string datas)
        {
            try
            {
                int cathecticNum = 0;
                Dictionary<int, int> dicCathetic = new Dictionary<int, int>();
                dicCathetic.Add(0, 1);
                dicCathetic.Add(1, 2);
                dicCathetic.Add(2, 4);
                dicCathetic.Add(3, 5);
                dicCathetic.Add(4, 8);
                dicCathetic.Add(5, 10);
                dicCathetic.Add(6, 13);
                dicCathetic.Add(7, 16);
                dicCathetic.Add(8, 20);
                dicCathetic.Add(9, 23);
                dicCathetic.Add(10, 26);
                dicCathetic.Add(11, 28);
                dicCathetic.Add(12, 29);
                dicCathetic.Add(13, 30);
                dicCathetic.Add(14, 30);
                dicCathetic.Add(15, 29);
                dicCathetic.Add(16, 28);
                dicCathetic.Add(17, 26);
                dicCathetic.Add(18, 23);
                dicCathetic.Add(19, 20);
                dicCathetic.Add(20, 16);
                dicCathetic.Add(21, 13);
                dicCathetic.Add(22, 10);
                dicCathetic.Add(23, 8);
                dicCathetic.Add(24, 5);
                dicCathetic.Add(25, 4);
                dicCathetic.Add(26, 2);
                dicCathetic.Add(27, 1);
                string[] datasNum = SplitByDatas(datas);
                foreach (string numStr in datasNum)
                {
                    cathecticNum += dicCathetic[Int32.Parse(numStr)];
                }
                return cathecticNum;
            }
            catch (Exception ex)
            {
                throw new ZzbException("内部算法错误" + ex);
            }
        }


        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 1, 28) &&
                    CheckRepeatData(datas) && CheckEachChoiceAtMaxCount(datas, 2));
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            int dataNum = int.Parse(data);

            int sum = 0;
            Dictionary<int, bool> dic = new Dictionary<int, bool>();
            for (int i = 0; i < 3; i++)
            {
                sum += openDatas[i];
                if (!dic.ContainsKey(openDatas[i]))
                {
                    dic.Add(openDatas[i], true);
                }
            }
            if (sum == dataNum)
            {
                return 1;
            }
            return 0;
        }
    }
}