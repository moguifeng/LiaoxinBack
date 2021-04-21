using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Zzb;

namespace AllLottery.Business.Cathectic.LiuHeCai.Credit
{
    public abstract class BaseLiuHeCaiCredit : BaseCredit
    {
        protected BaseLiuHeCaiCredit(string helpKey)
        {
            if (!HelpKeys.Contains(helpKey))
            {
                throw new ZzbException("创建六合彩算法错误：关键字错误");
            }
            Index = HelpKeys.IndexOf(helpKey);
        }


        private string[] HelpKeys => new[] { "正码一", "正码二", "正码三", "正码四", "正码五", "正码六", "特码" };

        protected string HelpKey => HelpKeys[Index];

        protected int Index { get; }

        public abstract string NewKey { get; }

        public override string Key => HelpKey + "-" + NewKey;
    }
}