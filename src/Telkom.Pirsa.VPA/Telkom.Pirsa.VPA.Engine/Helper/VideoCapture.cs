using System;
using System.Collections.Generic;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Telkom.Pirsa.VPA.Setting;


namespace Telkom.Pirsa.VPA.Engine.Helper
{
    public class VideoCapture
    {
        #region Capture Options Enumeration
        public enum CaptureOptions
        { 
            CaptureOnly,
            CaptureAndSave
        }

        public enum CaptureSaveOption
        { 
            SaveOriginal,
            SaveGray,
            SaveOriginalResized,
            SaveGrayResized
        }

        #endregion

        /// <summary>
        /// Capture a video file and optionally save the images extracted from video
        /// </summary>
        /// <param name="path">The video path location</param>
        /// <param name="name">The name of person present on video</param>
        /// <param name="option"></param>
        /// <param name="saveOption"></param>
        /// <param name="capturePath"></param>
        /// <param name="preferredSize"></param>
        /// <returns></returns>
        public int Capture(string path, string name, CaptureOptions option, CaptureSaveOption saveOption, out List<Image<Bgr, Byte>> results)
        {

            if (!File.Exists(path))
                throw new ArgumentException("The specified file does not exist!");

            if (string.IsNullOrEmpty(name.Trim()))
                throw new ArgumentException("The parameter person name could not be empty!");

            Setting.Setting setting = SettingManager.ReadFromFile();

            double size = new FileInfo(path).Length; // size in bytes
            size = size / 1024 / 1024; // size in MBs

            if (size > setting.MaximumFileSize)
                throw new ArgumentException("The video file size exceeds maximum file setting!");
            using (Capture capture = new Capture(path))
            {
                double totalFrame = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_COUNT);
                var fps = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);
                var duration = Math.Ceiling(totalFrame/fps);
                if (duration > setting.MaximumDuration)
                    throw new ArgumentException("The video duration exceeds maximum duration setting!");

                results = new List<Image<Bgr, byte>>();
                int captured = 0;
                while (captured < totalFrame)
                {
                    using (var image = capture.QueryFrame())
                    {
                        if (image == null)
                            return -1;
                        var faces = FeatureExtractor.DetectFace(image);
                        foreach (var face in faces)
                        {
                            image.ROI = face;
                            captured++;
                            //var savedImage = SaveAndResize(image, setting.PreferredImageSize, String.Format("{0}\\{1}-{2}.jpeg", setting.TrainingImagesPath, name, DateTime.Now.Ticks));
                        }
                    }
                }


                return captured;
            }
        }


    }
}
