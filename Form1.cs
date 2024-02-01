using AForge.Video.DirectShow;
using AForge.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Forms.Design;
using IDEA.UniLib.Extensions;
using System.Reflection;
using Microsoft.Win32;
using System.Runtime.Remoting.Channels;


namespace UsbCam
{
    public partial class Form1 : Form
    {
        private const string DEVICE_KEY = "Device";
        private const string ALWAYS_ON_TOP_KEY = "AlwaysOnTop";
        private const string PEN_SET_KEY = "PenSet";

        private int Angle = 0;
        private int AngleMark = 0;

        private int CircleDiameter = 0;

        private VideoCaptureDevice VideoSource;

        private PenSetDict PenSetDict = new PenSetDict();
        private PenSet CurrentPenSet = null; 

        public Form1()
        {
            InitializeComponent();

            var assembly = Assembly.GetEntryAssembly();

            this.Text = $"{assembly.GetTitle()} ({assembly.GetVersion()})";

            PenSetDict.DefaultInit();
            ShowPenSetDictToMenu();
            SetPenSet(UserAppRegistryKey.GetData(PEN_SET_KEY, ""));

            ConCircleDiameter.Value = CircleDiameter;
            ConAngle.Value = Angle;

            pictureBox.BackColor = Color.Gray;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            
            this.KeyPreview = true;

            this.Shown += this.Form1_Shown;

            this.MnuCamera.DropDownOpening += (sender, e) => AddCameraListToMenu(VideoSource);
            this.MnuCamera.DropDownItemClicked += (sender, e) => StartCamera($"{e.ClickedItem.Tag}");

            this.ConCircleDiameter.KeyDown += this.ConCircleDiameter_KeyDown;

            this.ConAngle.Scroll += this.ConAngle_Scroll;
            this.ConAngle.KeyDown += this.ConAngle_KeyDown;

            this.FormClosing += this.Form1_FormClosing;
            this.Load += (sender, e) => this.Bounds = UserAppRegistryKey.GetData(this.Name, this.Bounds);
            this.KeyDown += this.Form1_KeyDown;

            this.MnuCopyImage.Click += (sender, e) => CopyImage();

            SetAlwaysOnTop(UserAppRegistryKey.GetData(ALWAYS_ON_TOP_KEY, false));
        }

        #region Helpers

        private RegistryKey UserAppRegistryKey => Assembly.GetEntryAssembly().GetUserAppRegistryKey();

        /// <summary>
        /// Aktuaizace menu se seznamem kamer.
        /// </summary>
        private void AddCameraListToMenu(VideoCaptureDevice videoSource)
        {
            var currentMoniker = videoSource?.Source;

            var toDelete = MnuCamera.DropDownItems.Cast<ToolStripItem>().Where(it => it.Tag != null).ToList();

            toDelete.ForEach(it => MnuCamera.DropDownItems.Remove(it));

            foreach (var camera in new FilterInfoCollection(FilterCategory.VideoInputDevice).Cast<FilterInfo>().OrderByDescending(it=>it.Name))
            {
                var menuItem = new ToolStripMenuItem()
                {
                    Text = camera.Name,
                    Checked = camera.MonikerString.Equals(currentMoniker),
                    Tag = camera.MonikerString,
                    
                };

                MnuCamera.DropDownItems.Insert(0, menuItem);
            }
        }

        /// <summary>
        /// Spuštění kamery
        /// </summary>
        /// <param name="monikerString"></param>
        private void StartCamera(string monikerString)
        {
            //Nemáme identifikátor kamery
            if (string.IsNullOrEmpty(monikerString))
                return;

            StopCamera();

            VideoSource = new VideoCaptureDevice(monikerString);
            VideoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            VideoSource.Start();

            UserAppRegistryKey.SetData(DEVICE_KEY, monikerString);
        }

        /// <summary>
        /// Vypnutí kamery
        /// </summary>
        private void StopCamera()
        {
            if (VideoSource != null && VideoSource.IsRunning)
            {
                VideoSource.NewFrame -= new NewFrameEventHandler(VideoSource_NewFrame);

                VideoSource.SignalToStop();
                VideoSource.WaitForStop();
                VideoSource = null;
            }
        }

        /// <summary>
        /// Zobrazení a výpočet úhlů do labelu.
        /// </summary>
        private void ShowLabAngle()
        {
            var angle = Math.Abs(Angle - AngleMark);
            LabAngle.Text = $"Angle {AngleMark}° - {Angle}° = {angle}° ({360-angle}°)";
        }

        /// <summary>
        /// Zobrazení kružnice do labelu
        /// </summary>
        private void ShowLabCircle()
        {
            LabCircleDiameter.Text = $"Circle {CircleDiameter}";
        }

        private void DrawEtc(Bitmap bitmap)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;
            var centerX = width / 2;
            var centerY = height / 2;

            using (var graphics = Graphics.FromImage(bitmap))
            {
                DrawAngleCross(graphics, CurrentPenSet.PenBg, CurrentPenSet.CrossPen, CurrentPenSet.CrossPen, 0, centerX, centerY, width * 2);
                DrawCircle(graphics, CurrentPenSet.CirclePen, centerX, centerY, 2 * CircleDiameter);
                
                if (Angle > 0)
                {
                    DrawAngleCross(graphics, CurrentPenSet.PenBg, CurrentPenSet.AngleMasterPen, CurrentPenSet.AngleSlavePen, Angle, centerX, centerY, width * 2);
                }

                if (AngleMark > 0)
                {
                    DrawAngleLine(graphics, CurrentPenSet.PenBg, CurrentPenSet.AngleMarkPen, AngleMark, centerX, centerY, width * 2);
                }
                
                DrawArc(graphics, CurrentPenSet.PenBg, CurrentPenSet.AngleArcPen, Angle, AngleMark, centerX, centerY, 100);
            }
        }

        private static void DrawArc(Graphics g, Pen penBg, Pen pen, int startAngle, int endAngle, int x, int y, int size)
        {
            var arcRect = new Rectangle(x-size/2, y-size/2, size, size);

            g.DrawArc(penBg, arcRect, 360 - startAngle, startAngle - endAngle);
            g.DrawArc(pen, arcRect, 360 - startAngle, startAngle - endAngle);
        }

        /// <summary>
        /// Kreslíme kružnici
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="d"></param>
        private static void DrawCircle(Graphics g, Pen pen, int x, int y, int d)
        {
            if (d >0)
            {
                g.DrawEllipse(pen, x - d / 2, y - d / 2, d, d);
            }
        }

        /// <summary>
        /// Nakreslí čáru pod úhlem
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="angle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="length"></param>
        private static void DrawAngleLine(Graphics g, Pen penBg, Pen pen, int angle, int x, int y, int length)
        {
            //--- Výpočet koncového bodu úsečky na základě úhlu a délky
            double angleInRadians = angle * Math.PI / 180; // převod úhlu z stupňů na radiány

            int dx = (int)(length * Math.Cos(angleInRadians)); // výpočet x-ové souřadnice koncového bodu
            int dy = (int)(length * Math.Sin(angleInRadians)); // výpočet y-ové souřadnice koncového bodu
            //---

            g.DrawLine(penBg, x, y, x + dx, y - dy);
            g.DrawLine(pen, x, y, x + dx, y - dy);
        }

        /// <summary>
        /// Kreslíme kříž pod úhlem.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="penSlave"></param>
        /// <param name="angle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        private static void DrawAngleCross(Graphics g, Pen penBg, Pen pen, Pen penSlave, int angle, int x, int y, int length)
        {
            DrawAngleLine(g, penBg, pen, angle, x, y, length);
            DrawAngleLine(g, penBg, penSlave, angle + 90, x, y, length);
            DrawAngleLine(g, penBg, penSlave, angle + 180, x, y, length);
            DrawAngleLine(g, penBg, penSlave, angle + 270, x, y, length);
        }

        private void ShowPenSetDictToMenu()
        {
            foreach (var item in PenSetDict.Values.OrderByDescending(it => it.Name))
            {
                var menuItem = new ToolStripMenuItem()
                {
                    Text = item.Name,
                    Tag = item
                };

                MnuSettings.DropDownItems.Insert(0, menuItem);

                menuItem.Click += (sender, e) => SetPenSet(item.Key);
            }
        }

        private void SetPenSet(Keys KeyCode)
        {
            var penSet = PenSetDict.GetSet(KeyCode);

            if (penSet == null)
                return;

            SetPenSet(PenSetDict.GetSet(KeyCode));
        }

        private void SetPenSet(string key)
        {
            SetPenSet(PenSetDict.GetSet(key));
        }

        private void SetPenSet(PenSet penSet)
        {
            CurrentPenSet = penSet;

            UserAppRegistryKey.SetData(PEN_SET_KEY, CurrentPenSet.Key);
            MnuSettings.DropDownItems.Cast<ToolStripItem>().Where(it => it.Tag is PenSet).Select(it => it as ToolStripMenuItem).ToList().ForEach(it => it.Checked = it.Tag == CurrentPenSet);
        }


        /// <summary>
        /// Nastaví Always on Top
        /// </summary>
        /// <param name="alwaysOnTop"></param>
        private void SetAlwaysOnTop(bool alwaysOnTop)
        {
            this.TopMost = alwaysOnTop;
            MnuAlwaysOnTop.Checked = alwaysOnTop;

            UserAppRegistryKey.SetData(ALWAYS_ON_TOP_KEY, alwaysOnTop);
        }

        /// <summary>
        /// Kopie obrázku do clipboardu
        /// </summary>
        private void CopyImage()
        {
            try
            {
                Clipboard.SetImage(pictureBox.Image);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerMessages(), "Copy Image to Clipboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        #endregion

        #region GUI response

        /// <summary>
        /// Enter nastaví značku úhlu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConAngle_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    AngleMark = Angle;

                    ShowLabAngle();
                    break;
                case Keys.Escape:
                    if (AngleMark == 0)
                    { 
                        ConAngle.Value = 0;
                    }

                    AngleMark = 0;
                        
                    break;
            }
        }

        /// <summary>
        /// Zajištění cyklického procházení
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConAngle_Scroll(object sender, EventArgs e)
        {
            if (ConAngle.Value == ConAngle.Maximum)
            {
                ConAngle.Value = 0;

                return;
            }

            if (ConAngle.Value == ConAngle.Minimum)
            {
                ConAngle.Value = 359;

                return;
            }
        }

        /// <summary>
        /// Po startu aplikace je focus na úhlu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            var device = UserAppRegistryKey.GetData(DEVICE_KEY, string.Empty);
            
            StartCamera(device);

            ConAngle.Focus();
        }

        /// <summary>
        /// Zavření okna uvolní prostředky.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e); // Uvolnění prostředků při zavření okna

            StopCamera();
        }

        /// <summary>
        /// Obsluha změny úhlu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConAngle_ValueChanged(object sender, EventArgs e)
        {
            Angle = ConAngle.Value;

            ShowLabAngle();
        }

        /// <summary>
        /// Obsluha změny kružnice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConCircleDiameter_ValueChanged(object sender, EventArgs e)
        {
            CircleDiameter = ConCircleDiameter.Value;

            ShowLabCircle();
        }

        private void ConCircleDiameter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ConCircleDiameter.Value = 0;
            }
        }

        /// <summary>
        /// Akce při každém snímku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            var bitmap = (Bitmap)eventArgs.Frame.Clone(); // Získání nového snímku z kamery

            DrawEtc(bitmap); //domalujeme pár věcí do výstupu

            pictureBox.Image = bitmap; //Zobrazíme v Pictureboxu
        }

        /// <summary>
        /// Uložíme nastavení okna.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserAppRegistryKey.SetData(this.Name, (this.WindowState == FormWindowState.Normal) ? this.Bounds : this.RestoreBounds);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            SetPenSet(e.KeyCode);
        }

        private void MnuExit_Click(object sender, EventArgs e) => this.Close();


        /// <summary>
        /// Nastaví formulář na 'Vždy navrchu'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuAlwaysOnTop_Click(object sender, EventArgs e) => SetAlwaysOnTop(!MnuAlwaysOnTop.Checked);

        /// <summary>
        /// Zobrazí informace o aplikaci.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuAbout_Click(object sender, EventArgs e)
        {
            var dialog = new AboutBox1();
            dialog.TopMost = this.TopMost;

            dialog.ShowDialog();
        }

        #endregion
    }
}
