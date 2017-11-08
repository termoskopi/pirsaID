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
    public partial class RecognizerSettingUI : UserControl
    {
        private Setting.Setting appSetting = null;
        private Setting.RecognizerSetting recognizerSetting = null;

        public RecognizerSettingUI()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            try
            {
                this.appSetting = Setting.SettingManager.ReadFromFile();
                this.recognizerSetting = this.appSetting.RecognizerConfiguration;
                PlaceValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PlaceValues()
        {
            this.thresholdText.Text = this.recognizerSetting.Threshold.ToString("N2");
            this.minWindowText.Text = this.recognizerSetting.MinimumWindowSize.ToString("N0");
            this.minNeighbourText.Text = this.recognizerSetting.MinimumNeighbour.ToString("N0");
            this.scaleFactorText.Text = this.recognizerSetting.ScaleFactor.ToString("N3");
            this.pComponentText.Text = this.recognizerSetting.PrincipalComponent.ToString("N0");
            this.userHistogramCheckBox.Checked = this.recognizerSetting.UseHistogramEqualizer;
            this.recognizerPathText.Text = this.recognizerSetting.ClassifierPath;
        }

        private bool RetreiveValues()
        {
            try
            {
                double threshold, scaleFactor;
                int windowSize, minNeighbour, principalComponent;

                bool validated = Validate(out threshold, out windowSize, out minNeighbour, out scaleFactor, out principalComponent);

                if (validated)
                {
                    this.recognizerSetting = new Setting.RecognizerSetting()
                    {
                        Threshold = threshold,
                        MinimumWindowSize = windowSize,
                        MinimumNeighbour = minNeighbour,
                        ScaleFactor = scaleFactor,
                        PrincipalComponent = principalComponent,
                        UseHistogramEqualizer = userHistogramCheckBox.Checked,
                        ClassifierPath = recognizerPathText.Text
                    };
                    this.appSetting.RecognizerConfiguration = recognizerSetting;

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

        private bool Validate(out double threshold, out int windowSize, out int minNeighbour, out double scaleFactor, out int principalComponent)
        {
            try
            {
                threshold = double.Epsilon;
                windowSize = -1;
                minNeighbour = -1;
                scaleFactor = double.Epsilon;
                principalComponent = -1;

                if (!Validator.IsNumber(thresholdText.Text, out threshold))
                    return false;

                if (!Validator.IsInteger(minWindowText.Text, out windowSize))
                    return false;

                if (!Validator.IsInteger(minNeighbourText.Text, out minNeighbour))
                    return false;

                if (!Validator.IsNumber(scaleFactorText.Text, out scaleFactor))
                    return false;

                if (!Validator.IsInteger(pComponentText.Text, out principalComponent))
                    return false;

                if (string.IsNullOrEmpty(recognizerPathText.Text))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Action Handlers
        private void BrowseForClassifierFile(object sender, EventArgs e)
        {
            var dialogResult = browseRecognizerFile.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                recognizerPathText.Text = browseRecognizerFile.FileName;
            }
            else MessageBox.Show("File selection canceled!", "Operation Aborted", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetAction(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("You are going to reset recognizer setting. All unsaved changes will be lost.\n Are you sure?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
                InitializeData();
        }

        private void SaveFormAction(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("You are going to update recognizer setting.\nAre you sure?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                            MessageBox.Show("Recognizer setting succesfully updated!", "Operation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
