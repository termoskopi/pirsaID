using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Engine.Helper
{
    public class VideoProperty
    {
        public string Name { set; get; }
        public int TotalFrame { set; get; }
        public double FrameRate { set; get; }
        public double Duration { set; get; }
        public double Size { set; get; }
    }
}
