namespace DoubleKeyPressDetector
{
    partial class MainForm
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.numericUpDownThreshold = new System.Windows.Forms.NumericUpDown();
            this.labelThreshold = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkBoxPlaySound = new System.Windows.Forms.CheckBox();
            this.buttonPreviewSound = new System.Windows.Forms.Button();
            this.textBoxSoundAlias = new System.Windows.Forms.TextBox();
            this.labelSoundAlias = new System.Windows.Forms.Label();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.buttonOpenLog = new System.Windows.Forms.Button();
            this.buttonSoundHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(34, 75);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(135, 38);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start Monitoring";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(191, 75);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(135, 38);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop Monitoring";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // numericUpDownThreshold
            // 
            this.numericUpDownThreshold.Location = new System.Drawing.Point(154, 27);
            this.numericUpDownThreshold.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownThreshold.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownThreshold.Name = "numericUpDownThreshold";
            this.numericUpDownThreshold.Size = new System.Drawing.Size(79, 26);
            this.numericUpDownThreshold.TabIndex = 2;
            this.numericUpDownThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelThreshold
            // 
            this.labelThreshold.AutoSize = true;
            this.labelThreshold.Location = new System.Drawing.Point(30, 28);
            this.labelThreshold.Name = "labelThreshold";
            this.labelThreshold.Size = new System.Drawing.Size(118, 20);
            this.labelThreshold.TabIndex = 3;
            this.labelThreshold.Text = "Threshold (ms):";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(30, 138);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(153, 22);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "Status: Stopped";
            // 
            // checkBoxPlaySound
            // 
            this.checkBoxPlaySound.AutoSize = true;
            this.checkBoxPlaySound.Checked = true;
            this.checkBoxPlaySound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlaySound.Location = new System.Drawing.Point(20, 187);
            this.checkBoxPlaySound.Name = "checkBoxPlaySound";
            this.checkBoxPlaySound.Size = new System.Drawing.Size(213, 24);
            this.checkBoxPlaySound.TabIndex = 5;
            this.checkBoxPlaySound.Text = "Play Sound On Detection";
            this.checkBoxPlaySound.UseVisualStyleBackColor = true;
            // 
            // buttonPreviewSound
            // 
            this.buttonPreviewSound.Location = new System.Drawing.Point(239, 180);
            this.buttonPreviewSound.Name = "buttonPreviewSound";
            this.buttonPreviewSound.Size = new System.Drawing.Size(95, 36);
            this.buttonPreviewSound.TabIndex = 6;
            this.buttonPreviewSound.Text = "🔊 Preview";
            this.buttonPreviewSound.UseVisualStyleBackColor = true;
            this.buttonPreviewSound.Click += new System.EventHandler(this.buttonPreviewSound_Click);
            // 
            // textBoxSoundAlias
            // 
            this.textBoxSoundAlias.Location = new System.Drawing.Point(122, 231);
            this.textBoxSoundAlias.Name = "textBoxSoundAlias";
            this.textBoxSoundAlias.Size = new System.Drawing.Size(194, 26);
            this.textBoxSoundAlias.TabIndex = 7;
            // 
            // labelSoundAlias
            // 
            this.labelSoundAlias.AutoSize = true;
            this.labelSoundAlias.Location = new System.Drawing.Point(13, 231);
            this.labelSoundAlias.Name = "labelSoundAlias";
            this.labelSoundAlias.Size = new System.Drawing.Size(98, 20);
            this.labelSoundAlias.TabIndex = 8;
            this.labelSoundAlias.Text = "Sound Alias:";
            // 
            // buttonInfo
            // 
            this.buttonInfo.Location = new System.Drawing.Point(321, 12);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(27, 36);
            this.buttonInfo.TabIndex = 9;
            this.buttonInfo.Text = "?";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonOpenLog
            // 
            this.buttonOpenLog.Location = new System.Drawing.Point(239, 135);
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.Size = new System.Drawing.Size(95, 31);
            this.buttonOpenLog.TabIndex = 10;
            this.buttonOpenLog.Text = "Open Log";
            this.buttonOpenLog.UseCompatibleTextRendering = true;
            this.buttonOpenLog.UseVisualStyleBackColor = true;
            this.buttonOpenLog.Click += new System.EventHandler(this.buttonOpenLog_Click);
            // 
            // buttonSoundHelp
            // 
            this.buttonSoundHelp.Location = new System.Drawing.Point(322, 226);
            this.buttonSoundHelp.Name = "buttonSoundHelp";
            this.buttonSoundHelp.Size = new System.Drawing.Size(27, 36);
            this.buttonSoundHelp.TabIndex = 11;
            this.buttonSoundHelp.Text = "?";
            this.buttonSoundHelp.UseVisualStyleBackColor = true;
            this.buttonSoundHelp.Click += new System.EventHandler(this.buttonSoundHelp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 279);
            this.Controls.Add(this.buttonSoundHelp);
            this.Controls.Add(this.buttonOpenLog);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.labelSoundAlias);
            this.Controls.Add(this.textBoxSoundAlias);
            this.Controls.Add(this.buttonPreviewSound);
            this.Controls.Add(this.checkBoxPlaySound);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelThreshold);
            this.Controls.Add(this.numericUpDownThreshold);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Double Key Press Detector";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.NumericUpDown numericUpDownThreshold;
        private System.Windows.Forms.Label labelThreshold;
        private System.Windows.Forms.Label labelStatus;

        #endregion

        private System.Windows.Forms.CheckBox checkBoxPlaySound;
        private System.Windows.Forms.Button buttonPreviewSound;
        private System.Windows.Forms.TextBox textBoxSoundAlias;
        private System.Windows.Forms.Label labelSoundAlias;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.Button buttonOpenLog;
        private System.Windows.Forms.Button buttonSoundHelp;
    }
}

