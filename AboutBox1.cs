using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using IDEA.UniLib.Extensions;

namespace UsbCam
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();

            this.Text = $"About {AssemblyTitle}";
            this.labelProductName.Text = assembly.GetProduct();
            this.labelVersion.Text = $"Version {assembly.GetVersion()}";
            this.labelCopyright.Text = assembly.GetCopyright();
            this.labelCompanyName.Text = assembly.GetCompany();
            this.textBoxDescription.Text = assembly.GetDescription();

            linkLabel1.LinkClicked += (sender, e) =>
            {
                try
                {
                    //V .NET (Core) je pro otevření URL nutné UseShellExecute.
                    Process.Start(new ProcessStartInfo(linkLabel1.Text) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.InnerMessages(), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
        }

        public string AssemblyTitle
        {
            get
            {
                var title = Assembly.GetExecutingAssembly().GetTitle();

                return string.IsNullOrEmpty(title) ? Assembly.GetExecutingAssembly().GetShortName() : title;
            }
        }
    }
}
