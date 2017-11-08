using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telkom.Pirsa.VPA.Engine.Helper;

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
            try
            {
                this.appSetting = GlobalVariable.ApplicationSetting;
                PlaceValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PlaceValues()
        {
            this.sizeLimitText.Text = this.appSetting.MaximumFileSize.ToString("N0");
            this.durationText.Text = this.appSetting.MaximumDuration.ToString("N2");
            this.preferreSizeText.Text = this.appSetting.PreferredImageSize.ToString("N0");
            this.capturePathText.Text = this.appSetting.TrainingImagesPath;
            this.testPathText.Text = this.appSetting.TestImagesPath;
            this.logPathText.Text = this.appSetting.LogPath;
            var format = this.appSetting.PreferredImageFormat;
            if (format == System.Drawing.Imaging.ImageFormat.Png)
                this.pngRadio.Checked = true;
            else if (format == System.Drawing.Imaging.ImageFormat.Bmp)
                this.bmpRadio.Checked = true;
            else
                this.jpegRadio.Checked = true;
        }

        private bool RetreiveValues()
        {
            try
            {
                int maxSize;
                double maxDuration;
                int imageSize;
                if (Validate(out maxSize, out maxDuration, out imageSize))
                {
                    this.appSetting.MaximumFileSize = maxSize;
                    this.appSetting.MaximumDuration = maxDuration;
                    this.appSetting.PreferredImageSize = imageSize;
                    this.appSetting.TrainingImagesPath = capturePathText.Text;
                    this.appSetting.TestImagesPath = testPathText.Text;
                    this.appSetting.LogPath = logPathText.Text;
                    var format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    if (pngRadio.Checked)
                        format = System.Drawing.Imaging.ImageFormat.Png;
                    else if (bmpRadio.Checked)
                        format = System.Drawing.Imaging.ImageFormat.Bmp;
                    this.appSetting.PreferredImageFormat = format;
                    return true;
                }
                else
                    MessageBox.Show("Setting form must be correctly filled!", "Operation Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool Validate(out int maxSize, out double maxDuration, out int imageSize)
        {
            try
            {
                maxSize = 0;
                maxDuration = 0;
                imageSize = 0;

                if (!Validator.IsInteger(sizeLimitText.Text, out maxSize))
                    return false;

                if (!Validator.IsNumber(durationText.Text, out maxDuration))
                    return false;

                if (!Validator.IsInteger(preferreSizeText.Text, out imageSize))
                    return false;

                if (string.IsNullOrEmpty(capturePathText.Text) ||
                    string.IsNullOrEmpty(testPathText.Text) ||
                    string.IsNullOrEmpty(logPathText.Text))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Action Handler

        private void BrowseCapturePathAction(object sender, EventArgs e)
        {
            if (browseFolderDialog.ShowDialog() == DialogResult.OK)
            {
                capturePathText.Text = browseFolderDialog.SelectedPath;
            }
            else MessageBox.Show("Directory selection canceled!", "Operation Aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BrowseTestPathAction(object sender, EventArgs e)
        {
            if (browseFolderDialog.ShowDialog() == DialogResult.OK)
            {
                testPathText.Text = browseFolderDialog.SelectedPath;
            }
            else MessageBox.Show("Directory selection canceled!", "Operation Aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BrowseLogPathAction(object sender, EventArgs e)
        {
            if (browseFolderDialog.ShowDialog() == DialogResult.OK)
            {
                logPathText.Text = browseFolderDialog.SelectedPath;
            }
            else MessageBox.Show("Directory selection canceled!", "Operation Aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetFormAction(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("You are going to reset application setting. All unsaved changes will be lost.\n Are you sure?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
                InitializeData();
        }

        private void SaveFormAction(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("You are going to update application setting.\nAre you sure?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    bool validated = RetreiveValues();
                    if (validated)
                    {
                        bool result = Setting.SettingManager.SaveToFile(this.appSetting);
                        if (result)
                        {
                            MessageBox.Show("Application setting succesfully updated!", "Operation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.ParentForm.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}
