using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Telkom.Pirsa.VPA.FormApplication.Controls
{
    public partial class LoggingControlUI : UserControl
    {
        public LoggingControlUI()
        {
            InitializeComponent();
        }


        private void RunLogging()
        {
            var timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Start();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            logBox.AppendText("Logging events: " + e.SignalTime.ToString("hh:mm:ss tt"));
            logBox.AppendText(Environment.NewLine);

        }
    }
}
