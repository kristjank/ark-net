using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace io.ark.core.Tests
{
    [TestClass()]
    public class CryptoTests
    {
        [TestMethod()]
        public void getKeysTest()
        {
           // Assert.AreSame(Crypto.getKeys("this is a top secret passphrase").PubKey,);
           

                
                
        }

        [TestMethod()]
        public void getAddressTest()
        {
            Assert.AreSame(Crypto.getAddress(Crypto.getKeys("this is a top secret passphrase")), "AGeYmgbg2LgGxRW2vNNJvQ88PknEJsYizC");
        }
    }
}