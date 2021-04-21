using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Cathectic.LiuHeCai.CathecticShengXiao
{
    public abstract class BaseCathecticShengXiao : BaseCathecticLiuHeCai
    {
        public override bool Valid(string data)
        {
            var datas = data.Split(',');
            List<string> list = new List<string>();
            foreach (string s in datas)
            {
                if (list.Contains(s) || !Shuxiang.Contains(s))
                {
                    return false;
                }
                list.Add(s);
            }
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return datas.Split(',');
        }
    }
}