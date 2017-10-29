using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.Structure;
using System.Threading;
using System.Diagnostics;

namespace Telkom.Pirsa.VPA.ConsoleApplication
{
    class Program
    {
        static Stopwatch watch = new Stopwatch();
        static void Main(string[] args)
        {
            //CaptureMain();
            //TrainMain();
            //TestMain();
            var video = @"E:\Users\Rohmad Raharjo\Videos\Captures\muthiah2.mp4";
            double size = new FileInfo(video).Length;
            Console.WriteLine("Loading a video file {0} with size of {1} MB", video, (size/1024/1024).ToString("N2"));
            Console.ReadKey();

        }

        static void CaptureMain()
        {
            var video = @"E:\Users\Rohmad Raharjo\Videos\Captures\muthiah2.mp4";
            var name = "Muthiah";
            Console.WriteLine("Capturing a video file from {0} started!", video);

            watch.Start();
            var totalFrame = CaptureVideo(video, name);
            if (totalFrame > 0)
            {
                Console.WriteLine("Training subject {0} completed. {1} frames added!", name, totalFrame);
            }
            else
            {
                Console.WriteLine("Capture finished! no frame exists!");
            }
            watch.Stop();
            Console.WriteLine("Total Training time for {0} images: {1} minutes {2} seconds {3} miliseconds", totalFrame, watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds);
        }

        static void TrainMain()
        {
            watch.Start();
            Console.WriteLine("Training started!");
            var trainedCounter = 0;
            var eigenLabels = new List<string>();
            var eigenIds = new List<int>();
            var eigenTrainingImages = new List<Image<Gray, Byte>>();
            LoadDataset(out trainedCounter, ref eigenLabels, ref eigenIds, out eigenTrainingImages);
            Console.WriteLine("Dataset Loaded. Elapsed time: {0} minutes {1} seconds {2} miliseconds", watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds);
            var recognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);
            var images = eigenTrainingImages.ToArray();
            var ids = eigenIds.ToArray();
            Console.WriteLine("Dataset converted to arrays {3} x {4}. Elapsed time {0} minutes {1} seconds {2} miliseconds", watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds, images.Length, ids.Length);
            recognizer.Train(images, ids);
            Console.WriteLine("Recognizer trained. Elapsed time: {0} minutes {1} seconds {2} miliseconds", watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds);
            recognizer.Save(@"E:\!Project\Captured\trainedEigen.dataset");
            watch.Stop();
            Console.WriteLine("Eigen matrix saved. Completed in {0} minutes {1} seconds {2} miliseconds!", watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds);
        }

        static void TestMain()
        {
            var video = @"E:\Users\Rohmad Raharjo\Videos\Captures\test2.mp4";
            if (!Directory.Exists(@"E:\!Project\Captured\Log"))
            {
                Directory.CreateDirectory(@"E:\!Project\Captured\Log");
            }
            var trainedCounter = 0;
            var eigenLabels = new List<string>();
            var eigenIds = new List<int>();
            var eigenTrainingImages = new List<Image<Gray, Byte>>();
            LoadDataset(out trainedCounter, ref eigenLabels, ref eigenIds, out eigenTrainingImages);
            var recognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);

            var logpath = String.Format(@"E:\!Project\Captured\Log\TestLog-{0}.txt", DateTime.Now.ToString("yyyy, dd MMMM"));
            Log(logpath, String.Format("Logging started: {0}", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss tt")));
            recognizer.Load(@"E:\!Project\Captured\trainedEigen.dataset");
            Test(recognizer, video, logpath, eigenLabels);
        }


        static int CaptureVideo(string path, string name)
        {
            var tmp = @"E:\!Project\Captured";
            if (!Directory.Exists(tmp))
            {
                Directory.CreateDirectory(tmp);
            }

            using (Capture capture = new Capture(path))
            {
                var totalFrame = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_COUNT);
                var frame = 0;
                while (frame < totalFrame)
                {
                    using (var image = capture.QueryFrame())
                    {
                        if (image == null)
                            return -1;
                        var faces = DetectFace(image);
                        foreach (var face in faces)
                        {
                            image.ROI = face;
                            frame++;
                            var savedImage = SaveAndResize(image, 100, String.Format("{0}\\{1}-{2}.jpeg", tmp, name, DateTime.Now.Ticks));
                        }
                    }
                }
                return (int)totalFrame;
            }

        }

        static void Test(EigenFaceRecognizer recognizer, string srcpath, string logpath, List<string> eigenLabels)
        { 
            var tmp = @"E:\!Project\CapturedTest";
            if (!Directory.Exists(tmp))
            {
                Directory.CreateDirectory(tmp);
            }
            using (Capture capture = new Capture(srcpath))
            {
                var name = Path.GetFileName(srcpath);
                if (!Directory.Exists(tmp + "\\" + name))
                {
                    Directory.CreateDirectory(tmp + "\\" + name);
                }
                var totalFrame = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_COUNT);
                var size = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FORMAT);
                Log(logpath, String.Format("Loading video file with {0} frames with {1} format", totalFrame, size));
                var frame = 0;
                KeyValuePair<string, double> Max = new KeyValuePair<string, double>("", double.MinValue);
                while (frame < totalFrame)
                {
                    using (var image = capture.QueryFrame())
                    {
                        if (image == null)
                        {
                            Log(logpath, "Capturing finished!");
                            return;
                        }
                        using (var bmp = image.ToBitmap())
                        {
                            ++frame;
                            var path = string.Format("{0}\\{2}\\{1}.jpeg", tmp, frame, name);
                            Log(logpath, string.Format("Processing frame {0}", frame));
                            //bmp.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                            Log(logpath, String.Format("Frame {0} saved as {1}", frame, path));
                            var faces = DetectFace(image);
                            if(faces != null)
                                Log(logpath, "Detected faces: " + faces.Count);
                            var clonedImage = image.Copy();
                            var detected = false;
                            foreach (var face in faces)
                            {
                                image.ROI = face;
                                //var savedImage = SaveAndResize(image, 200, String.Format("{0}\\{1}-{2}.jpeg", tmp, frame, DateTime.Now.Ticks));
                                //Log(logpath, savedImage + " saved!");
                                //var loadedImage = new Bitmap(savedImage);
                                var savedImage = String.Format("{0}\\{1}-{2}.jpeg", tmp, frame, DateTime.Now.Ticks);
                                var target = image.Copy(face);
                                clonedImage.Draw(face, new Bgr(Color.Red), 3);
                                
                                var result = recognizer.Predict(target.Convert<Gray, Byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC));
                                if (result.Distance > Max.Value)
                                {
                                    Max = new KeyValuePair<string, double>(savedImage, result.Distance);
                                }
                                var font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_SIMPLEX, .75d, .75d);
                                if (result.Label < 0 || result.Distance > 4000)
                                {
                                    clonedImage.Draw("Unknown", ref font  , new Point(face.X - 5, face.Y - 5), new Bgr(Color.Red));
                                    Log(logpath, string.Format("Person on {0} is Unknown!", savedImage));
                                }
                                else
                                {
                                    detected = true;
                                    clonedImage.Draw(eigenLabels[result.Label], ref font, new Point(face.X - 2, face.Y - 2), new Bgr(Color.LightGreen));
                                    Log(logpath, string.Format("Person {0} is detected as {1}", savedImage, eigenLabels[result.Label]));
                                }
                            }
                            if(detected)
                                clonedImage.ToBitmap().Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }

                        Log(logpath, "====================================\n");
                        Log(logpath, string.Format("Maximum Distance ({0}) : {1}\n", Max.Key, Max.Value));
                    }
                }
                return;
            }
        }

        static void Log(string path, string data)
        {
            data += Environment.NewLine;
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                fs.Write(Encoding.Default.GetBytes(data), 0, data.Length);
                fs.Flush();
            }
        }

        static int Training(string path, string name)
        {
            var tmp = @"E:\!Project\Captured";
            if (!Directory.Exists(tmp))
            {
                Directory.CreateDirectory(tmp);
            }
            
            using (Capture capture = new Capture(path))
            {
                var totalFrame = capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_COUNT);
                var frame = 0;
                var eigenImages = new List<Image<Gray, Byte>>();
                var eigenLabels = new List<String>();
                var eigenIds = new List<int>();
                var trainedCounter = 0;

                LoadDataset(out trainedCounter, ref eigenLabels, ref eigenIds, out eigenImages);
                while (frame < totalFrame)
                {
                    using (var image = capture.QueryFrame())
                    {
                        if (image == null)
                            return -1;
                        var faces = DetectFace(image);
                        foreach (var face in faces)
                        {
                            image.ROI = face;
                            frame++;
                            var savedImage = SaveAndResize(image, 100, String.Format("{0}\\{1}-{2}.jpeg", tmp, name, DateTime.Now.Ticks));
                            Image<Bgr, Byte> trainedImage = new Image<Bgr, Byte>(savedImage);
                            //trainedImage._EqualizeHist();
                            eigenImages.Add(trainedImage.Convert<Gray, Byte>());
                            eigenLabels.Add(name);
                            eigenIds.Add(trainedCounter);
                            trainedCounter++;
                        }
                    }
                }
                Console.WriteLine("Elapsed time: {0} seconds", watch.ElapsedMilliseconds / 1000);
                Console.WriteLine("Begin generate eigen face dataset of {0} images", trainedCounter);
                var recognizer = new EigenFaceRecognizer(trainedCounter, 2000);
                recognizer.Train(eigenImages.Take(10).ToArray(), eigenIds.Take(10).ToArray());
                recognizer.Save(tmp + "\\trainedEigen.dataset");
                Console.WriteLine("Eigen dataset saved!");
                Console.WriteLine("Elapsed time: {0} seconds", watch.ElapsedMilliseconds / 1000);
                return (int)totalFrame;
            }
        }

        static string SaveAndResize(Image<Bgr, Byte> image, int desiredSize, string path)
        {
            image.Resize(desiredSize, desiredSize, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC).ToBitmap().Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            return path;
            //image.ToBitmap().Save(path);
        }

        static List<Rectangle> DetectFace(Image<Gray, Byte> input)
        {
            var classifierPath = @"classifier\haarcascade_frontalface_default.xml";
            var classifier = new CascadeClassifier(classifierPath);
            //input._EqualizeHist();
            Rectangle[] facesDetected = classifier.DetectMultiScale(
                   input,
                   1.1,
                   10,
                   new Size(50, 50),
                   Size.Empty);

            return new List<Rectangle>(facesDetected);
        }

        static List<Rectangle> DetectFace(Image<Bgr, Byte> input)
        {
            
            using (Image<Gray, Byte> gray = input.Convert<Gray, Byte>())
            {
                return DetectFace(gray);
            }

        }

        static void LoadDataset(out int trainedCounter, ref List<string> eigenLabels, ref List<int> eigenIds, out List<Image<Gray, Byte>> eigenTrainingImages)
        {
            string dataDirectory = @"E:\!Project\Captured";
            trainedCounter = 0;
            string[] files = Directory.GetFiles(dataDirectory, "*.jpeg", SearchOption.AllDirectories);
            var totalSet = files.Length;
            eigenTrainingImages = new List<Image<Gray, Byte>>();
            foreach (var file in files)
            {
                Image<Bgr, Byte> trainedImage = new Image<Bgr, Byte>(file);
                //trainedImage._EqualizeHist();
                eigenTrainingImages.Add(trainedImage.Convert<Gray, Byte>());
                eigenLabels.Add(GetLabel(file));
                eigenIds.Add(trainedCounter++);
            }
            
        }

        static string GetLabel(string filename)
        {
            return Path.GetFileName(filename).Split('-')[0];
        }
    }
}
