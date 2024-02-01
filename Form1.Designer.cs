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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButSetMark = new System.Windows.Forms.Button();
            this.ConAngle = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ConCircleDiameter = new System.Windows.Forms.NumericUpDown();
            this.LabCircleDiameter = new System.Windows.Forms.Label();
            this.LabAngle = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MnuCamera = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuSizeMode = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuCenter = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ButImageCopy = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConCircleDiameter)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 74);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(733, 462);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.WaitOnLoad = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ButImageCopy);
            this.panel1.Controls.Add(this.ButSetMark);
            this.panel1.Controls.Add(this.ConAngle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ConCircleDiameter);
            this.panel1.Controls.Add(this.LabCircleDiameter);
            this.panel1.Controls.Add(this.LabAngle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(733, 50);
            this.panel1.TabIndex = 1;
            // 
            // ButSetMark
            // 
            this.ButSetMark.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.ButSetMark.Location = new System.Drawing.Point(282, 12);
            this.ButSetMark.Name = "ButSetMark";
            this.ButSetMark.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.ButSetMark.Size = new System.Drawing.Size(30, 29);
            this.ButSetMark.TabIndex = 5;
            this.ButSetMark.Text = "8";
            this.toolTip1.SetToolTip(this.ButSetMark, "Set guide line (Enter).");
            this.ButSetMark.UseVisualStyleBackColor = true;
            // 
            // ConAngle
            // 
            this.ConAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ConAngle.Location = new System.Drawing.Point(217, 13);
            this.ConAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.ConAngle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.ConAngle.Name = "ConAngle";
            this.ConAngle.Size = new System.Drawing.Size(56, 26);
            this.ConAngle.TabIndex = 3;
            this.ConAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(157, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Angle:";
            // 
            // ConCircleDiameter
            // 
            this.ConCircleDiameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ConCircleDiameter.Location = new System.Drawing.Point(73, 13);
            this.ConCircleDiameter.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.ConCircleDiameter.Name = "ConCircleDiameter";
            this.ConCircleDiameter.Size = new System.Drawing.Size(56, 26);
            this.ConCircleDiameter.TabIndex = 1;
            this.ConCircleDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabCircleDiameter
            // 
            this.LabCircleDiameter.AutoSize = true;
            this.LabCircleDiameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LabCircleDiameter.Location = new System.Drawing.Point(15, 15);
            this.LabCircleDiameter.Name = "LabCircleDiameter";
            this.LabCircleDiameter.Size = new System.Drawing.Size(52, 20);
            this.LabCircleDiameter.TabIndex = 0;
            this.LabCircleDiameter.Text = "Circle:";
            // 
            // LabAngle
            // 
            this.LabAngle.AutoSize = true;
            this.LabAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LabAngle.Location = new System.Drawing.Point(330, 15);
            this.LabAngle.Name = "LabAngle";
            this.LabAngle.Size = new System.Drawing.Size(21, 20);
            this.LabAngle.TabIndex = 4;
            this.LabAngle.Text = "...";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuCamera,
            this.MnuSettings,
            this.MnuAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(733, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "MnuMain";
            // 
            // MnuCamera
            // 
            this.MnuCamera.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.MnuExit});
            this.MnuCamera.Name = "MnuCamera";
            this.MnuCamera.Size = new System.Drawing.Size(60, 20);
            this.MnuCamera.Text = "&Camera";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // MnuExit
            // 
            this.MnuExit.Name = "MnuExit";
            this.MnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.MnuExit.Size = new System.Drawing.Size(180, 22);
            this.MnuExit.Text = "&Exit";
            this.MnuExit.Click += new System.EventHandler(this.MnuExit_Click);
            // 
            // MnuSettings
            // 
            this.MnuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.MnuSizeMode,
            this.toolStripSeparator2,
            this.MnuAlwaysOnTop});
            this.MnuSettings.Name = "MnuSettings";
            this.MnuSettings.Size = new System.Drawing.Size(61, 20);
            this.MnuSettings.Text = "&Settings";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // MnuSizeMode
            // 
            this.MnuSizeMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuNormal,
            this.MnuCenter,
            this.MnuZoom});
            this.MnuSizeMode.Name = "MnuSizeMode";
            this.MnuSizeMode.Size = new System.Drawing.Size(180, 22);
            this.MnuSizeMode.Text = "Size Mode";
            // 
            // MnuNormal
            // 
            this.MnuNormal.Name = "MnuNormal";
            this.MnuNormal.Size = new System.Drawing.Size(114, 22);
            this.MnuNormal.Text = "Normal";
            // 
            // MnuCenter
            // 
            this.MnuCenter.Name = "MnuCenter";
            this.MnuCenter.Size = new System.Drawing.Size(114, 22);
            this.MnuCenter.Text = "Center";
            // 
            // MnuZoom
            // 
            this.MnuZoom.Name = "MnuZoom";
            this.MnuZoom.Size = new System.Drawing.Size(114, 22);
            this.MnuZoom.Text = "Zoom";
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
            this.MnuAbout.Text = "&About";
            this.MnuAbout.Click += new System.EventHandler(this.MnuAbout_Click);
            // 
            // ButImageCopy
            // 
            this.ButImageCopy.Dock = System.Windows.Forms.DockStyle.Right;
            this.ButImageCopy.Image = global::UsbCam.Properties.Resources.camera;
            this.ButImageCopy.Location = new System.Drawing.Point(688, 4);
            this.ButImageCopy.Name = "ButImageCopy";
            this.ButImageCopy.Size = new System.Drawing.Size(41, 42);
            this.ButImageCopy.TabIndex = 6;
            this.toolTip1.SetToolTip(this.ButImageCopy, "Take a picture to the clipboard.");
            this.ButImageCopy.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 536);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UsbCam";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConCircleDiameter)).EndInit();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MnuCamera;
        private System.Windows.Forms.ToolStripMenuItem MnuSettings;
        private System.Windows.Forms.ToolStripMenuItem MnuAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MnuExit;
        private System.Windows.Forms.ToolStripMenuItem MnuAlwaysOnTop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.NumericUpDown ConCircleDiameter;
        private System.Windows.Forms.NumericUpDown ConAngle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButSetMark;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MnuSizeMode;
        private System.Windows.Forms.ToolStripMenuItem MnuNormal;
        private System.Windows.Forms.ToolStripMenuItem MnuCenter;
        private System.Windows.Forms.ToolStripMenuItem MnuZoom;
        private System.Windows.Forms.Button ButImageCopy;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

