using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArkNet.Tests
{
    [TestClass()]
    public class ArkNetApiTests
    {
        [TestMethod()]
        public void StartTest()
        {
             ArkNetApi.Start("33");

            Assert.Fail();
        }
    }
}