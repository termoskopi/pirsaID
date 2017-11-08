using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telkom.Pirsa.VPA.Engine.ComputerVision.FaceRecognition;


namespace Telkom.Pirsa.VPA.Engine.ComputerVision
{
    public class FeatureExtractor
    {
        public static List<Rectangle> DetectFace(Image<Gray, Byte> input, RecognizerParameter setting)
        {
            try
            {
                var classifier = new CascadeClassifier(setting.ClassifierPath);
                if (setting.IsUsingHistogram)
                    input._EqualizeHist();

                Rectangle[] facesDetected = classifier.DetectMultiScale(
                       input,
                       setting.ScaleFactor,
                       setting.MinimumNeighbour,
                       new Size(setting.MinimumWindowSize, setting.MinimumWindowSize),
                       Size.Empty);

                return new List<Rectangle>(facesDetected);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static List<Rectangle> DetectFace(Image<Bgr, Byte> input, RecognizerParameter setting)
        {
            try
            {
                using (Image<Gray, Byte> gray = input.Convert<Gray, Byte>())
                {
                    return DetectFace(gray, setting);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }
    }
}
