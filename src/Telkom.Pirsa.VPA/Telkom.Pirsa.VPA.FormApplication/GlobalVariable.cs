using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.FormApplication
{
    public class GlobalVariable
    {
        private static  VPA.Engine.ComputerVision.FaceRecognition.Recognizer recognizer = null;

        public static void SaveRecognizer(VPA.Engine.ComputerVision.FaceRecognition.Recognizer recognizer)
        {
            GlobalVariable.recognizer = recognizer;
        }


        public static Setting.Setting ApplicationSetting
        {
            get
            {
                return Setting.SettingManager.ReadFromFile();
            }
        }

        public static VPA.Engine.ComputerVision.FaceRecognition.Recognizer Recognizer
        {
            get
            {
                return GlobalVariable.recognizer;
            }
        }
    }
}
