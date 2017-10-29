using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Setting
{
    /// <summary>
    /// Represent eigen face recognizer parameter setting. Changes on its variable will require
    /// re-training the recognizer to take effect
    /// </summary>
    public class RecognizerSetting
    {
        /// <summary>
        /// Specifiy the maximum distance allowed to recognize face feature with eigenface data
        /// </summary>
        public double Threshold { set; get; }

        /// <summary>
        /// Specifiy where the classifier xml file located
        /// </summary>
        public string ClassifierPath { set; get; }

        /// <summary>
        /// Specifiy whether the recognizer will use histogram equalization or not
        /// </summary>
        public bool UseHistogramEqualizer { set; get; }

        /// <summary>
        /// Specify the minimum square window size in pixel used to scan whole image to find face region
        /// </summary>
        public int MinimumWindowSize { set; get; }

        /// <summary>
        /// Specify the minimum neighbour to execute distance calculation
        /// </summary>
        public int MinimumNeighbour { set; get; }

        /// <summary>
        /// Specify the scaling factor on PCA process. Closer value to 1 will run slower and produce better result 
        /// </summary>
        public double ScaleFactor { set; get; }

        /// <summary>
        /// Specify the number of principal. Must be positif number. More value will produce better result but set at around 80 will handle most cases.
        /// </summary>
        public int PrincipalComponent { set; get; }
    }
}
