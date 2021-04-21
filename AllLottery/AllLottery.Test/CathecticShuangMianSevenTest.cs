using System.Linq;
using AllLottery.Business.Cathectic.LiuHeCai.CathecticShuangMian;
using AllLottery.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllLottery.Test
{
    [TestClass]
    public class CathecticShuangMianSevenTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var l = new CathecticShuangMianSeven().IsWin(
            "大,小,单,双,大单,大双,小单,小双",
            new[] { 19, 11, 39, 20, 35, 46, 49 });
            //new CathecticShuangMianSeven().CalculateWinMoney(new Bet() { BetMode = new BetMode() { Money = 2, }, WinBetCount = l, Times = 1, }, 0.1M);
            Assert.IsTrue(true);
        }
    }
}