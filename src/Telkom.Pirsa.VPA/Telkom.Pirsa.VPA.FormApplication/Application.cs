using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telkom.Pirsa.VPA.FormApplication.Controls;
using Telkom.Pirsa.VPA.Engine.ComputerVision.FaceRecognition;
using Telkom.Pirsa.VPA.Engine.Helper;
using System.Diagnostics;

namespace Telkom.Pirsa.VPA.FormApplication
{
    public partial class Application : Form
    {
        private UserControl activeControl = null;
        private bool loadVideoMode = false;
        private bool videoLoaded = false;
        private bool directoryBrowsed = false;
        private bool trainingDataReady = false;
        private Recognizer recognizer = null;
        private Setting.Setting appSetting;
        private System.Threading.AutoResetEvent autoEvent = new System.Threading.AutoResetEvent(false);
        public Application()
        {
            appSetting = GlobalVariable.ApplicationSetting;
            InitializeComponent();
            InitiateForm();
            InitializeRecognizer();
            
        }

        #region Helper
        private void InitilizeTimer()
        {
            var timer = new System.Threading.Timer(TimerTickElapsed, autoEvent, 500, 500);
            autoEvent.WaitOne();
            timer.Dispose();
        }

        private void DestroyTimer()
        {
            autoEvent.Set();
        
        }
        private void RefreshControl()
        {
            videoInfoGroup.Enabled = videoLoaded;
            captureButton.Enabled = videoLoaded;
            testButton.Enabled = videoLoaded && recognizer != null && recognizer.IsTrained;
            trainRecognizerMenu.Enabled = recognizer != null && trainingDataReady;
            trackBar1.Enabled = !loadVideoMode && directoryBrowsed;
            panel1.Enabled = !loadVideoMode && directoryBrowsed;

            saveRecognizerMenu.Enabled = recognizer != null;
        }

        private void UpdateVideoInfo(VideoProperty property)
        {
            if (videoLoaded)
            {
                nameLbl.Text = property.Name;
                sizeLbl.Text = property.Size.ToString("N2") + " MBs";
                frameRateLbl.Text = property.FrameRate.ToString("N1") + " f/s";
                totalFrameLbl.Text = property.TotalFrame.ToString();
                durationLbl.Text = property.Duration.ToString("N2") + " s";


            }
            else
            {
                nameLbl.Text = string.Empty;
                sizeLbl.Text = string.Empty;
                frameRateLbl.Text = string.Empty;
                totalFrameLbl.Text = "0";
                durationLbl.Text =string.Empty;
            }

            RefreshControl();
        }

        private void InitiateForm()
        {
            videoLoaded = false;
            loadVideoMode = true;
            directoryBrowsed = false;
            RefreshControl();
        }

        private void InitializeRecognizer()
        {
            var param = new RecognizerParameter(this.appSetting.RecognizerConfiguration);
            recognizer = new Recognizer(param);

            string[] files;
            trainingDataReady = recognizer.LoadData(out files);

        }
        #endregion

        private void LoadVideoClickAction(object sender, EventArgs e)
        {
            try
            {
                var dialogResult = videoLoadDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    urlTextBox.Text = videoLoadDialog.FileName;
                    var prop = VideoCapture.LoadVideo(urlTextBox.Text, appSetting);
                    videoLoaded = true;
                    UpdateVideoInfo(prop);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Application_Load(object sender, EventArgs e)
        {

        }

        private void GeneralSettingClickAction(object sender, EventArgs e)
        {
            activeControl = new GeneralSettingUI();
            ModalDialog dialog = new ModalDialog();
            dialog.Controls.Add(activeControl);
            activeControl.Dock = DockStyle.Fill;
            dialog.Text = "General Setting";
            dialog.ShowDialog();
            appSetting = GlobalVariable.ApplicationSetting;

        }

        private void RecognizerSettingClickAction(object sender, EventArgs e)
        {
            activeControl = new RecognizerSettingUI();
            ModalDialog dialog = new ModalDialog();
            dialog.Controls.Add(activeControl);
            activeControl.Dock = DockStyle.Fill;
            dialog.Text = "Recognizer Setting";
            dialog.ShowDialog();
            InitializeRecognizer();
            RefreshControl();
            appSetting = GlobalVariable.ApplicationSetting;
        }

        private void SaveRecognizerAction(object sender, EventArgs e)
        {
            MessageBox.Show("Recognizer succesfully saved!", "Operation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadRecognizerAction(object sender, EventArgs e)
        {
            recognizer = GlobalVariable.Recognizer;
            if (recognizer == null)
                InitializeRecognizer();
            MessageBox.Show("Recognizer succesfully loaded!", "Operation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshControl();
        }


        private void TrainRecognizerAction(object sender, EventArgs e)
        {
            try
            {
                Stopwatch watch = new Stopwatch();
                logPanel.AppendText("Training process begin at "  + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt") + "\n");
                watch.Start();
                recognizer.TrainModel();
                watch.Stop();
                logPanel.AppendText("Training process completed at " + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt") + "\n");
                logPanel.AppendText(string.Format("Elapsed time: {0} minutes {1} seconds {2} miliseconds\n", watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds));
                MessageBox.Show("Training process succesfully completed!", "Operation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private List<string> fileList;
        private void LoadTrainingDirectoryAction(object sender, EventArgs e)
        {
            string[] files;
            if (recognizer.LoadData(out files))
            {
                var totalImages = files.Length;
                trackBar1.Minimum = 0;
                trackBar1.Maximum = totalImages;
                trackBar1.Value = 0;
                frameText.Minimum = 0;
                frameText.Maximum = totalImages;
                frameText.Value = trackBar1.Value;
                totalFrameLbl.Text = totalImages.ToString();
                loadVideoMode = false;
                directoryBrowsed = true;
                videoLoaded = false;
                fileList = new List<string>(files);
                RefreshControl();
            }
            else
            {
                MessageBox.Show("Training directory still empty, please capture a video first!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadTestingDirectoryAction(object sender, EventArgs e)
        {
            string[] files;
            if (recognizer.LoadTestData(out files))
            {
                var totalImages = files.Length;
                trackBar1.Minimum = 0;
                trackBar1.Maximum = totalImages;
                trackBar1.Value = 0;
                frameText.Minimum = 0;
                frameText.Maximum = totalImages;
                frameText.Value = trackBar1.Value;
                totalFrameLbl.Text = totalImages.ToString();
                loadVideoMode = false;
                directoryBrowsed = true;
                videoLoaded = false;
                fileList = new List<string>(files);
                RefreshControl();
            }
            else
            {
                MessageBox.Show("Testing directory still empty, please capture a video first!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string name, url;
        private void CaptureVideoAction(object sender, EventArgs e)
        {
            try
            {
                if(videoLoaded && !string.IsNullOrEmpty(nameTextbox.Text))
                {
                    this.Enabled = false;
                    //var thread = new System.Threading.Thread(ExecuteCapture);
                    url = urlTextBox.Text;
                    name = nameTextbox.Text;
                    logPanel.Text = string.Format("Capturing {0} frames started at {1}", totalFrameLbl.Text, DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt"));
                    //InitilizeTimer();
                    //timer.Start();
                    ExecuteCapture();
                   // thread.Start();
                    //thread.Join();
                    
                }
                else
                    MessageBox.Show("Name of test subject still empty, please specify the name!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DestroyTimer();
            }
        }

        

        private void ExecuteCapture()
        {
            try
            {
                //System.Threading.Thread worker = new System.Threading.Thread(ShowLoader);
                //worker.Start();
                var captured = VideoCapture.Capture(url, name, appSetting, VideoCapture.CaptureOptions.CaptureAndSave, VideoCapture.CaptureSaveOption.SaveOriginalResized);
                //DestroyTimer();
                //worker.Join();
                MessageBox.Show(String.Format("{0} frames captured! All face images stored at {1}", captured, appSetting.TrainingImagesPath), "Operation Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ShowLoader()
        {
            activeControl = new LoggingControlUI();
            ModalDialog dialog = new ModalDialog();
            dialog.Controls.Add(activeControl);
            activeControl.Dock = DockStyle.Fill;
            dialog.Text = "Processing";
            dialog.Show();
        }

        private void TimerTickElapsed(Object stateInfo)
        {
            //AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            string[] files;
            recognizer.LoadData(out files);
            logPanel.Text += DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt") + " - " + ((files != null && files.Length > 0) ? files.Length + " images saved!\n" : "\n");
        }

        private Image UpdateImageContainer(int index)
        {
            try
            {
                if(index > 0 && fileList != null && fileList.Count >= index)
                    return Image.FromFile(fileList[index - 1]);

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ScrollImageBroserAction(object sender, EventArgs e)
        {
            frameText.Value = trackBar1.Value;
        }

        private void FrameNumberValueChanged(object sender, EventArgs e)
        {
            var value = (int)frameText.Value;
            trackBar1.Value = value;
            if (value > 0)
            {
                imageContainer.Image = UpdateImageContainer(value);
            }
        }

        private void DeleteImageAction(object sender, EventArgs e)
        {
            try
            {
                var value = (int)frameText.Value;
                var old = imageContainer.Image;
                var bmp = new Bitmap(old);
                old.Dispose();
                bmp.Dispose();
                imageContainer.Image = null;
                var target = string.Copy(fileList[value - 1]);
                fileList.Remove(target);
                System.IO.File.Delete(target);
                imageContainer.Image = UpdateImageContainer(value);
                trackBar1.Maximum = trackBar1.Maximum - 1;
                totalFrameLbl.Text = trackBar1.Maximum.ToString();
                frameText.Maximum = trackBar1.Maximum;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch watch = new Stopwatch();
                logPanel.AppendText("Testing process begin at " + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt") + "\n");
                watch.Start();
                recognizer.Tests(urlTextBox.Text, appSetting.TestImagesPath);
                watch.Stop();
                logPanel.AppendText("Testing process completed at " + DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt") + "\n");
                logPanel.AppendText(string.Format("Elapsed time: {0} minutes {1} seconds {2} miliseconds", watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds));
                MessageBox.Show("Training process succesfully completed!", "Operation Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
