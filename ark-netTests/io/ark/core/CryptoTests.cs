using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace io.ark.core.Tests
{
    [TestClass()]
    public class CryptoTests
    {
        [TestMethod()]
        public void GetKeysTest()
        {
           // Assert.AreSame(Crypto.getKeys("this is a top secret passphrase").PubKey,);                   
        }

        [TestMethod()]
        public void GetAddressTest()
        {
            String a1 = Crypto.GetAddress(Crypto.GetKeys("this is a top secret passphrase"));
            String a2 = "AGeYmgbg2LgGxRW2vNNJvQ88PknEJsYizC";

            Assert.AreEqual(a2,a1);
        }
    }
}