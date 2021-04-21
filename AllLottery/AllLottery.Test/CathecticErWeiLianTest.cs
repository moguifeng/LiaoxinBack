using AllLottery.Business.Cathectic.LiuHeCai.CathecticWeiShu;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllLottery.Test
{
    [TestClass]
    public class CathecticErWeiLianTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var l = new CathecticErWeiLian().IsWin(
                "0尾,1尾,2尾,3尾,4尾,5尾,6尾,7尾,8尾,9尾",
                new[] { 26, 49, 8, 23, 17, 41, 37 });
            Assert.IsTrue(true);
        }
    }
}