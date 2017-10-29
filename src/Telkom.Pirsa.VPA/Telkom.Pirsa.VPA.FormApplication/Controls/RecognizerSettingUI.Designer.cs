namespace Telkom.Pirsa.VPA.FormApplication.Controls
{
    partial class RecognizerSettingUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.resetButton = new System.Windows.Forms.Button();
            this.actionPanel = new System.Windows.Forms.TableLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.recognizerPathBrowse = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pComponentText = new System.Windows.Forms.TextBox();
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.scaleFactorText = new System.Windows.Forms.TextBox();
            this.recognizerPathText = new System.Windows.Forms.TextBox();
            this.minNeighbourText = new System.Windows.Forms.TextBox();
            this.minWindowText = new System.Windows.Forms.TextBox();
            this.thresholdText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.userHistogramCheckBox = new System.Windows.Forms.CheckBox();
            this.browseRecognizerFile = new System.Windows.Forms.OpenFileDialog();
            this.actionPanel.SuspendLayout();
            this.ContentPanel.SuspendLayout();
            this.MainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // resetButton
            // 
            this.resetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetButton.Location = new System.Drawing.Point(94, 3);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(85, 34);
            this.resetButton.TabIndex = 1;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // actionPanel
            // 
            this.actionPanel.ColumnCount = 5;
            this.actionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.actionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.actionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.actionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.actionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.actionPanel.Controls.Add(this.saveButton, 3, 0);
            this.actionPanel.Controls.Add(this.resetButton, 1, 0);
            this.actionPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.actionPanel.Location = new System.Drawing.Point(0, 484);
            this.actionPanel.Name = "actionPanel";
            this.actionPanel.RowCount = 1;
            this.actionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.actionPanel.Size = new System.Drawing.Size(376, 40);
            this.actionPanel.TabIndex = 24;
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Location = new System.Drawing.Point(195, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(85, 34);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // recognizerPathBrowse
            // 
            this.recognizerPathBrowse.Location = new System.Drawing.Point(338, 217);
            this.recognizerPathBrowse.Name = "recognizerPathBrowse";
            this.recognizerPathBrowse.Size = new System.Drawing.Size(26, 23);
            this.recognizerPathBrowse.TabIndex = 21;
            this.recognizerPathBrowse.Text = "...";
            this.recognizerPathBrowse.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(341, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "pixel";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Recognizer Path";
            // 
            // pComponentText
            // 
            this.pComponentText.Cursor = System.Windows.Forms.Cursors.No;
            this.pComponentText.Location = new System.Drawing.Point(140, 160);
            this.pComponentText.Name = "pComponentText";
            this.pComponentText.Size = new System.Drawing.Size(224, 20);
            this.pComponentText.TabIndex = 14;
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.userHistogramCheckBox);
            this.ContentPanel.Controls.Add(this.actionPanel);
            this.ContentPanel.Controls.Add(this.recognizerPathBrowse);
            this.ContentPanel.Controls.Add(this.label10);
            this.ContentPanel.Controls.Add(this.label9);
            this.ContentPanel.Controls.Add(this.pComponentText);
            this.ContentPanel.Controls.Add(this.scaleFactorText);
            this.ContentPanel.Controls.Add(this.recognizerPathText);
            this.ContentPanel.Controls.Add(this.minNeighbourText);
            this.ContentPanel.Controls.Add(this.minWindowText);
            this.ContentPanel.Controls.Add(this.thresholdText);
            this.ContentPanel.Controls.Add(this.label6);
            this.ContentPanel.Controls.Add(this.label5);
            this.ContentPanel.Controls.Add(this.label3);
            this.ContentPanel.Controls.Add(this.label2);
            this.ContentPanel.Controls.Add(this.label1);
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(13, 13);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(376, 524);
            this.ContentPanel.TabIndex = 0;
            // 
            // scaleFactorText
            // 
            this.scaleFactorText.Cursor = System.Windows.Forms.Cursors.No;
            this.scaleFactorText.Location = new System.Drawing.Point(140, 127);
            this.scaleFactorText.Name = "scaleFactorText";
            this.scaleFactorText.Size = new System.Drawing.Size(224, 20);
            this.scaleFactorText.TabIndex = 11;
            // 
            // recognizerPathText
            // 
            this.recognizerPathText.Cursor = System.Windows.Forms.Cursors.No;
            this.recognizerPathText.Location = new System.Drawing.Point(140, 219);
            this.recognizerPathText.Name = "recognizerPathText";
            this.recognizerPathText.ReadOnly = true;
            this.recognizerPathText.Size = new System.Drawing.Size(190, 20);
            this.recognizerPathText.TabIndex = 10;
            // 
            // minNeighbourText
            // 
            this.minNeighbourText.Location = new System.Drawing.Point(140, 94);
            this.minNeighbourText.Name = "minNeighbourText";
            this.minNeighbourText.Size = new System.Drawing.Size(224, 20);
            this.minNeighbourText.TabIndex = 8;
            // 
            // minWindowText
            // 
            this.minWindowText.Location = new System.Drawing.Point(140, 60);
            this.minWindowText.Name = "minWindowText";
            this.minWindowText.Size = new System.Drawing.Size(190, 20);
            this.minWindowText.TabIndex = 7;
            // 
            // thresholdText
            // 
            this.thresholdText.Location = new System.Drawing.Point(140, 26);
            this.thresholdText.Name = "thresholdText";
            this.thresholdText.Size = new System.Drawing.Size(224, 20);
            this.thresholdText.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Principal Component";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Scale Factor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Minimum Neighbour";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minimum Window Size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Threshold";
            // 
            // MainLayout
            // 
            this.MainLayout.ColumnCount = 3;
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.MainLayout.Controls.Add(this.ContentPanel, 1, 1);
            this.MainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainLayout.Location = new System.Drawing.Point(0, 0);
            this.MainLayout.Name = "MainLayout";
            this.MainLayout.RowCount = 3;
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.MainLayout.Size = new System.Drawing.Size(402, 550);
            this.MainLayout.TabIndex = 1;
            // 
            // userHistogramCheckBox
            // 
            this.userHistogramCheckBox.AutoSize = true;
            this.userHistogramCheckBox.Location = new System.Drawing.Point(140, 190);
            this.userHistogramCheckBox.Name = "userHistogramCheckBox";
            this.userHistogramCheckBox.Size = new System.Drawing.Size(155, 17);
            this.userHistogramCheckBox.TabIndex = 25;
            this.userHistogramCheckBox.Text = "Use Histogram Equalization";
            this.userHistogramCheckBox.UseVisualStyleBackColor = true;
            // 
            // browseRecognizerFile
            // 
            this.browseRecognizerFile.FileName = "haarcascade_frontalface_default.xml";
            this.browseRecognizerFile.Filter = "XML files|*.xml";
            // 
            // RecognizerSettingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainLayout);
            this.Name = "RecognizerSettingUI";
            this.Size = new System.Drawing.Size(402, 550);
            this.actionPanel.ResumeLayout(false);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.MainLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TableLayoutPanel actionPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button recognizerPathBrowse;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox pComponentText;
        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.TextBox scaleFactorText;
        private System.Windows.Forms.TextBox recognizerPathText;
        private System.Windows.Forms.TextBox minNeighbourText;
        private System.Windows.Forms.TextBox minWindowText;
        private System.Windows.Forms.TextBox thresholdText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.CheckBox userHistogramCheckBox;
        private System.Windows.Forms.OpenFileDialog browseRecognizerFile;

    }
}
