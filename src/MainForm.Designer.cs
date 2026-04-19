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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOpenLog = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonStart.Location = new System.Drawing.Point(2, 3);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(90, 24);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start Monitoring";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonStop.Location = new System.Drawing.Point(99, 3);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(90, 24);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop Monitoring";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // numericUpDownThreshold
            // 
            this.numericUpDownThreshold.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numericUpDownThreshold.Location = new System.Drawing.Point(88, 4);
            this.numericUpDownThreshold.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
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
            this.numericUpDownThreshold.Size = new System.Drawing.Size(53, 20);
            this.numericUpDownThreshold.TabIndex = 2;
            this.numericUpDownThreshold.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // labelThreshold
            // 
            this.labelThreshold.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelThreshold.AutoSize = true;
            this.labelThreshold.Location = new System.Drawing.Point(2, 7);
            this.labelThreshold.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelThreshold.Name = "labelThreshold";
            this.labelThreshold.Size = new System.Drawing.Size(79, 13);
            this.labelThreshold.TabIndex = 3;
            this.labelThreshold.Text = "Threshold (ms):";
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(81, 94);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(108, 15);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "Status: Stopped";
            // 
            // checkBoxPlaySound
            // 
            this.checkBoxPlaySound.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxPlaySound.AutoSize = true;
            this.checkBoxPlaySound.Checked = true;
            this.checkBoxPlaySound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlaySound.Location = new System.Drawing.Point(2, 3);
            this.checkBoxPlaySound.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxPlaySound.Name = "checkBoxPlaySound";
            this.checkBoxPlaySound.Size = new System.Drawing.Size(146, 17);
            this.checkBoxPlaySound.TabIndex = 5;
            this.checkBoxPlaySound.Text = "Play Sound On Detection";
            this.checkBoxPlaySound.UseVisualStyleBackColor = true;
            // 
            // buttonPreviewSound
            // 
            this.buttonPreviewSound.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonPreviewSound.Location = new System.Drawing.Point(152, 2);
            this.buttonPreviewSound.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPreviewSound.Name = "buttonPreviewSound";
            this.buttonPreviewSound.Size = new System.Drawing.Size(63, 19);
            this.buttonPreviewSound.TabIndex = 6;
            this.buttonPreviewSound.Text = "🔊 Preview";
            this.buttonPreviewSound.UseVisualStyleBackColor = true;
            this.buttonPreviewSound.Click += new System.EventHandler(this.buttonPreviewSound_Click);
            // 
            // textBoxSoundAlias
            // 
            this.textBoxSoundAlias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSoundAlias.Location = new System.Drawing.Point(77, 2);
            this.textBoxSoundAlias.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSoundAlias.Name = "textBoxSoundAlias";
            this.textBoxSoundAlias.Size = new System.Drawing.Size(96, 20);
            this.textBoxSoundAlias.TabIndex = 7;
            // 
            // labelSoundAlias
            // 
            this.labelSoundAlias.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSoundAlias.AutoSize = true;
            this.labelSoundAlias.Location = new System.Drawing.Point(4, 6);
            this.labelSoundAlias.Margin = new System.Windows.Forms.Padding(0);
            this.labelSoundAlias.Name = "labelSoundAlias";
            this.labelSoundAlias.Size = new System.Drawing.Size(66, 13);
            this.labelSoundAlias.TabIndex = 8;
            this.labelSoundAlias.Text = "Sound Alias:";
            // 
            // buttonInfo
            // 
            this.buttonInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonInfo.Location = new System.Drawing.Point(74, 2);
            this.buttonInfo.Margin = new System.Windows.Forms.Padding(2);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(37, 23);
            this.buttonInfo.TabIndex = 9;
            this.buttonInfo.Text = "Args";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonSoundHelp
            // 
            this.buttonSoundHelp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonSoundHelp.Location = new System.Drawing.Point(177, 2);
            this.buttonSoundHelp.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSoundHelp.Name = "buttonSoundHelp";
            this.buttonSoundHelp.Size = new System.Drawing.Size(18, 21);
            this.buttonSoundHelp.TabIndex = 11;
            this.buttonSoundHelp.Text = "?";
            this.buttonSoundHelp.UseVisualStyleBackColor = true;
            this.buttonSoundHelp.Click += new System.EventHandler(this.buttonSoundHelp_Click);
            // 
            // labelIgnore
            // 
            this.labelIgnore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelIgnore.AutoSize = true;
            this.labelIgnore.Location = new System.Drawing.Point(4, 31);
            this.labelIgnore.Margin = new System.Windows.Forms.Padding(0);
            this.labelIgnore.Name = "labelIgnore";
            this.labelIgnore.Size = new System.Drawing.Size(67, 13);
            this.labelIgnore.TabIndex = 12;
            this.labelIgnore.Text = "Ignored Key:";
            // 
            // textBoxIgnore
            // 
            this.textBoxIgnore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxIgnore.Location = new System.Drawing.Point(77, 27);
            this.textBoxIgnore.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxIgnore.Name = "textBoxIgnore";
            this.textBoxIgnore.Size = new System.Drawing.Size(96, 20);
            this.textBoxIgnore.TabIndex = 13;
            // 
            // buttonIgnoreHelp
            // 
            this.buttonIgnoreHelp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonIgnoreHelp.Location = new System.Drawing.Point(177, 27);
            this.buttonIgnoreHelp.Margin = new System.Windows.Forms.Padding(2);
            this.buttonIgnoreHelp.Name = "buttonIgnoreHelp";
            this.buttonIgnoreHelp.Size = new System.Drawing.Size(18, 21);
            this.buttonIgnoreHelp.TabIndex = 14;
            this.buttonIgnoreHelp.Text = "?";
            this.buttonIgnoreHelp.UseVisualStyleBackColor = true;
            this.buttonIgnoreHelp.Click += new System.EventHandler(this.buttonIgnoreHelp_Click);
            // 
            // buttonCreateShortcut
            // 
            this.buttonCreateShortcut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCreateShortcut.Location = new System.Drawing.Point(148, 2);
            this.buttonCreateShortcut.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCreateShortcut.Name = "buttonCreateShortcut";
            this.buttonCreateShortcut.Size = new System.Drawing.Size(89, 23);
            this.buttonCreateShortcut.TabIndex = 15;
            this.buttonCreateShortcut.Text = "Copy Command";
            this.buttonCreateShortcut.UseVisualStyleBackColor = true;
            this.buttonCreateShortcut.Click += new System.EventHandler(this.buttonCreateShortcut_Click);
            // 
            // buttonCopyCommandHelp
            // 
            this.buttonCopyCommandHelp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCopyCommandHelp.Location = new System.Drawing.Point(241, 2);
            this.buttonCopyCommandHelp.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCopyCommandHelp.Name = "buttonCopyCommandHelp";
            this.buttonCopyCommandHelp.Size = new System.Drawing.Size(17, 23);
            this.buttonCopyCommandHelp.TabIndex = 16;
            this.buttonCopyCommandHelp.Text = "?";
            this.buttonCopyCommandHelp.UseVisualStyleBackColor = true;
            this.buttonCopyCommandHelp.Click += new System.EventHandler(this.buttonCopyCommandHelp_Click);
            // 
            // labelCopyCheck
            // 
            this.labelCopyCheck.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelCopyCheck.AutoSize = true;
            this.labelCopyCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelCopyCheck.ForeColor = System.Drawing.Color.SeaGreen;
            this.labelCopyCheck.Location = new System.Drawing.Point(122, 5);
            this.labelCopyCheck.Margin = new System.Windows.Forms.Padding(0);
            this.labelCopyCheck.Name = "labelCopyCheck";
            this.labelCopyCheck.Size = new System.Drawing.Size(24, 17);
            this.labelCopyCheck.TabIndex = 17;
            this.labelCopyCheck.Text = "✔️";
            this.labelCopyCheck.Visible = false;
            // 
            // buttonRemoveLastEntry
            // 
            this.buttonRemoveLastEntry.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemoveLastEntry.Location = new System.Drawing.Point(26, 2);
            this.buttonRemoveLastEntry.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRemoveLastEntry.Name = "buttonRemoveLastEntry";
            this.buttonRemoveLastEntry.Size = new System.Drawing.Size(179, 25);
            this.buttonRemoveLastEntry.TabIndex = 18;
            this.buttonRemoveLastEntry.Text = "Remove Last Log Entry";
            this.buttonRemoveLastEntry.UseVisualStyleBackColor = true;
            this.buttonRemoveLastEntry.Click += new System.EventHandler(this.buttonRemoveLastEntry_Click);
            // 
            // buttonRemoveLastEntryHelp
            // 
            this.buttonRemoveLastEntryHelp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRemoveLastEntryHelp.Location = new System.Drawing.Point(209, 3);
            this.buttonRemoveLastEntryHelp.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRemoveLastEntryHelp.Name = "buttonRemoveLastEntryHelp";
            this.buttonRemoveLastEntryHelp.Size = new System.Drawing.Size(18, 23);
            this.buttonRemoveLastEntryHelp.TabIndex = 19;
            this.buttonRemoveLastEntryHelp.Text = "?";
            this.buttonRemoveLastEntryHelp.UseVisualStyleBackColor = true;
            this.buttonRemoveLastEntryHelp.Click += new System.EventHandler(this.buttonRemoveLastEntryHelp_Click);
            // 
            // labelRemoveLastEntry
            // 
            this.labelRemoveLastEntry.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelRemoveLastEntry.AutoSize = true;
            this.labelRemoveLastEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelRemoveLastEntry.ForeColor = System.Drawing.Color.SeaGreen;
            this.labelRemoveLastEntry.Location = new System.Drawing.Point(0, 6);
            this.labelRemoveLastEntry.Margin = new System.Windows.Forms.Padding(0);
            this.labelRemoveLastEntry.Name = "labelRemoveLastEntry";
            this.labelRemoveLastEntry.Size = new System.Drawing.Size(24, 17);
            this.labelRemoveLastEntry.TabIndex = 20;
            this.labelRemoveLastEntry.Text = "✔️";
            this.labelRemoveLastEntry.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.Controls.Add(this.buttonOpenLog, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonInfo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelCopyCheck, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonCreateShortcut, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonCopyCommandHelp, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(260, 27);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // buttonOpenLog
            // 
            this.buttonOpenLog.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonOpenLog.Location = new System.Drawing.Point(2, 2);
            this.buttonOpenLog.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.Size = new System.Drawing.Size(63, 23);
            this.buttonOpenLog.TabIndex = 10;
            this.buttonOpenLog.Text = "Open Log";
            this.buttonOpenLog.UseCompatibleTextRendering = true;
            this.buttonOpenLog.UseVisualStyleBackColor = true;
            this.buttonOpenLog.Click += new System.EventHandler(this.buttonOpenLog_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.labelThreshold, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericUpDownThreshold, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(48, 32);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(173, 28);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.buttonStart, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonStop, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(39, 60);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(191, 30);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel2.Controls.Add(this.checkBoxPlaySound);
            this.flowLayoutPanel2.Controls.Add(this.buttonPreviewSound);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(24, 113);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(221, 27);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.Controls.Add(this.labelSoundAlias, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelIgnore, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.textBoxSoundAlias, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.textBoxIgnore, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.buttonIgnoreHelp, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.buttonSoundHelp, 2, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(35, 140);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(200, 50);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel3.Controls.Add(this.labelRemoveLastEntry);
            this.flowLayoutPanel3.Controls.Add(this.buttonRemoveLastEntry);
            this.flowLayoutPanel3.Controls.Add(this.buttonRemoveLastEntryHelp);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(13, 196);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(243, 30);
            this.flowLayoutPanel3.TabIndex = 7;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.labelStatus, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.flowLayoutPanel3, 0, 6);
            this.tableLayoutPanel6.Controls.Add(this.flowLayoutPanel2, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel4, 0, 5);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel6.RowCount = 7;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.5F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(270, 237);
            this.tableLayoutPanel6.TabIndex = 22;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 237);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(286, 276);
            this.Name = "MainForm";
            this.Text = "Double Key Press Detector";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button buttonOpenLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    }
}

