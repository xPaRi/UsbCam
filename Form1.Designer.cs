namespace UsbCam
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabCircleDiameter = new System.Windows.Forms.Label();
            this.LabAngle = new System.Windows.Forms.Label();
            this.ConCircleDiameter = new System.Windows.Forms.TrackBar();
            this.ConAngle = new System.Windows.Forms.TrackBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MnuCamera = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuCopyImage = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConCircleDiameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConAngle)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(733, 459);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.WaitOnLoad = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LabCircleDiameter);
            this.panel1.Controls.Add(this.LabAngle);
            this.panel1.Controls.Add(this.ConCircleDiameter);
            this.panel1.Controls.Add(this.ConAngle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 459);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 77);
            this.panel1.TabIndex = 0;
            // 
            // LabCircleDiameter
            // 
            this.LabCircleDiameter.AutoSize = true;
            this.LabCircleDiameter.Location = new System.Drawing.Point(12, 11);
            this.LabCircleDiameter.Name = "LabCircleDiameter";
            this.LabCircleDiameter.Size = new System.Drawing.Size(33, 13);
            this.LabCircleDiameter.TabIndex = 0;
            this.LabCircleDiameter.Text = "&Circle";
            // 
            // LabAngle
            // 
            this.LabAngle.AutoSize = true;
            this.LabAngle.Location = new System.Drawing.Point(227, 11);
            this.LabAngle.Name = "LabAngle";
            this.LabAngle.Size = new System.Drawing.Size(34, 13);
            this.LabAngle.TabIndex = 2;
            this.LabAngle.Text = "&Angle";
            // 
            // ConCircleDiameter
            // 
            this.ConCircleDiameter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConCircleDiameter.LargeChange = 10;
            this.ConCircleDiameter.Location = new System.Drawing.Point(3, 29);
            this.ConCircleDiameter.Maximum = 400;
            this.ConCircleDiameter.Name = "ConCircleDiameter";
            this.ConCircleDiameter.Size = new System.Drawing.Size(208, 45);
            this.ConCircleDiameter.TabIndex = 1;
            this.ConCircleDiameter.TickFrequency = 50;
            this.ConCircleDiameter.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.ConCircleDiameter.ValueChanged += new System.EventHandler(this.ConCircleDiameter_ValueChanged);
            // 
            // ConAngle
            // 
            this.ConAngle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConAngle.LargeChange = 15;
            this.ConAngle.Location = new System.Drawing.Point(217, 29);
            this.ConAngle.Maximum = 360;
            this.ConAngle.Minimum = -1;
            this.ConAngle.Name = "ConAngle";
            this.ConAngle.Size = new System.Drawing.Size(513, 45);
            this.ConAngle.TabIndex = 3;
            this.ConAngle.TickFrequency = 15;
            this.ConAngle.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.ConAngle.ValueChanged += new System.EventHandler(this.ConAngle_ValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuCamera,
            this.MnuSettings,
            this.MnuCopyImage,
            this.MnuAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(733, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "MnuMain";
            // 
            // MnuCamera
            // 
            this.MnuCamera.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.MnuExit});
            this.MnuCamera.Name = "MnuCamera";
            this.MnuCamera.Size = new System.Drawing.Size(60, 20);
            this.MnuCamera.Text = "Camera";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(132, 6);
            // 
            // MnuExit
            // 
            this.MnuExit.Name = "MnuExit";
            this.MnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.MnuExit.Size = new System.Drawing.Size(135, 22);
            this.MnuExit.Text = "&Exit";
            this.MnuExit.Click += new System.EventHandler(this.MnuExit_Click);
            // 
            // MnuSettings
            // 
            this.MnuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.MnuAlwaysOnTop});
            this.MnuSettings.Name = "MnuSettings";
            this.MnuSettings.Size = new System.Drawing.Size(61, 20);
            this.MnuSettings.Text = "Settings";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // MnuAlwaysOnTop
            // 
            this.MnuAlwaysOnTop.Name = "MnuAlwaysOnTop";
            this.MnuAlwaysOnTop.Size = new System.Drawing.Size(180, 22);
            this.MnuAlwaysOnTop.Text = "Always on Top";
            this.MnuAlwaysOnTop.Click += new System.EventHandler(this.MnuAlwaysOnTop_Click);
            // 
            // MnuAbout
            // 
            this.MnuAbout.Name = "MnuAbout";
            this.MnuAbout.Size = new System.Drawing.Size(52, 20);
            this.MnuAbout.Text = "About";
            this.MnuAbout.Click += new System.EventHandler(this.MnuAbout_Click);
            // 
            // MnuCopyImage
            // 
            this.MnuCopyImage.Name = "MnuCopyImage";
            this.MnuCopyImage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.MnuCopyImage.Size = new System.Drawing.Size(47, 20);
            this.MnuCopyImage.Text = "Copy";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 536);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UsbCam";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConCircleDiameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConAngle)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LabCircleDiameter;
        private System.Windows.Forms.Label LabAngle;
        private System.Windows.Forms.TrackBar ConCircleDiameter;
        private System.Windows.Forms.TrackBar ConAngle;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MnuCamera;
        private System.Windows.Forms.ToolStripMenuItem MnuSettings;
        private System.Windows.Forms.ToolStripMenuItem MnuAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MnuExit;
        private System.Windows.Forms.ToolStripMenuItem MnuAlwaysOnTop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MnuCopyImage;
    }
}

