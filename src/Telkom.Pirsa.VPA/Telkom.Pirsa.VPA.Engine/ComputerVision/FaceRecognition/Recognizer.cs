using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;

namespace Telkom.Pirsa.VPA.Engine.ComputerVision.FaceRecognition
{
    public class Recognizer
    {
        private FaceRecognizer recognizer;


        public Recognizer()
        {
            Initialize();
        }

        private void Initialize()
        {
            //recognizer = new EigenFaceRecognizer();
        }

    }
}
