namespace Telkom.Pirsa.VPA.FormApplication
{
    partial class Application
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenuControl = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainingDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recognizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRecognizerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainRecognizerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recognizerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.imageContainer = new System.Windows.Forms.PictureBox();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.seekerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.totalFrameLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.frameText = new System.Windows.Forms.NumericUpDown();
            this.captureButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nameTextbox = new System.Windows.Forms.TextBox();
            this.videoInfoGroup = new System.Windows.Forms.GroupBox();
            this.durationLbl = new System.Windows.Forms.Label();
            this.frameRateLbl = new System.Windows.Forms.Label();
            this.sizeLbl = new System.Windows.Forms.Label();
            this.nameLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.logPanel = new System.Windows.Forms.RichTextBox();
            this.videoLoadDialog = new System.Windows.Forms.OpenFileDialog();
            this.deleteButton = new System.Windows.Forms.Button();
            this.mainMenuControl.SuspendLayout();
            this.mainLayout.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).BeginInit();
            this.bottomPanel.SuspendLayout();
            this.seekerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameText)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.videoInfoGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuControl
            // 
            this.mainMenuControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.recognizerToolStripMenuItem,
            this.settingToolStripMenuItem});
            this.mainMenuControl.Location = new System.Drawing.Point(0, 0);
            this.mainMenuControl.Name = "mainMenuControl";
            this.mainMenuControl.Size = new System.Drawing.Size(977, 24);
            this.mainMenuControl.TabIndex = 0;
            this.mainMenuControl.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadVideoToolStripMenuItem,
            this.informationToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.ToolTipText = "Manage Video Files";
            // 
            // loadVideoToolStripMenuItem
            // 
            this.loadVideoToolStripMenuItem.Name = "loadVideoToolStripMenuItem";
            this.loadVideoToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.loadVideoToolStripMenuItem.Text = "Load Video";
            this.loadVideoToolStripMenuItem.Click += new System.EventHandler(this.LoadVideoClickAction);
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trainingDirectoryToolStripMenuItem,
            this.testDirectoryToolStripMenuItem});
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.informationToolStripMenuItem.Text = "Load Directory";
            // 
            // trainingDirectoryToolStripMenuItem
            // 
            this.trainingDirectoryToolStripMenuItem.Name = "trainingDirectoryToolStripMenuItem";
            this.trainingDirectoryToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.trainingDirectoryToolStripMenuItem.Text = "Training Directory";
            this.trainingDirectoryToolStripMenuItem.Click += new System.EventHandler(this.LoadTrainingDirectoryAction);
            // 
            // testDirectoryToolStripMenuItem
            // 
            this.testDirectoryToolStripMenuItem.Name = "testDirectoryToolStripMenuItem";
            this.testDirectoryToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.testDirectoryToolStripMenuItem.Text = "Test Directory";
            this.testDirectoryToolStripMenuItem.Click += new System.EventHandler(this.LoadTestingDirectoryAction);
            // 
            // recognizerToolStripMenuItem
            // 
            this.recognizerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveRecognizerMenu,
            this.loadToolStripMenuItem,
            this.trainRecognizerMenu});
            this.recognizerToolStripMenuItem.Name = "recognizerToolStripMenuItem";
            this.recognizerToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.recognizerToolStripMenuItem.Text = "Recognizer";
            // 
            // saveRecognizerMenu
            // 
            this.saveRecognizerMenu.Name = "saveRecognizerMenu";
            this.saveRecognizerMenu.Size = new System.Drawing.Size(100, 22);
            this.saveRecognizerMenu.Text = "Save";
            this.saveRecognizerMenu.Click += new System.EventHandler(this.SaveRecognizerAction);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadRecognizerAction);
            // 
            // trainRecognizerMenu
            // 
            this.trainRecognizerMenu.Name = "trainRecognizerMenu";
            this.trainRecognizerMenu.Size = new System.Drawing.Size(100, 22);
            this.trainRecognizerMenu.Text = "Train";
            this.trainRecognizerMenu.Click += new System.EventHandler(this.TrainRecognizerAction);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalToolStripMenuItem,
            this.recognizerToolStripMenuItem1});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.ToolTipText = "Managa Application Setting";
            // 
            // generalToolStripMenuItem
            // 
            this.generalToolStripMenuItem.Name = "generalToolStripMenuItem";
            this.generalToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.generalToolStripMenuItem.Text = "General";
            this.generalToolStripMenuItem.Click += new System.EventHandler(this.GeneralSettingClickAction);
            // 
            // recognizerToolStripMenuItem1
            // 
            this.recognizerToolStripMenuItem1.Name = "recognizerToolStripMenuItem1";
            this.recognizerToolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.recognizerToolStripMenuItem1.Text = "Recognizer";
            this.recognizerToolStripMenuItem1.Click += new System.EventHandler(this.RecognizerSettingClickAction);
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 10;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.mainLayout.Controls.Add(this.mainPanel, 1, 1);
            this.mainLayout.Controls.Add(this.bottomPanel, 1, 4);
            this.mainLayout.Controls.Add(this.videoInfoGroup, 6, 1);
            this.mainLayout.Controls.Add(this.logPanel, 6, 2);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 24);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 6;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayout.Size = new System.Drawing.Size(977, 548);
            this.mainLayout.TabIndex = 1;
            // 
            // mainPanel
            // 
            this.mainLayout.SetColumnSpan(this.mainPanel, 5);
            this.mainPanel.Controls.Add(this.imageContainer);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(23, 23);
            this.mainPanel.Name = "mainPanel";
            this.mainLayout.SetRowSpan(this.mainPanel, 3);
            this.mainPanel.Size = new System.Drawing.Size(579, 375);
            this.mainPanel.TabIndex = 0;
            // 
            // imageContainer
            // 
            this.imageContainer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.imageContainer.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.imageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageContainer.Location = new System.Drawing.Point(0, 0);
            this.imageContainer.Name = "imageContainer";
            this.imageContainer.Size = new System.Drawing.Size(579, 375);
            this.imageContainer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imageContainer.TabIndex = 0;
            this.imageContainer.TabStop = false;
            this.imageContainer.UseWaitCursor = true;
            // 
            // bottomPanel
            // 
            this.mainLayout.SetColumnSpan(this.bottomPanel, 5);
            this.bottomPanel.Controls.Add(this.seekerLayout);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(23, 404);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(579, 121);
            this.bottomPanel.TabIndex = 1;
            // 
            // seekerLayout
            // 
            this.seekerLayout.ColumnCount = 5;
            this.seekerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.seekerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.seekerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.seekerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.seekerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.seekerLayout.Controls.Add(this.trackBar1, 0, 2);
            this.seekerLayout.Controls.Add(this.panel1, 3, 2);
            this.seekerLayout.Controls.Add(this.captureButton, 3, 1);
            this.seekerLayout.Controls.Add(this.testButton, 4, 1);
            this.seekerLayout.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.seekerLayout.Controls.Add(this.label6, 0, 0);
            this.seekerLayout.Controls.Add(this.nameTextbox, 1, 0);
            this.seekerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seekerLayout.Location = new System.Drawing.Point(0, 0);
            this.seekerLayout.Name = "seekerLayout";
            this.seekerLayout.RowCount = 4;
            this.seekerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.seekerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.seekerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.seekerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.seekerLayout.Size = new System.Drawing.Size(579, 121);
            this.seekerLayout.TabIndex = 0;
            // 
            // trackBar1
            // 
            this.seekerLayout.SetColumnSpan(this.trackBar1, 3);
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar1.Location = new System.Drawing.Point(3, 63);
            this.trackBar1.Maximum = 0;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(339, 34);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Scroll += new System.EventHandler(this.ScrollImageBroserAction);
            // 
            // panel1
            // 
            this.seekerLayout.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Controls.Add(this.totalFrameLbl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.frameText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(348, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 34);
            this.panel1.TabIndex = 1;
            // 
            // totalFrameLbl
            // 
            this.totalFrameLbl.AutoSize = true;
            this.totalFrameLbl.Location = new System.Drawing.Point(111, 8);
            this.totalFrameLbl.Name = "totalFrameLbl";
            this.totalFrameLbl.Size = new System.Drawing.Size(13, 13);
            this.totalFrameLbl.TabIndex = 2;
            this.totalFrameLbl.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "of";
            // 
            // frameText
            // 
            this.frameText.Location = new System.Drawing.Point(4, 6);
            this.frameText.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.frameText.Name = "frameText";
            this.frameText.Size = new System.Drawing.Size(77, 20);
            this.frameText.TabIndex = 0;
            this.frameText.ValueChanged += new System.EventHandler(this.FrameNumberValueChanged);
            // 
            // captureButton
            // 
            this.captureButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captureButton.Location = new System.Drawing.Point(348, 23);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(109, 34);
            this.captureButton.TabIndex = 2;
            this.captureButton.Text = "Capture";
            this.captureButton.UseVisualStyleBackColor = true;
            this.captureButton.Click += new System.EventHandler(this.CaptureVideoAction);
            // 
            // testButton
            // 
            this.testButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testButton.Location = new System.Drawing.Point(463, 23);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(113, 34);
            this.testButton.TabIndex = 3;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.seekerLayout.SetColumnSpan(this.tableLayoutPanel1, 3);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.urlTextBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(339, 34);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // urlTextBox
            // 
            this.urlTextBox.Cursor = System.Windows.Forms.Cursors.No;
            this.urlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urlTextBox.Location = new System.Drawing.Point(3, 7);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.ReadOnly = true;
            this.urlTextBox.Size = new System.Drawing.Size(333, 20);
            this.urlTextBox.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Name";
            // 
            // nameTextbox
            // 
            this.seekerLayout.SetColumnSpan(this.nameTextbox, 2);
            this.nameTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameTextbox.Location = new System.Drawing.Point(118, 3);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.Size = new System.Drawing.Size(224, 20);
            this.nameTextbox.TabIndex = 6;
            // 
            // videoInfoGroup
            // 
            this.mainLayout.SetColumnSpan(this.videoInfoGroup, 3);
            this.videoInfoGroup.Controls.Add(this.durationLbl);
            this.videoInfoGroup.Controls.Add(this.frameRateLbl);
            this.videoInfoGroup.Controls.Add(this.sizeLbl);
            this.videoInfoGroup.Controls.Add(this.nameLbl);
            this.videoInfoGroup.Controls.Add(this.label5);
            this.videoInfoGroup.Controls.Add(this.label4);
            this.videoInfoGroup.Controls.Add(this.label3);
            this.videoInfoGroup.Controls.Add(this.label2);
            this.videoInfoGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoInfoGroup.Location = new System.Drawing.Point(608, 23);
            this.videoInfoGroup.Name = "videoInfoGroup";
            this.videoInfoGroup.Size = new System.Drawing.Size(345, 121);
            this.videoInfoGroup.TabIndex = 2;
            this.videoInfoGroup.TabStop = false;
            this.videoInfoGroup.Text = "Video Information";
            // 
            // durationLbl
            // 
            this.durationLbl.AutoSize = true;
            this.durationLbl.Location = new System.Drawing.Point(141, 89);
            this.durationLbl.Name = "durationLbl";
            this.durationLbl.Size = new System.Drawing.Size(47, 13);
            this.durationLbl.TabIndex = 7;
            this.durationLbl.Text = "Duration";
            // 
            // frameRateLbl
            // 
            this.frameRateLbl.AutoSize = true;
            this.frameRateLbl.Location = new System.Drawing.Point(141, 65);
            this.frameRateLbl.Name = "frameRateLbl";
            this.frameRateLbl.Size = new System.Drawing.Size(62, 13);
            this.frameRateLbl.TabIndex = 6;
            this.frameRateLbl.Text = "Frame Rate";
            // 
            // sizeLbl
            // 
            this.sizeLbl.AutoSize = true;
            this.sizeLbl.Location = new System.Drawing.Point(141, 42);
            this.sizeLbl.Name = "sizeLbl";
            this.sizeLbl.Size = new System.Drawing.Size(27, 13);
            this.sizeLbl.TabIndex = 5;
            this.sizeLbl.Text = "Size";
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Location = new System.Drawing.Point(141, 19);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(35, 13);
            this.nameLbl.TabIndex = 4;
            this.nameLbl.Text = "Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Duration";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Frame Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name";
            // 
            // logPanel
            // 
            this.mainLayout.SetColumnSpan(this.logPanel, 3);
            this.logPanel.Cursor = System.Windows.Forms.Cursors.No;
            this.logPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logPanel.Location = new System.Drawing.Point(608, 150);
            this.logPanel.Name = "logPanel";
            this.logPanel.ReadOnly = true;
            this.mainLayout.SetRowSpan(this.logPanel, 3);
            this.logPanel.Size = new System.Drawing.Size(345, 375);
            this.logPanel.TabIndex = 3;
            this.logPanel.Text = "";
            // 
            // videoLoadDialog
            // 
            this.videoLoadDialog.FileName = "openFileDialog1";
            this.videoLoadDialog.Filter = "MP4 Video File | *.mp4|All Files|*.*";
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(148, 4);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteImageAction);
            // 
            // Application
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 572);
            this.Controls.Add(this.mainLayout);
            this.Controls.Add(this.mainMenuControl);
            this.MainMenuStrip = this.mainMenuControl;
            this.MaximizeBox = false;
            this.Name = "Application";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pirsa - Video Processing Analytic";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Application_Load);
            this.mainMenuControl.ResumeLayout(false);
            this.mainMenuControl.PerformLayout();
            this.mainLayout.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            this.seekerLayout.ResumeLayout(false);
            this.seekerLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameText)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.videoInfoGroup.ResumeLayout(false);
            this.videoInfoGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuControl;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recognizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRecognizerMenu;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recognizerToolStripMenuItem1;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.PictureBox imageContainer;
        private System.Windows.Forms.TableLayoutPanel seekerLayout;
        private System.Windows.Forms.OpenFileDialog videoLoadDialog;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown frameText;
        private System.Windows.Forms.Label totalFrameLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem trainingDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testDirectoryToolStripMenuItem;
        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.GroupBox videoInfoGroup;
        private System.Windows.Forms.RichTextBox logPanel;
        private System.Windows.Forms.Label durationLbl;
        private System.Windows.Forms.Label frameRateLbl;
        private System.Windows.Forms.Label sizeLbl;
        private System.Windows.Forms.Label nameLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem trainRecognizerMenu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox nameTextbox;
        private System.Windows.Forms.Button deleteButton;
    }
}

