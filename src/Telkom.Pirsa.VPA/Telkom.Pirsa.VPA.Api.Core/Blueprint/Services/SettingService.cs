using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Telkom.Pirsa.VPA.Setting;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
    public class SettingService
    {
        private readonly DataAccessManager _manager; 

        public SettingService()
        {
            throw new Exception("This default constructor has been disbaled. All Service instance only generated on ApplicationServiceBuilder");
        }

        public SettingService(DataAccessManager dataAccess)
        {
            _manager = dataAccess;
        }

        public JObject LoadSetting()
        {
            try
            {
                var sett = SettingManager.ReadFromFile();
                return ToJson(sett);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public bool SaveSetting(JObject setting)
        {
            try
            {
                var sett = ToSettingInstance(setting);
                return SettingManager.SaveToFile(sett);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Mapper

        private JObject ToJson(Setting.Setting setting)
        {
            try
            {
                var settingObj = new JObject();
                settingObj.Add(new JProperty("ImageExtension", setting.ImageExtension));
                settingObj.Add(new JProperty("MaximumDuration", setting.MaximumDuration));
                settingObj.Add(new JProperty("MaximumFileSize", setting.MaximumFileSize));
                settingObj.Add(new JProperty("ModifiedDate", setting.ModifiedDate));
                settingObj.Add(new JProperty("PreferredImageFormat", setting.PreferredImageFormat.ToString()));
                settingObj.Add(new JProperty("PreferredImageSize", setting.PreferredImageSize));
                settingObj.Add(new JProperty("TrainingImagesPath", setting.TrainingImagesPath));
                settingObj.Add(new JProperty("TestImagesPath", setting.TestImagesPath));
                settingObj.Add(new JProperty("LogPath", setting.LogPath));
                settingObj.Add(new JProperty("RecognizerConfiguration", ToJson(setting.RecognizerConfiguration)));

                return settingObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private JObject ToJson(RecognizerSetting setting)
        {
            try
            {
                var settingObj = new JObject();
                settingObj.Add(new JProperty("ClassifierPath", setting.ClassifierPath));
                settingObj.Add(new JProperty("MinimumNeighbour", setting.MinimumNeighbour));
                settingObj.Add(new JProperty("MinimumWindowSize", setting.MinimumWindowSize));
                settingObj.Add(new JProperty("PrincipalComponent", setting.PrincipalComponent));
                settingObj.Add(new JProperty("ScaleFactor", setting.ScaleFactor));
                settingObj.Add(new JProperty("Threshold", setting.Threshold));
                settingObj.Add(new JProperty("UseHistogramEqualizer", setting.UseHistogramEqualizer));

                return settingObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Setting.Setting ToSettingInstance(JObject obj)
        {
            try
            {
                var setting = SettingManager.ReadFromFile();
                var preferredFormat = GetStringValue(obj, "ImageExtension", setting.ImageExtension);
                System.Drawing.Imaging.ImageFormat format = preferredFormat.Trim().ToLower() == "png" ? System.Drawing.Imaging.ImageFormat.Png :
                    preferredFormat.Trim().ToLower() == "bmp" ? System.Drawing.Imaging.ImageFormat.Bmp : System.Drawing.Imaging.ImageFormat.Jpeg;
                setting.PreferredImageFormat = format;
                setting.MaximumDuration = GetDoubleValue(obj, "MaximumDuration", setting.MaximumDuration);
                setting.MaximumFileSize = GetIntValue(obj, "MaximumFileSize", setting.MaximumFileSize);
                setting.PreferredImageSize = GetIntValue(obj, "PreferredImageSize", setting.PreferredImageSize);
                setting.TrainingImagesPath = GetStringValue(obj, "TrainingImagesPath", setting.TrainingImagesPath);
                setting.TestImagesPath = GetStringValue(obj, "TestImagesPath", setting.TestImagesPath);
                setting.LogPath = GetStringValue(obj, "LogPath", setting.LogPath);

                var recognizerSetting = setting.RecognizerConfiguration;
                recognizerSetting.ClassifierPath = GetStringValue(obj, "ClassifierPath", recognizerSetting.ClassifierPath);
                recognizerSetting.MinimumNeighbour = GetIntValue(obj, "MinimumNeighbour", recognizerSetting.MinimumNeighbour);
                recognizerSetting.MinimumWindowSize = GetIntValue(obj, "MinimumWindowSize", recognizerSetting.MinimumWindowSize);
                recognizerSetting.PrincipalComponent = GetIntValue(obj, "PrincipalComponent", recognizerSetting.PrincipalComponent);
                recognizerSetting.ScaleFactor = GetDoubleValue(obj, "ScaleFactor", recognizerSetting.ScaleFactor);
                recognizerSetting.Threshold = GetDoubleValue(obj, "Threshold", recognizerSetting.Threshold);
                recognizerSetting.UseHistogramEqualizer = GetBooleanValue(obj, "UseHistogramEqualizer", recognizerSetting.UseHistogramEqualizer);
                setting.RecognizerConfiguration = recognizerSetting;

                return setting;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool PropertyExists(JObject source, string propName)
        {
            try
            {
                return source[propName] != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetStringValue(JObject obj, string propName, string original)
        {
            try
            {
                if (PropertyExists(obj, propName))
                    return Convert.ToString(obj[propName]);
                return original;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetIntValue(JObject obj, string propName, int original)
        {
            try
            {
                if (PropertyExists(obj, propName))
                    return Convert.ToInt32(obj[propName]);
                return original;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private double GetDoubleValue(JObject obj, string propName, double original)
        {
            try
            {
                if (PropertyExists(obj, propName))
                    return Convert.ToDouble(obj[propName]);
                return original;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool GetBooleanValue(JObject obj, string propName, bool original)
        {
            try
            {
                if (PropertyExists(obj, propName))
                    return Convert.ToBoolean(obj[propName]);
                return original;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
