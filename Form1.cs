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


namespace UsbCam
{
    public partial class Form1 : Form
    {
        private int Angle = 0;
        private int AngleMark = 0;

        private int CircleDiameter = 0;

        private VideoCaptureDevice VideoSource;

        private Pen CrossPen = null;
        private Pen CirclePen = null;
        private Pen AngleMasterPen = null;
        private Pen AngleSlavePen = null;
        private Pen AngleMarkPen = null;

        public Form1()
        {
            InitializeComponent();

            CrossPen = new Pen(Color.White, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash};
            CirclePen = new Pen(Color.Yellow, 1);
            AngleMasterPen = new Pen(Color.Yellow, 1);
            AngleSlavePen = new Pen(Color.Gray, 1);
            AngleMarkPen = new Pen(Color.Yellow, 1);

            ConCircleDiameter.Value = CircleDiameter;
            ConAngle.Value = Angle;

            pictureBox.BackColor = Color.Gray;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            this.Shown += this.Form1_Shown;
            this.MenuCameraList.Opened += this.MenuCameraList_Opened;
            this.MenuCameraList.ItemClicked += this.MenuCameraList_ItemClicked;

            this.ConCircleDiameter.KeyDown += this.ConCircleDiameter_KeyDown;

            this.ConAngle.Scroll += this.ConAngle_Scroll;
            this.ConAngle.KeyDown += this.ConAngle_KeyDown;

            this.FormClosing += this.Form1_FormClosing;
            this.Load += (sender, e) => this.Bounds = UserAppRegistryKey.GetData(this.Name, this.Bounds);
        }

        #region Helpers

        private RegistryKey UserAppRegistryKey => Assembly.GetEntryAssembly().GetUserAppRegistryKey();

        /// <summary>
        /// Aktuaizace menu se seznamem kamer.
        /// </summary>
        private void AddCameraListToMenu(VideoCaptureDevice videoSource)
        {
            MenuCameraList.Items.Clear();

            string currentMoniker = videoSource?.Source;

            MenuCameraList.Items.AddRange(
                new FilterInfoCollection(FilterCategory.VideoInputDevice).Cast<FilterInfo>().Select(camera =>
                    new ToolStripMenuItem
                    {
                        Text = camera.Name,
                        Checked = camera.MonikerString.Equals(currentMoniker),
                        Tag = camera.MonikerString
                    }
                ).ToArray());


            if (MenuCameraList.Items.Count == 0)
            {
                MenuCameraList.Items.Add("-");
            }
        }

        /// <summary>
        /// Spuštění kamery
        /// </summary>
        /// <param name="monikerString"></param>
        private void StartCamera(string monikerString)
        {
            VideoSource = new VideoCaptureDevice(monikerString);
            VideoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
            VideoSource.Start();
        }

        /// <summary>
        /// Vypnutí kamery
        /// </summary>
        private void StopCamera()
        {
            if (VideoSource != null && VideoSource.IsRunning)
            {
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
                DrawCross(graphics, CrossPen, centerX, centerY, width, height);
                DrawCircle(graphics, CirclePen, centerX, centerY, 2 * CircleDiameter);
                DrawAngleCross(graphics, AngleMasterPen, AngleSlavePen, Angle, centerX, centerY, width * 2);

                if (AngleMark > 0)
                {
                    DrawAngleLine(graphics, AngleMarkPen, AngleMark, centerX, centerY, width * 2);
                }
                
                DrawArc(graphics, AngleMasterPen, Angle, AngleMark, centerX, centerY, 100);
            }
        }

        private static void DrawArc(Graphics g, Pen pen, int startAngle, int endAngle, int x, int y, int size)
        {
            var arcRect = new Rectangle(x-size/2, y-size/2, size, size);

            g.DrawArc(pen, arcRect, 360 - startAngle, startAngle - endAngle);
        }

        /// <summary>
        /// Vykreslí kříž
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static void DrawCross(Graphics g, Pen pen, int x, int y, int width, int height)
        {
            g.DrawLine(pen, 0, y, width, y);
            g.DrawLine(pen, x, 0, x, height);
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
        private static void DrawAngleLine(Graphics g, Pen pen, int angle, int x, int y, int length)
        {
            //--- Výpočet koncového bodu úsečky na základě úhlu a délky
            double angleInRadians = angle * Math.PI / 180; // převod úhlu z stupňů na radiány

            int dx = (int)(length * Math.Cos(angleInRadians)); // výpočet x-ové souřadnice koncového bodu
            int dy = (int)(length * Math.Sin(angleInRadians)); // výpočet y-ové souřadnice koncového bodu
            //---

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
        private static void DrawAngleCross(Graphics g, Pen pen, Pen penSlave, int angle, int x, int y, int length)
        {
            //--- kreslíme úhel
            if (angle > 0)
            {
                //--- Výpočet koncového bodu úsečky na základě úhlu a délky
                double angleInRadians = angle * Math.PI / 180; // převod úhlu z stupňů na radiány

                int dx = (int)(length * Math.Cos(angleInRadians)); // výpočet x-ové souřadnice koncového bodu
                int dy = (int)(length * Math.Sin(angleInRadians)); // výpočet y-ové souřadnice koncového bodu
                //---

                g.DrawLine(pen, x, y, x + dx, y - dy);

                g.DrawLine(penSlave, x, y, x - dx, y + dy);
                g.DrawLine(penSlave, x, y, x + dy, y + dx);
                g.DrawLine(penSlave, x, y, x - dy, y - dx);
            }
            //---
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
                    AngleMark = 0;
                    ConAngle.Value = 0;
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
        /// Otevře se menu s kamerami
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuCameraList_Opened(object sender, EventArgs e)
        {
            AddCameraListToMenu(VideoSource);
        }

        /// <summary>
        /// Obsluha vybere kameru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuCameraList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            StopCamera();
            StartCamera(e.ClickedItem.Tag.ToString());
        }

        /// <summary>
        /// Po startu aplikace je focus na úhlu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            MenuCameraList.Show(pictureBox, 10, 10);
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

            CrossPen.Dispose();
            CirclePen.Dispose();
            AngleMasterPen.Dispose();
            AngleSlavePen.Dispose();
            AngleMarkPen.Dispose();
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

        #endregion

    }
}
