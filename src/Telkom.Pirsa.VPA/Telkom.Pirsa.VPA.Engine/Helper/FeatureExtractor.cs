using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telkom.Pirsa.VPA.Setting;

namespace Telkom.Pirsa.VPA.Engine.Helper
{
    public class FeatureExtractor
    {
        public static List<Rectangle> DetectFace(Image<Gray, Byte> input)
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

        public static List<Rectangle> DetectFace(Image<Bgr, Byte> input)
        {

            using (Image<Gray, Byte> gray = input.Convert<Gray, Byte>())
            {
                return DetectFace(gray);
            }

        }
    }
}
