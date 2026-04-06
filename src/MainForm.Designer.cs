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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.labelIgnore = new System.Windows.Forms.Label();
            this.textBoxIgnore = new System.Windows.Forms.TextBox();
            this.buttonIgnoreHelp = new System.Windows.Forms.Button();
            this.buttonCreateShortcut = new System.Windows.Forms.Button();
            this.buttonCopyCommandHelp = new System.Windows.Forms.Button();
            this.labelCopyCheck = new System.Windows.Forms.Label();
            this.buttonRemoveLastEntry = new System.Windows.Forms.Button();
            this.buttonRemoveLastEntryHelp = new System.Windows.Forms.Button();
            this.labelRemoveLastEntry = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(48, 96);
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
            this.buttonStop.Location = new System.Drawing.Point(189, 96);
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
            this.numericUpDownThreshold.Location = new System.Drawing.Point(189, 60);
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
            50,
            0,
            0,
            0});
            // 
            // labelThreshold
            // 
            this.labelThreshold.AutoSize = true;
            this.labelThreshold.Location = new System.Drawing.Point(65, 62);
            this.labelThreshold.Name = "labelThreshold";
            this.labelThreshold.Size = new System.Drawing.Size(118, 20);
            this.labelThreshold.TabIndex = 3;
            this.labelThreshold.Text = "Threshold (ms):";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(106, 145);
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
            this.checkBoxPlaySound.Location = new System.Drawing.Point(44, 177);
            this.checkBoxPlaySound.Name = "checkBoxPlaySound";
            this.checkBoxPlaySound.Size = new System.Drawing.Size(213, 24);
            this.checkBoxPlaySound.TabIndex = 5;
            this.checkBoxPlaySound.Text = "Play Sound On Detection";
            this.checkBoxPlaySound.UseVisualStyleBackColor = true;
            // 
            // buttonPreviewSound
            // 
            this.buttonPreviewSound.Location = new System.Drawing.Point(254, 174);
            this.buttonPreviewSound.Name = "buttonPreviewSound";
            this.buttonPreviewSound.Size = new System.Drawing.Size(95, 29);
            this.buttonPreviewSound.TabIndex = 6;
            this.buttonPreviewSound.Text = "🔊 Preview";
            this.buttonPreviewSound.UseVisualStyleBackColor = true;
            this.buttonPreviewSound.Click += new System.EventHandler(this.buttonPreviewSound_Click);
            // 
            // textBoxSoundAlias
            // 
            this.textBoxSoundAlias.Location = new System.Drawing.Point(127, 218);
            this.textBoxSoundAlias.Name = "textBoxSoundAlias";
            this.textBoxSoundAlias.Size = new System.Drawing.Size(194, 26);
            this.textBoxSoundAlias.TabIndex = 7;
            // 
            // labelSoundAlias
            // 
            this.labelSoundAlias.AutoSize = true;
            this.labelSoundAlias.Location = new System.Drawing.Point(18, 218);
            this.labelSoundAlias.Name = "labelSoundAlias";
            this.labelSoundAlias.Size = new System.Drawing.Size(98, 20);
            this.labelSoundAlias.TabIndex = 8;
            this.labelSoundAlias.Text = "Sound Alias:";
            // 
            // buttonInfo
            // 
            this.buttonInfo.Location = new System.Drawing.Point(113, 13);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(56, 36);
            this.buttonInfo.TabIndex = 9;
            this.buttonInfo.Text = "Args";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonOpenLog
            // 
            this.buttonOpenLog.Location = new System.Drawing.Point(12, 12);
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.Size = new System.Drawing.Size(95, 37);
            this.buttonOpenLog.TabIndex = 10;
            this.buttonOpenLog.Text = "Open Log";
            this.buttonOpenLog.UseCompatibleTextRendering = true;
            this.buttonOpenLog.UseVisualStyleBackColor = true;
            this.buttonOpenLog.Click += new System.EventHandler(this.buttonOpenLog_Click);
            // 
            // buttonSoundHelp
            // 
            this.buttonSoundHelp.Location = new System.Drawing.Point(327, 213);
            this.buttonSoundHelp.Name = "buttonSoundHelp";
            this.buttonSoundHelp.Size = new System.Drawing.Size(27, 36);
            this.buttonSoundHelp.TabIndex = 11;
            this.buttonSoundHelp.Text = "?";
            this.buttonSoundHelp.UseVisualStyleBackColor = true;
            this.buttonSoundHelp.Click += new System.EventHandler(this.buttonSoundHelp_Click);
            // 
            // labelIgnore
            // 
            this.labelIgnore.AutoSize = true;
            this.labelIgnore.Location = new System.Drawing.Point(21, 256);
            this.labelIgnore.Name = "labelIgnore";
            this.labelIgnore.Size = new System.Drawing.Size(98, 20);
            this.labelIgnore.TabIndex = 12;
            this.labelIgnore.Text = "Ignored Key:";
            // 
            // textBoxIgnore
            // 
            this.textBoxIgnore.Location = new System.Drawing.Point(127, 253);
            this.textBoxIgnore.Name = "textBoxIgnore";
            this.textBoxIgnore.Size = new System.Drawing.Size(194, 26);
            this.textBoxIgnore.TabIndex = 13;
            // 
            // buttonIgnoreHelp
            // 
            this.buttonIgnoreHelp.Location = new System.Drawing.Point(326, 253);
            this.buttonIgnoreHelp.Name = "buttonIgnoreHelp";
            this.buttonIgnoreHelp.Size = new System.Drawing.Size(27, 36);
            this.buttonIgnoreHelp.TabIndex = 14;
            this.buttonIgnoreHelp.Text = "?";
            this.buttonIgnoreHelp.UseVisualStyleBackColor = true;
            this.buttonIgnoreHelp.Click += new System.EventHandler(this.buttonIgnoreHelp_Click);
            // 
            // buttonCreateShortcut
            // 
            this.buttonCreateShortcut.Location = new System.Drawing.Point(212, 11);
            this.buttonCreateShortcut.Name = "buttonCreateShortcut";
            this.buttonCreateShortcut.Size = new System.Drawing.Size(137, 27);
            this.buttonCreateShortcut.TabIndex = 15;
            this.buttonCreateShortcut.Text = "Copy Command";
            this.buttonCreateShortcut.UseVisualStyleBackColor = true;
            this.buttonCreateShortcut.Click += new System.EventHandler(this.buttonCreateShortcut_Click);
            // 
            // buttonCopyCommandHelp
            // 
            this.buttonCopyCommandHelp.Location = new System.Drawing.Point(355, 11);
            this.buttonCopyCommandHelp.Name = "buttonCopyCommandHelp";
            this.buttonCopyCommandHelp.Size = new System.Drawing.Size(27, 27);
            this.buttonCopyCommandHelp.TabIndex = 16;
            this.buttonCopyCommandHelp.Text = "?";
            this.buttonCopyCommandHelp.UseVisualStyleBackColor = true;
            this.buttonCopyCommandHelp.Click += new System.EventHandler(this.buttonCopyCommandHelp_Click);
            // 
            // labelCopyCheck
            // 
            this.labelCopyCheck.AutoSize = true;
            this.labelCopyCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelCopyCheck.ForeColor = System.Drawing.Color.SeaGreen;
            this.labelCopyCheck.Location = new System.Drawing.Point(184, 11);
            this.labelCopyCheck.Name = "labelCopyCheck";
            this.labelCopyCheck.Size = new System.Drawing.Size(33, 25);
            this.labelCopyCheck.TabIndex = 17;
            this.labelCopyCheck.Text = "✔️";
            this.labelCopyCheck.Visible = false;
            // 
            // buttonRemoveLastEntry
            // 
            this.buttonRemoveLastEntry.Location = new System.Drawing.Point(53, 299);
            this.buttonRemoveLastEntry.Name = "buttonRemoveLastEntry";
            this.buttonRemoveLastEntry.Size = new System.Drawing.Size(268, 38);
            this.buttonRemoveLastEntry.TabIndex = 18;
            this.buttonRemoveLastEntry.Text = "Remove Last Log Entry";
            this.buttonRemoveLastEntry.UseVisualStyleBackColor = true;
            this.buttonRemoveLastEntry.Click += new System.EventHandler(this.buttonRemoveLastEntry_Click);
            // 
            // buttonRemoveLastEntryHelp
            // 
            this.buttonRemoveLastEntryHelp.Location = new System.Drawing.Point(326, 301);
            this.buttonRemoveLastEntryHelp.Name = "buttonRemoveLastEntryHelp";
            this.buttonRemoveLastEntryHelp.Size = new System.Drawing.Size(27, 36);
            this.buttonRemoveLastEntryHelp.TabIndex = 19;
            this.buttonRemoveLastEntryHelp.Text = "?";
            this.buttonRemoveLastEntryHelp.UseVisualStyleBackColor = true;
            this.buttonRemoveLastEntryHelp.Click += new System.EventHandler(this.buttonRemoveLastEntryHelp_Click);
            // 
            // labelRemoveLastEntry
            // 
            this.labelRemoveLastEntry.AutoSize = true;
            this.labelRemoveLastEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelRemoveLastEntry.ForeColor = System.Drawing.Color.SeaGreen;
            this.labelRemoveLastEntry.Location = new System.Drawing.Point(20, 301);
            this.labelRemoveLastEntry.Name = "labelRemoveLastEntry";
            this.labelRemoveLastEntry.Size = new System.Drawing.Size(33, 25);
            this.labelRemoveLastEntry.TabIndex = 20;
            this.labelRemoveLastEntry.Text = "✔️";
            this.labelRemoveLastEntry.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 355);
            this.Controls.Add(this.labelRemoveLastEntry);
            this.Controls.Add(this.buttonRemoveLastEntryHelp);
            this.Controls.Add(this.buttonRemoveLastEntry);
            this.Controls.Add(this.buttonCopyCommandHelp);
            this.Controls.Add(this.buttonCreateShortcut);
            this.Controls.Add(this.buttonIgnoreHelp);
            this.Controls.Add(this.textBoxIgnore);
            this.Controls.Add(this.labelIgnore);
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
            this.Controls.Add(this.labelCopyCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Label labelIgnore;
        private System.Windows.Forms.TextBox textBoxIgnore;
        private System.Windows.Forms.Button buttonIgnoreHelp;
        private System.Windows.Forms.Button buttonCreateShortcut;
        private System.Windows.Forms.Button buttonCopyCommandHelp;
        private System.Windows.Forms.Label labelCopyCheck;
        private System.Windows.Forms.Button buttonRemoveLastEntry;
        private System.Windows.Forms.Button buttonRemoveLastEntryHelp;
        private System.Windows.Forms.Label labelRemoveLastEntry;
    }
}

