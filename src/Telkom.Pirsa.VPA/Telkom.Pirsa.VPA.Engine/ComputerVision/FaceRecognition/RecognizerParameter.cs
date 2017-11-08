using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Engine.ComputerVision.FaceRecognition
{
    public class RecognizerParameter
    {
        private Double threshold;
        private int windowSize;
        private int neighbour;
        private double scaleFactor;
        private int principalComponent;
        private string classifierFilePath;
        private bool useHistogram;

        public RecognizerParameter(Setting.RecognizerSetting setting)
        {
            try
            {
                Initialize(setting);
            }
            catch(Exception ex)
            {
                throw Setting.ExceptionHandling.GenerateException("Recognizer parameter initialization failed!", ex);
            }
            
        }

        public void Initialize(Setting.RecognizerSetting setting)
        {
            try
            {
                SetInitialParamater(setting.Threshold, setting.PrincipalComponent);
                SetFaceRegionParameter(setting.ScaleFactor, setting.MinimumNeighbour, setting.MinimumWindowSize, setting.UseHistogramEqualizer);
                SetClassifierPath(setting.ClassifierPath);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void SetInitialParamater(double threshold, int principalComponent)
        {
            this.threshold = threshold;
            this.principalComponent = principalComponent;
        }

        private void SetFaceRegionParameter(double scaleFactor, int minNeighbour, int minWindowSize, bool useHistogram)
        {
            this.scaleFactor = scaleFactor;
            this.neighbour = minNeighbour;
            this.windowSize = minWindowSize;
            this.useHistogram = useHistogram;
        }

        private void SetClassifierPath(string path)
        {
            if (!System.IO.File.Exists(path))
                throw new ArgumentException("The specified file doesn't exist!");
            classifierFilePath = path;
        }

        public bool IsUsingHistogram 
        {
            get { return this.useHistogram; }
        }

        public double ScaleFactor
        {
            get { return this.scaleFactor; }
        }

        public double Threshold
        {
            get { return this.threshold; }
        }

        public int PrincipalComponentCount
        {
            get { return this.principalComponent; }
        }

        public int MinimumNeighbour
        {
            get { return this.neighbour; }
        }

        public int MinimumWindowSize
        {
            get { return this.windowSize; }
        }

        public string ClassifierPath
        {
            get { return this.classifierFilePath; }
        }
    }

}
