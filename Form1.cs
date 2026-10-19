using AForge.Video.DirectShow;
using AForge.Video;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using IDEA.UniLib.Extensions;

namespace UsbCam;

public partial class Form1 : Form
{

    #region Values

    private int Angle = 0;
    private int AngleMark = 0;

    private int CircleDiameter = 0;

    private VideoCaptureDevice VideoSource;

    /// <summary>
    /// Příznak, že UI ještě nezpracovalo předchozí snímek (snímky navíc zahazujeme).
    /// </summary>
    private int FramePending = 0;

    /// <summary>
    /// Timer periodického snímání do souboru.
    /// </summary>
    private readonly System.Windows.Forms.Timer PeriodicalTimer = new System.Windows.Forms.Timer();

    /// <summary>
    /// Uložené nastavení, otevřené po celou dobu běhu formuláře.
    /// </summary>
    private readonly AppSettings Settings = new AppSettings();

    private PenSetDict PenSetDict = new PenSetDict();
    private volatile PenSet CurrentPenSet = null;

    #endregion

    public Form1()
    {
        InitializeComponent();

        var assembly = Assembly.GetEntryAssembly();

        this.Text = $"{assembly.GetTitle()} ({assembly.GetVersion()})";

        PenSetDict.DefaultInit();
        ShowPenSetDictToMenu();
        SetPenSet(Settings.PenSet);

        CircleDiameter = Settings.CircleDiameter;
        Angle = Settings.Angle;
        AngleMark = Settings.AngleMark;

        ConCircleDiameter.Value = CircleDiameter;
        ConAngle.Value = Angle;

        MnuNormal.Tag = PictureBoxSizeMode.Normal;
        MnuCenter.Tag = PictureBoxSizeMode.CenterImage;
        MnuZoom.Tag = PictureBoxSizeMode.Zoom;
        MnuSizeMode.DropDownItemClicked += (sender, e) => SetSizeMode((PictureBoxSizeMode)e.ClickedItem.Tag);

        SetSizeMode(Settings.SizeMode);

        pictureBox.BackColor = Color.FromArgb(30,30,30);

        this.KeyPreview = true;

        this.Shown += this.Form1_Shown;
        this.Load += (sender, e) => ApplyDpi();

        this.MnuCamera.DropDownOpening += (sender, e) => AddCameraListToMenu(VideoSource);
        this.MnuCamera.DropDownItemClicked += (sender, e) => StartCamera($"{e.ClickedItem.Tag}");

        this.ConCircleDiameter.ValueChanged += (sender, e) => CircleDiameter = (int)ConCircleDiameter.Value;
        this.ConCircleDiameter.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Escape) ConCircleDiameter.Value = 0; };

        this.ConAngle.ValueChanged += this.ConAngle_ValueChanged;
        this.ConAngle.KeyDown += this.ConAngle_KeyDown;

        this.ButSetMark.Click += (sender, e) =>
        {
            AngleMark = Angle;

            ShowLabAngle();
        };

        this.FormClosing += this.Form1_FormClosing;
        this.Load += (sender, e) => this.Bounds = GetVisibleBounds(Settings.GetWindowBounds(this.Bounds));
        this.KeyDown += this.Form1_KeyDown;

        this.ButImageCopy.Click += (sender, e) => CopyImage();
        this.ButImageSave.Click += (sender, e) => SaveImageToFile();
        this.ButTakePeriodical.Click += (sender, e) => SetPeriodical(!PeriodicalTimer.Enabled);

        PeriodicalTimer.Tick += (sender, e) => PeriodicalTick();

        SetAlwaysOnTop(Settings.AlwaysOnTop);
        SetDarkMode(Program.DarkMode, false);


        ShowLabAngle();
    }

    #region DPI

    /// <summary>Velikost ikon tlačítek při 100 % (96 DPI).</summary>
    private const int ICON_SIZE = 32;

    /// <summary>Výchozí písmo menu při 100 % (96 DPI).</summary>
    private readonly Font MenuBaseFont = new Font("Segoe UI", 9F);

    /// <summary>
    /// ToolStrip (menu) ani obrázky tlačítek se při přesunu na monitor s jiným DPI samy nepřizpůsobí,
    /// proto je přepočítáme podle aktuálního DPI okna.
    /// </summary>
    private void ApplyDpi()
    {
        if (IsDisposed)
            return;

        var scale = DeviceDpi / 96f;

        //Původní písmo ani obrázky neuvolňujeme: ToolStrip a jeho položky je mohou ještě používat (Dispose by způsobil ArgumentException při kreslení).
        //Jde o pár malých objektů při každé změně DPI.
        menuStrip1.Font = new Font(MenuBaseFont.FontFamily, MenuBaseFont.Size * scale, MenuBaseFont.Style, MenuBaseFont.Unit);

        var size = (int)Math.Round(ICON_SIZE * scale);

        ButTakePeriodical.Image = Scale(Properties.Resources.video, size);
        ButImageSave.Image = Scale(Properties.Resources.file, size);
        ButImageCopy.Image = Scale(Properties.Resources.camera, size);

        //Po přesunu na monitor s jiným DPI se v tmavém režimu mohou ovládací prvky vykreslit s černým písmem / pozadím,
        //proto barvu písma nastavíme výslovně a vynutíme přepočet rozvržení i překreslení.
        if (Program.DarkMode)
        {
            ForeColor = Color.White;
        }

        PerformLayout();
        Invalidate(true);
        Update();
    }

    private static Bitmap Scale(Image source, int size)
    {
        var bitmap = new Bitmap(size, size);

        using (var g = Graphics.FromImage(bitmap))
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(source, 0, 0, size, size);
        }

        return bitmap;
    }

    protected override void OnDpiChanged(DpiChangedEventArgs e)
    {
        base.OnDpiChanged(e);

        //Framework po události ještě přepočítává velikosti, proto použijeme výsledné DPI až po dokončení zprávy.
        BeginInvoke(ApplyDpi);
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Uložené umístění okna může ležet mimo aktuální monitory (odpojený monitor, změna rozlišení).
    /// </summary>
    private static Rectangle GetVisibleBounds(Rectangle bounds)
    {
        return Screen.AllScreens.Any(it => it.WorkingArea.IntersectsWith(bounds)) && bounds.Width > 100 && bounds.Height > 100
            ? bounds
            : new Rectangle(Screen.PrimaryScreen.WorkingArea.Location, new Size(733, 536));
    }

    private void SetSizeMode(string sizeModeString)
    {
        if (Enum.TryParse(sizeModeString, out PictureBoxSizeMode sizeMode))
        {
            SetSizeMode(sizeMode);
        }
        else
        {
            SetSizeMode(PictureBoxSizeMode.CenterImage);
        }
    }

    private void SetSizeMode(PictureBoxSizeMode sizeMode)
    {
        pictureBox.SizeMode = sizeMode;

        Settings.SizeMode = sizeMode.ToString();

        MnuNormal.Checked = sizeMode.Equals(MnuNormal.Tag);
        MnuCenter.Checked = sizeMode.Equals(MnuCenter.Tag);
        MnuZoom.Checked = sizeMode.Equals(MnuZoom.Tag);
    }

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

        try
        {
            VideoSource = new VideoCaptureDevice(monikerString);
            VideoSource.NewFrame += VideoSource_NewFrame;
            VideoSource.VideoSourceError += (sender, e) => Debug.WriteLine($"Camera error: {e.Description}");
            VideoSource.Start();

            Settings.Device = monikerString;
        }
        catch (Exception ex)
        {
            VideoSource = null;

            MessageBox.Show(this, ex.InnerMessages(), "Start Camera", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Vypnutí kamery
    /// </summary>
    private void StopCamera()
    {
        var videoSource = VideoSource;

        VideoSource = null;

        if (videoSource == null)
            return;

        videoSource.NewFrame -= VideoSource_NewFrame;

        if (videoSource.IsRunning)
        {
            //Nečekáme na vlákno kamery nekonečně dlouho - snímky se do UI předávají přes BeginInvoke, takže k deadlocku nedojde.
            videoSource.SignalToStop();
            videoSource.WaitForStop();
        }
    }

    /// <summary>
    /// Zobrazení a výpočet úhlů do labelu.
    /// </summary>
    private void ShowLabAngle()
    {
        var angle = Math.Abs(Angle - AngleMark);
        LabAngle.Text = $"{AngleMark}° - {Angle}° = {angle}° / {360-angle}°";
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

        SetPenSet(penSet);
    }

    private void SetPenSet(string key)
    {
        SetPenSet(PenSetDict.GetSet(key));
    }

    private void SetPenSet(PenSet penSet)
    {
        CurrentPenSet = penSet;

        Settings.PenSet = CurrentPenSet.Key;
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

        Settings.AlwaysOnTop = alwaysOnTop;
    }

    /// <summary>
    /// Nastaví tmavý / světlý režim.
    /// </summary>
    /// <param name="darkMode">true = tmavý režim.</param>
    /// <param name="apply">Příznak, zda se má režim aplikovat (při startu je již aplikovaný z Program.Main).</param>
    private void SetDarkMode(bool darkMode, bool apply = true)
    {
        MnuDarkMode.Checked = darkMode;

        if (apply)
        {
            Program.SetDarkMode(darkMode);

            //Windows Forms neumí změnit barevný režim již otevřených oken, nový režim se projeví po restartu.
            if (MessageBox.Show(this, "The color mode will be applied after restarting the application.\r\nRestart now?", "Dark Mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RestartApplication();
            }
        }
    }

    /// <summary>
    /// Spustí novou instanci aplikace a tuto zavře.
    /// </summary>
    private void RestartApplication()
    {
        try
        {
            Process.Start(new ProcessStartInfo(Environment.ProcessPath) { UseShellExecute = false });
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.InnerMessages(), "Restart", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Vrací kopii aktuálně zobrazeného snímku (i s nakreslenými pomůckami) nebo null.
    /// </summary>
    private Bitmap TakePicture() => pictureBox.Image is Bitmap bitmap ? new Bitmap(bitmap) : null;

    /// <summary>
    /// Kopie obrázku do clipboardu
    /// </summary>
    private void CopyImage()
    {
        try
        {
            using (var image = TakePicture())
            {
                if (image == null)
                {
                    MessageBox.Show(this, "There is no image from the camera.", "Copy Image to Clipboard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Clipboard.SetImage(image);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.InnerMessages(), "Copy Image to Clipboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Uloží snímek do složky zadané jako 'target' pod názvem podle aktuálního času.
    /// </summary>
    /// <returns>Cesta k uloženému souboru nebo null, pokud není co ukládat.</returns>
    private string SaveImage(string target)
    {
        using (var image = TakePicture())
        {
            if (image == null)
                return null;

            Directory.CreateDirectory(target);

            var path = Path.Combine(target, $"UsbCam_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png");

            image.Save(path, ImageFormat.Png);

            return path;
        }
    }

    /// <summary>
    /// Uložení snímku do souboru (Settings.FileTarget).
    /// </summary>
    private void SaveImageToFile()
    {
        try
        {
            if (SaveImage(Settings.FileTarget) == null)
            {
                MessageBox.Show(this, "There is no image from the camera.", "Save Image to File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.InnerMessages(), "Save Image to File", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Zapne / vypne periodické ukládání snímků (Settings.PeriodicalTarget, Settings.PeriodicalTargetInterval).
    /// </summary>
    private void SetPeriodical(bool enabled)
    {
        PeriodicalTimer.Enabled = false;

        if (enabled)
        {
            PeriodicalTimer.Interval = Settings.PeriodicalTargetInterval * 1000;
            PeriodicalTimer.Enabled = true;
        }

        ButTakePeriodical.BackColor = enabled ? Color.IndianRed : SystemColors.Control;
        ButTakePeriodical.UseVisualStyleBackColor = !enabled;

        toolTip1.SetToolTip(ButTakePeriodical, enabled
            ? $"Periodical take is ON ({Settings.PeriodicalTargetInterval} s).\r\nClick to stop."
            : "Periodical take a picture to the file\r\n(see Settings).");
    }

    private void PeriodicalTick()
    {
        try
        {
            SaveImage(Settings.PeriodicalTarget);
        }
        catch (Exception ex)
        {
            //Při chybě (plný disk, nedostupná složka...) periodické snímání vypneme, ať nezahltíme uživatele hláškami.
            SetPeriodical(false);

            MessageBox.Show(this, ex.InnerMessages(), "Periodical Take", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #endregion

    #region GUI response

    /// <summary>
    /// Obsluha změny úhlu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ConAngle_ValueChanged(object sender, EventArgs e)
    {
        var temp = (int)ConAngle.Value;

        if (temp < 0)
        {
            ConAngle.Value = 359;
            this.Angle = 359;
        }
        else if (temp >= 360)
        {
            ConAngle.Value = 0;
            this.Angle = 0;
        }
        else
        {
            this.Angle = temp;
        }

        ShowLabAngle();
    }

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

                ShowLabAngle();
                break;
        }
    }


    /// <summary>
    /// Po startu aplikace je focus na úhlu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_Shown(object sender, EventArgs e)
    {
        var device = Settings.Device;

        StartCamera(device);

        ConAngle.Focus();
    }

    /// <summary>
    /// Zavření okna uvolní prostředky.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);

        PeriodicalTimer.Dispose();
        StopCamera();

        pictureBox.Image?.Dispose();
        pictureBox.Image = null;

        Settings.Dispose();
    }

    /// <summary>
    /// Akce při každém snímku (běží ve vlákně kamery).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="eventArgs"></param>
    private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        //UI ještě nezobrazilo předchozí snímek, tento zahodíme.
        if (Interlocked.Exchange(ref FramePending, 1) == 1)
            return;

        Bitmap bitmap = null;

        try
        {
            bitmap = (Bitmap)eventArgs.Frame.Clone(); // Získání nového snímku z kamery
            //Nastavení se může během kreslení změnit v UI vlákně, proto kreslíme z neměnné kopie.
            var overlay = new OverlayState(CurrentPenSet, Angle, AngleMark, CircleDiameter);


            OverlayRenderer.Draw(bitmap, overlay); //domalujeme pár věcí do výstupu

            BeginInvoke(() => ShowFrame(bitmap)); //Zobrazíme v Pictureboxu (ve vlákně UI)
        }
        catch (Exception ex) when (ex is InvalidOperationException || ex is ObjectDisposedException || ex is ArgumentException || ex is System.Runtime.InteropServices.ExternalException)
        {
            //Formulář se zavírá nebo snímek nelze zpracovat.
            bitmap?.Dispose();

            Interlocked.Exchange(ref FramePending, 0);
        }
    }

    /// <summary>
    /// Zobrazí snímek a uvolní předchozí (jinak by docházelo k úniku GDI paměti).
    /// </summary>
    private void ShowFrame(Bitmap bitmap)
    {
        Interlocked.Exchange(ref FramePending, 0);

        if (IsDisposed)
        {
            bitmap.Dispose();
            return;
        }

        var old = pictureBox.Image;

        pictureBox.Image = bitmap;

        old?.Dispose();
    }

    /// <summary>
    /// Uložíme nastavení okna.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        Settings.SetWindowBounds((this.WindowState == FormWindowState.Normal) ? this.Bounds : this.RestoreBounds);
        Settings.Angle = Angle;
        Settings.AngleMark = AngleMark;
        Settings.CircleDiameter = CircleDiameter;
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control)
        {
            SetPenSet(e.KeyCode);
        }
    }

    private void MnuExit_Click(object sender, EventArgs e) => this.Close();


    /// <summary>
    /// Nastaví formulář na 'Vždy navrchu'
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MnuAlwaysOnTop_Click(object sender, EventArgs e) => SetAlwaysOnTop(!MnuAlwaysOnTop.Checked);

    /// <summary>
    /// Přepne tmavý / světlý režim.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MnuDarkMode_Click(object sender, EventArgs e) => SetDarkMode(!MnuDarkMode.Checked);

    /// <summary>
    /// Zobrazí informace o aplikaci.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MnuAbout_Click(object sender, EventArgs e)
    {
        using (var dialog = new AboutBox1 { TopMost = this.TopMost })
        {
            dialog.ShowDialog(this);
        }
    }

    /// <summary>
    /// Dialog pro nastavení cíle snímání.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MnuTargetSettings_Click(object sender, EventArgs e)
    {
        using (var dialog = new FrmTargetSettings()
        {
            Owner = this,
            TopMost = this.TopMost, //Pokud by byl dialog TopMost, překryl by Settings...
            StartPosition = FormStartPosition.CenterParent,
            FileTarget = Settings.FileTarget,
            PeriodicalTarget = Settings.PeriodicalTarget,
            PeriodicalTargetInterval = Settings.PeriodicalTargetInterval
        })
        {
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            Settings.FileTarget = dialog.FileTarget;
            Settings.PeriodicalTarget = dialog.PeriodicalTarget;
            Settings.PeriodicalTargetInterval = dialog.PeriodicalTargetInterval;

            //Běžící periodické snímání převezme nový interval.
            if (PeriodicalTimer.Enabled)
            {
                SetPeriodical(true);
            }
        }
    }

    #endregion

}
