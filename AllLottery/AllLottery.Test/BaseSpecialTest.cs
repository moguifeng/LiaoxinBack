using AllLottery.Business.Cathectic.ShiShiCai.XinYongPan;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AllLottery.Test
{
    [TestClass]
    public class BaseSpecialTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var a = BaseSpecial.WinningValue(new[] { 6, 9, 8, 0, 0 }, 1);
            Assert.IsTrue(a == "顺子");
        }
    }
}