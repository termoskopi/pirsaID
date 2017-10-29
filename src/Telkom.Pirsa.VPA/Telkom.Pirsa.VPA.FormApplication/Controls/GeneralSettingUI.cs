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
    public partial class GeneralSettingUI : UserControl
    {
        private Setting.Setting appSetting = null;

        public GeneralSettingUI()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            this.appSetting = GlobalVariable.ApplicationSetting;
            PlaceValues();
        }

        private void PlaceValues()
        {
            this.sizeLimitText.Text = this.appSetting.MaximumFileSize.ToString("N0");
            this.durationText.Text = this.appSetting.MaximumDuration.ToString("N2");

        }

        private void RetreiveValues()
        { 
        
        }

        #region Action Handler

        private void BrowseCapturePathAction(object sender, EventArgs e)
        {

        }

        private void BrowseTestPathAction(object sender, EventArgs e)
        {

        }

        private void BrowseLogPathAction(object sender, EventArgs e)
        {

        }

        private void ResetFormAction(object sender, EventArgs e)
        {

        }

        private void SaveFormAction(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
