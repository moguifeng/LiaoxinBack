using AllLottery.Business.Cathectic.LiuHeCai.CathecticWeiShu;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllLottery.Test
{
    [TestClass]
    public class CathecticTeMaWeiShuTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var l = new CathecticTeMaWeiShu().IsWin(
                "0尾,1尾,2尾,3尾,4尾,5尾,6尾,7尾,8尾,9尾",
                new[] { 1, 6, 9, 26, 19, 5, 24 });
            Assert.IsTrue(true);
        }
    }
}