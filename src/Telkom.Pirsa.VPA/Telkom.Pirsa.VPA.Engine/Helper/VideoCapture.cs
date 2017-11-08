using System;
using System.Collections.Generic;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Telkom.Pirsa.VPA.Setting;
using Telkom.Pirsa.VPA.Engine.ComputerVision;
using Telkom.Pirsa.VPA.Engine.ComputerVision.FaceRecognition;

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
        /// <param name="option">The capture option parameter, enumeration.</param>
        /// <param name="saveOption">The capture save option parameter, if capture option set to CaptureAndSave, enumeration </param>
        /// <returns></returns>
        public static int Capture(string path, string name, Setting.Setting setting, CaptureOptions option, CaptureSaveOption saveOption)
        {

            try
            {
                if (!File.Exists(path))
                    throw new ArgumentException("The specified file does not exist!");

                if (string.IsNullOrEmpty(name.Trim()))
                    throw new ArgumentException("The parameter person name could not be empty!");

                double size = new FileInfo(path).Length; // size in bytes
                size = size / 1024 / 1024; // size in MBs

                if (size > setting.MaximumFileSize)
                    throw new ArgumentException("The video file size exceeds maximum file setting!");

                using (Capture capture = new Capture(path))
                {
                    double totalFrame = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_COUNT);
                    var fps = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);
                    var duration = Math.Ceiling(totalFrame / fps);
                    if (duration > setting.MaximumDuration)
                        throw new ArgumentException("The video duration exceeds maximum duration setting!");

                    int captured = 0;
                    RecognizerParameter param = new RecognizerParameter(setting.RecognizerConfiguration);
                    while (captured < totalFrame)
                    {
                        using (var image = capture.QueryFrame())
                        {
                            if (image == null)
                                return -1;
                            var faces = FeatureExtractor.DetectFace(image, param);
                            foreach (var face in faces)
                            {
                                image.ROI = face;
                                captured++;
                                if (option == CaptureOptions.CaptureAndSave)
                                {
                                    SaveAndResize(image, setting, saveOption, name);
                                }
                            }
                        }
                    }


                    return captured;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static string SaveAndResize(Image<Bgr, Byte> image, Setting.Setting setting, CaptureSaveOption option, string name)
        {
            try
            {
                Image<Bgr, Byte> imageToSave = null;
                Image<Gray, Byte> imageGray = null;
                var resize = false;
                switch (option)
                {
                    case CaptureSaveOption.SaveGray:
                        imageGray = image.Convert<Gray, Byte>();
                        break;
                    case CaptureSaveOption.SaveGrayResized:
                        imageGray = image.Convert<Gray, Byte>().Resize(setting.PreferredImageSize, setting.PreferredImageSize, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                        break;
                    case CaptureSaveOption.SaveOriginal:
                        imageToSave = image;
                        break;
                    case CaptureSaveOption.SaveOriginalResized:
                        imageToSave = image.Resize(setting.PreferredImageSize, setting.PreferredImageSize, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                        break;
                }
                var path = String.Format("{0}\\{1}-{2}.{3}", setting.TrainingImagesPath, name, DateTime.Now.Ticks, setting.ImageExtension.ToLower());
                if (imageToSave != null)
                    imageToSave.ToBitmap().Save(path, setting.PreferredImageFormat);
                else
                    imageGray.ToBitmap().Save(path, setting.PreferredImageFormat);

                return path;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static VideoProperty LoadVideo(string path, Setting.Setting config)
        {
            try
            {
                if (!File.Exists(path))
                    throw new ArgumentException("The specified file does not exist!");

                double size = new FileInfo(path).Length; // size in bytes
                size = size / 1024 / 1024; // size in MBs

                if (size > config.MaximumFileSize)
                    throw new ArgumentException("The video file size exceeds maximum file setting!");

                using (Capture capture = new Capture(path))
                {
                    double totalFrame = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_COUNT);
                    var fps = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);
                    var duration = Math.Ceiling(totalFrame / fps);
                    if (duration > config.MaximumDuration)
                        throw new ArgumentException("The video duration exceeds maximum duration setting!");

                    return new VideoProperty()
                    {
                        Name = Path.GetFileName(path),
                        Duration = duration,
                        FrameRate = fps,
                        Size = size,
                        TotalFrame = (int)totalFrame
                    };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
