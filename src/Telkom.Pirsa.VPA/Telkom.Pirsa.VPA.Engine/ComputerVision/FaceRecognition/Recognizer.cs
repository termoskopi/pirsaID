using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using System.IO;
using Emgu.CV.Structure;
using Telkom.Pirsa.VPA.Engine.Helper;
using System.Drawing;

namespace Telkom.Pirsa.VPA.Engine.ComputerVision.FaceRecognition
{
    public class Recognizer
    {
        private RecognizerParameter parameter;
        private FaceRecognizer recognizer;
        private bool trained = false;
        private bool changed = false;
        private bool dataAvailable = false;
        private List<Image<Gray, Byte>> trainingData;
        private List<int> trainingIds;
        private List<RecognizerResult> trainedResults;
        
        public Recognizer(RecognizerParameter parameter)
        {
            this.parameter = parameter;
            Initialize();
        }

        private void Initialize()
        {
            recognizer = new EigenFaceRecognizer(parameter.PrincipalComponentCount, double.MaxValue);
        }

        private void ExecuteTraining(string[] paths)
        {
            try
            {
                this.trainingData = new List<Image<Gray, Byte>>();
                this.trainedResults = new List<RecognizerResult>();
                this.trainingIds = new List<int>();
                var trainedCount = 0;
                foreach (var path in paths)
                {
                    Image<Bgr, Byte> trainedImage = new Image<Bgr, Byte>(path);

                    if (parameter.IsUsingHistogram)
                    {
                        trainedImage._EqualizeHist();
                    }
                    this.trainingData.Add(trainedImage.Convert<Gray, Byte>());
                    this.trainedResults.Add(new RecognizerResult(double.MaxValue, GetLabel(path)));
                    this.trainingIds.Add(trainedCount++);


                }

                recognizer.Train(trainingData.ToArray(), trainingIds.ToArray());
                trained = true;
                recognizer.Save("trainedEigen.dataset");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetLabel(string filename)
        {
            return Path.GetFileName(filename).Split('-')[0];
        }

        public bool LoadData(out string[] files)
        {
            try
            {
                var setting = Setting.SettingManager.ReadFromFile();
                files = Directory.GetFiles(setting.TrainingImagesPath, "*." + setting.ImageExtension, SearchOption.AllDirectories);
                
                if (files == null || files.Length == 0)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public bool LoadTestData(out string[] files)
        {
            try
            {
                var setting = Setting.SettingManager.ReadFromFile();
                var ext = (setting.ImageExtension == "jpg") ? "jpeg" : setting.ImageExtension;
                files = Directory.GetFiles(setting.TestImagesPath, "*." + ext, SearchOption.AllDirectories);

                if (files == null || files.Length == 0)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public bool TrainModel()
        {
            string[] paths = null;
            var loaded = LoadData(out paths);
            if (loaded)
            {
                ExecuteTraining(paths);
                trained = true;
                changed = false;
            }

            return loaded;
        }

        public IList<string> Tests(string video, string targetLocation)
        {
            if (!Directory.Exists(targetLocation))
            {
                Directory.CreateDirectory(targetLocation);
            }
            IList<string> names = new List<string>();
            using (Capture capture = new Capture(video))
            {
                var name = Path.GetFileName(video);
                //if (!Directory.Exists(targetLocation + "\\" + name))
                //{
                //    Directory.CreateDirectory(targetLocation + "\\" + name);
                //}
                var totalFrame = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_COUNT);
                var size = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FORMAT);
                var frame = 0;
                while (frame < totalFrame)
                {
                    using (var image = capture.QueryFrame())
                    {
                        if (image == null)
                        {
                            throw new ArgumentException("Cannot retreive image from video!");
                        }
                        using (var bmp = image.ToBitmap())
                        {
                            ++frame;
                            var path = string.Format("{0}\\{2}-{1}.jpeg", targetLocation, frame, name);
                            var faces = FeatureExtractor.DetectFace(image, parameter);
                            var clonedImage = image.Copy();
                            var detected = false;
                            foreach (var face in faces)
                            {
                                image.ROI = face;
                                //var savedImage = String.Format("{0}\\{1}-{2}.jpeg", targetLocation, frame, DateTime.Now.Ticks);
                                var target = image.Copy(face);
                                clonedImage.Draw(face, new Bgr(Color.Red), 3);

                                var result = recognizer.Predict(target.Convert<Gray, Byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC));
                                var font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_SIMPLEX, .75d, .75d);
                                if (result.Label < 0 || result.Distance > parameter.Threshold)
                                {
                                    names.Add("Unknown");
                                    clonedImage.Draw("Unknown", ref font, new Point(face.X - 5, face.Y - 5), new Bgr(Color.Red));
                                }
                                else
                                {
                                    detected = true;
                                    names.Add(trainedResults[result.Label].Label);
                                    clonedImage.Draw(trainedResults[result.Label].Label, ref font, new Point(face.X - 2, face.Y - 2), new Bgr(Color.LightGreen));
                                }
                            }
                            if (detected)
                                clonedImage.ToBitmap().Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                }
                return names.Distinct().ToList();
            }
        }

        public void ParameterModified()
        {
            changed = true;
        }

        public bool IsTrained
        {
            get { return trained; }
        }

        public bool IsChanged
        {
            get { return changed; }
        }

        public bool IsDataAvalable
        {
            get { return dataAvailable; }
        }

    }
}
