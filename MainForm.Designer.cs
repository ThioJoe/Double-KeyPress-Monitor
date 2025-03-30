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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
            this.SuspendLayout();
            //
            // buttonStart
            //
            this.buttonStart.Location = new System.Drawing.Point(30, 60);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(120, 30);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start Monitoring";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            //
            // buttonStop
            //
            this.buttonStop.Location = new System.Drawing.Point(170, 60);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(120, 30);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop Monitoring";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            //
            // numericUpDownThreshold
            //
            this.numericUpDownThreshold.Location = new System.Drawing.Point(120, 20);
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
            this.numericUpDownThreshold.Size = new System.Drawing.Size(70, 22); // Adjust size as needed
            this.numericUpDownThreshold.TabIndex = 2;
            this.numericUpDownThreshold.Value = new decimal(new int[] {
             10, // Default value
             0,
             0,
             0});
            //
            // labelThreshold
            //
            this.labelThreshold.AutoSize = true;
            this.labelThreshold.Location = new System.Drawing.Point(27, 22);
            this.labelThreshold.Name = "labelThreshold";
            this.labelThreshold.Size = new System.Drawing.Size(87, 16); // Adjust size
            this.labelThreshold.TabIndex = 3;
            this.labelThreshold.Text = "Threshold (ms):";
            //
            // labelStatus
            //
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(27, 110);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(130, 18); // Adjust size
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "Status: Stopped";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F); // Adjust as needed
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 150); // Adjust size
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelThreshold);
            this.Controls.Add(this.numericUpDownThreshold);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Double Key Press Detector"; // Will be set in constructor
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
    }
}

