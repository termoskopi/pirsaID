using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.FormApplication
{
    public class GlobalVariable
    {
        public static Setting.Setting ApplicationSetting
        {
            get
            {
                return Setting.SettingManager.ReadFromFile();
            }
        }
    }
}
