using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telkom.Pirsa.VPA.Setting;

namespace Telkom.Pirsa.VPA.Tests.Setting
{
    /// <summary>
    /// Summary description for IOManagerTest
    /// </summary>
    [TestClass]
    public class IOManagerTest
    {

        [TestMethod]
        public void CreateFileTest()
        {
            var path = "TestFile.data";
            using (var writer = IOManager.CreateFile(path))
            { 
            
            }
            var existingFile = System.IO.File.Exists(path);
            Assert.IsTrue(existingFile);
        }

        [TestMethod]
        public void WriteReadTest()
        {
            var path = "TestFile.data";
            var data = "Test data string\nThis is\nSpartaa!!";

            var saved = IOManager.Save(path, data);
            Assert.IsTrue(saved);
            var loaded = IOManager.Load(path);
            Assert.AreEqual(data, loaded);
            
        }
    }
}
