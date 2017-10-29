using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telkom.Pirsa.VPA.Setting;

namespace Telkom.Pirsa.VPA.Tests.Setting
{
    [TestClass]
    public class EncryptionTest
    {
        #region Positive Test
        [TestMethod]
        public void EncryptDecryptTest()
        {
            String plain = "This is my value";
            String encrypted = Encryption.Encrypt(plain);
            String decrypted = Encryption.Decrypt(encrypted);

            Assert.AreEqual(plain, decrypted);
        }

        #endregion


    }
}
