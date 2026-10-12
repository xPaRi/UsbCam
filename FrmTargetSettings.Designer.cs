namespace UsbCam
{
    partial class FrmTargetSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTargetSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtFileTarget = new System.Windows.Forms.TextBox();
            this.TxtPeriodicalTarget = new System.Windows.Forms.TextBox();
            this.ButBrowseFileTarget = new System.Windows.Forms.Button();
            this.ConInterval = new System.Windows.Forms.NumericUpDown();
            this.ButOk = new System.Windows.Forms.Button();
            this.ButCancel = new System.Windows.Forms.Button();
            this.ButBrowsePeriodicalTarget = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ConInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Target Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Periodical Target Path:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Period:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "sec.";
            // 
            // TxtFileTarget
            // 
            this.TxtFileTarget.Location = new System.Drawing.Point(133, 12);
            this.TxtFileTarget.Name = "TxtFileTarget";
            this.TxtFileTarget.Size = new System.Drawing.Size(295, 20);
            this.TxtFileTarget.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.TxtFileTarget.TabIndex = 4;
            // 
            // TxtPeriodicalTarget
            // 
            this.TxtPeriodicalTarget.Location = new System.Drawing.Point(133, 38);
            this.TxtPeriodicalTarget.Name = "TxtPeriodicalTarget";
            this.TxtPeriodicalTarget.Size = new System.Drawing.Size(295, 20);
            this.TxtPeriodicalTarget.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.TxtPeriodicalTarget.TabIndex = 5;
            // 
            // ButBrowseFileTarget
            // 
            this.ButBrowseFileTarget.Location = new System.Drawing.Point(434, 12);
            this.ButBrowseFileTarget.Name = "ButBrowseFileTarget";
            this.ButBrowseFileTarget.Size = new System.Drawing.Size(26, 20);
            this.ButBrowseFileTarget.TabIndex = 6;
            this.ButBrowseFileTarget.Text = "…";
            this.ButBrowseFileTarget.UseVisualStyleBackColor = true;
            // 
            // ConInterval
            // 
            this.ConInterval.Location = new System.Drawing.Point(133, 66);
            this.ConInterval.Name = "ConInterval";
            this.ConInterval.Size = new System.Drawing.Size(54, 20);
            this.ConInterval.Maximum = new decimal(new int[] { 86400, 0, 0, 0 });
            this.ConInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.ConInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
            this.ConInterval.TabIndex = 8;
            // 
            // ButOk
            // 
            this.ButOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButOk.Location = new System.Drawing.Point(304, 118);
            this.ButOk.Name = "ButOk";
            this.ButOk.Size = new System.Drawing.Size(75, 31);
            this.ButOk.TabIndex = 9;
            this.ButOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButOk.Text = "&OK";
            this.ButOk.UseVisualStyleBackColor = true;
            // 
            // ButCancel
            // 
            this.ButCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButCancel.Location = new System.Drawing.Point(385, 118);
            this.ButCancel.Name = "ButCancel";
            this.ButCancel.Size = new System.Drawing.Size(75, 31);
            this.ButCancel.TabIndex = 10;
            this.ButCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButCancel.Text = "&Cancel";
            this.ButCancel.UseVisualStyleBackColor = true;
            // 
            // ButBrowsePeriodicalTarget
            // 
            this.ButBrowsePeriodicalTarget.Location = new System.Drawing.Point(434, 38);
            this.ButBrowsePeriodicalTarget.Name = "ButBrowsePeriodicalTarget";
            this.ButBrowsePeriodicalTarget.Size = new System.Drawing.Size(26, 20);
            this.ButBrowsePeriodicalTarget.TabIndex = 11;
            this.ButBrowsePeriodicalTarget.Text = "…";
            this.ButBrowsePeriodicalTarget.UseVisualStyleBackColor = true;
            // 
            // FrmTargetSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 161);
            this.Controls.Add(this.ButBrowsePeriodicalTarget);
            this.Controls.Add(this.ButCancel);
            this.Controls.Add(this.ButOk);
            this.Controls.Add(this.ConInterval);
            this.Controls.Add(this.ButBrowseFileTarget);
            this.Controls.Add(this.TxtPeriodicalTarget);
            this.Controls.Add(this.TxtFileTarget);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.AcceptButton = this.ButOk;
            this.CancelButton = this.ButCancel;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.Name = "FrmTargetSettings";
            this.Text = "Target Settings";
            ((System.ComponentModel.ISupportInitialize)(this.ConInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtFileTarget;
        private System.Windows.Forms.TextBox TxtPeriodicalTarget;
        private System.Windows.Forms.Button ButBrowseFileTarget;
        private System.Windows.Forms.NumericUpDown ConInterval;
        private System.Windows.Forms.Button ButOk;
        private System.Windows.Forms.Button ButCancel;
        private System.Windows.Forms.Button ButBrowsePeriodicalTarget;
    }
}