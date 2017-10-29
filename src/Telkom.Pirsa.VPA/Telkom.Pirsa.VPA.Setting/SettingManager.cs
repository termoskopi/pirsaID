using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telkom.Pirsa.VPA.Setting
{
    public class SettingManager
    {
        private static string FILENAME = "application.setting";

        private static string Parse(Setting content)
        {
            try
            {
                if (content == null)
                {
                    throw new ArgumentException("The setting object is required!");
                }
                StringBuilder builder = new StringBuilder();
                /* **
                 * Format (line by line): 
                 * Date modified 
                 * Maximum Size
                 * Maximum Duration
                 * Preferred Size
                 * Image Extension
                 * Image Training Path
                 * Image Test Path
                 * Image Log Path
                 * Threshold
                 * Windows Size
                 * Scale Factor
                 * Principal Component
                 * Use Histogram Equalizer
                 * Recognizer Path
                 * New Line
                 **  */
                builder
                    .AppendFormat("{0:dddd, dd MMMM yyyy hh:mm tt}", DateTime.Now).AppendLine()
                    .Append(content.MaximumFileSize).AppendLine()
                    .Append(content.MaximumDuration).AppendLine()
                    .Append(content.PreferredImageSize).AppendLine()
                    .Append(content.ImageExtension).AppendLine()
                    .Append(content.TrainingImagesPath).AppendLine()
                    .Append(content.TestImagesPath).AppendLine()
                    .Append(content.LogPath).AppendLine()
                    .Append(content.RecognizerConfiguration.Threshold).AppendLine()
                    .Append(content.RecognizerConfiguration.MinimumWindowSize).AppendLine()
                    .Append(content.RecognizerConfiguration.ScaleFactor).AppendLine()
                    .Append(content.RecognizerConfiguration.PrincipalComponent).AppendLine()
                    .Append(content.RecognizerConfiguration.UseHistogramEqualizer).AppendLine()
                    .Append(content.RecognizerConfiguration.ClassifierPath).AppendLine()
                    .Append(content.RecognizerConfiguration.MinimumNeighbour).AppendLine()
                    .AppendLine();

                return builder.ToString();
            }
            catch (Exception ex)
            {
                throw ExceptionHandling.GenerateException("Cannot parse specific setting object to a formatted string!", ex);
            }
        }

        private static Setting Encapsulate(string content)
        {
            try
            {
                if(string.IsNullOrEmpty(content))
                {
                    throw new ArgumentException("Cannot generate setting object from null or empty string!");
                }

                string[] contents = content.Split('\n');
                if (contents == null || content.Length < 15)
                {
                    throw new ArgumentException("The specified string must be correctly formatted!");
                }
                var date = contents[0].Trim();
                var maximumSize = 0;
                if (!int.TryParse(contents[1].Trim(), out maximumSize))
                {
                    throw new ArgumentException("The maximum size value must be an integer!");
                }
                var maximumDuration = 0.0;
                if (!double.TryParse(contents[2].Trim(), out maximumDuration))
                {
                    throw new ArgumentException("The maximum duration value must be a number!");
                }
                var imageSize = 0;
                if (!int.TryParse(contents[3].Trim(), out imageSize))
                {
                    throw new ArgumentException("The maximum size value must be an integer!");
                }
                var ext = contents[4].Trim().ToUpper();
                var format = System.Drawing.Imaging.ImageFormat.Jpeg;
                if(ext == "PNG")
                    format = System.Drawing.Imaging.ImageFormat.Png;
                else if(ext == "BMP")
                    format = System.Drawing.Imaging.ImageFormat.Bmp;
                var trainingPath = contents[5];
                var testPath = contents[6];
                var logPath = contents[7];
                var threshold = 0.0;
                if (!double.TryParse(contents[8].Trim(), out threshold))
                {
                    throw new ArgumentException("The threshold value must be a number!");
                }
                var windowSize = 0;
                if (!int.TryParse(contents[9].Trim(), out windowSize))
                {
                    throw new ArgumentException("The maximum size value must be an integer!");
                }
                var scaleFactor = 0.0;
                if (!double.TryParse(contents[10].Trim(), out scaleFactor))
                {
                    throw new ArgumentException("The maximum size value must be a number!");
                }
                var principalComponent = 0;
                if (!int.TryParse(contents[11].Trim(), out principalComponent))
                {
                    throw new ArgumentException("The maximum size value must be an integer!");
                }
                var useHistogram = contents[12];
                var classiferPath = contents[13];
                var minNeighbour = 0;
                if (!int.TryParse(contents[14].Trim(), out minNeighbour))
                {
                    throw new ArgumentException("The maximum size value must be an integer!");
                }

                return new Setting()
                {
                    ModifiedDate = date,
                    MaximumFileSize = maximumSize,
                    MaximumDuration = maximumDuration,
                    PreferredImageSize = imageSize,
                    PreferredImageFormat = format,
                    TrainingImagesPath = trainingPath,
                    TestImagesPath = testPath,
                    LogPath = logPath,
                    RecognizerConfiguration = new RecognizerSetting() 
                    { 
                        Threshold = threshold,
                        MinimumWindowSize = windowSize,
                        ScaleFactor = scaleFactor,
                        PrincipalComponent = principalComponent,
                        UseHistogramEqualizer = useHistogram.ToLower() == "true" || useHistogram == "1",
                        ClassifierPath = classiferPath,
                        MinimumNeighbour = minNeighbour
                    }
                };

            }
            catch (Exception ex)
            {
                throw ExceptionHandling.GenerateException("Cannot encapsulate specific formatted string to a setting object!", ex);
            }
        }
        
        private static void Initialize()
        {
            SaveToFile(new Setting());
        }

        public static Setting ReadFromFile()
        {
            try
            {
                if (!System.IO.File.Exists(FILENAME))
                    Initialize();

                string cfg = IOManager.Load(FILENAME);
                if (string.IsNullOrEmpty(cfg))
                {
                    throw new Exception("Not a valid or externally modified setting file!");
                }
                return Encapsulate(cfg);
            }
            catch (Exception ex)
            {
                throw ExceptionHandling.GenerateException("Cannot read setting file!", ex);
            }
        }

        public static bool SaveToFile(Setting content)
        {
            try
            {
                string cfg = Parse(content);
                if(string.IsNullOrEmpty(cfg))
                {
                    throw new ArgumentException("Not valid setting file!");
                }
                return IOManager.Save(FILENAME, cfg);
            }
            catch (Exception ex)
            {
                throw ExceptionHandling.GenerateException("Cannot save setting file!", ex);
            }
        }

    }
}
