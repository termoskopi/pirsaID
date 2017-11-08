using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Engine.ComputerVision.FaceRecognition
{
    public class RecognizerResult
    {
        private double distance;
        private string label;


        public RecognizerResult(double distance = double.MaxValue, string label = null)
        {
            this.distance = distance;
            this.label = label;
        }

        public void UpdateDistance(double value)
        {
            this.distance = value;
        }

        public double Similarity 
        {
            get 
            {
                if (distance - double.Epsilon < 0)
                    distance = double.MaxValue;

                return 1.0 / distance; 
            } 
        }

        public string Label
        {
            get
            {
                return label;
            }
        }

        public double Distance
        {
            get
            {
                return distance;
            }
        }

        
    }
}
