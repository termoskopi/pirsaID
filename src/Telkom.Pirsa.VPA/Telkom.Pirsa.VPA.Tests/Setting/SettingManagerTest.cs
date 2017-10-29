using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telkom.Pirsa.VPA.Setting;

namespace Telkom.Pirsa.VPA.Tests.Setting
{
    [TestClass]
    public class SettingManagerTest
    {
        [TestMethod]
        public void SaveLoadSettingTest()
        {
            VPA.Setting.Setting cfg = new VPA.Setting.Setting()
            {
                TrainingImagesPath = @"E:\!Project\Telkom\resource\ImageTraining",
                Threshold = 3000,
                ModifiedDate = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                MaximumDuration = 300,
                MaximumFileSize = 500
                
            };

            bool saved = SettingManager.SaveToFile(cfg);
            Assert.IsTrue(saved);

            var loaded = SettingManager.ReadFromFile();

            Assert.AreEqual(cfg.TrainingImagesPath, loaded.TrainingImagesPath);
            Assert.AreEqual(cfg.Threshold, loaded.Threshold);
            Assert.AreEqual(cfg.ModifiedDate, loaded.ModifiedDate);
            Assert.AreEqual(cfg.MaximumDuration, loaded.MaximumDuration);
            Assert.AreEqual(cfg.MaximumFileSize, loaded.MaximumFileSize);

        }
    }
}
