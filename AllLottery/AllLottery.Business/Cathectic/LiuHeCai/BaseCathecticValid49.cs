using System;
using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.LiuHeCai
{
    public abstract class BaseCathecticValid49 : BaseCathectic
    {
        public override bool Valid(string data)
        {
            try
            {
                var datas = data.Split(',');
                if (datas.Length > 49 || ExCheckError(datas))
                {
                    return false;
                }
                List<int> l = new List<int>();
                foreach (string s in datas)
                {
                    int i = int.Parse(s);
                    if (s.Length != 2 || i > 49 || i < 1 || l.Contains(i))
                    {
                        return false;
                    }
                    l.Add(i);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual bool ExCheckError(string[] datas)
        {
            return false;
        }
    }
}