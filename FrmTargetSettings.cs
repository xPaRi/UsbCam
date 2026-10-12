using System;
using System.IO;
using System.Windows.Forms;

namespace UsbCam;

/// <summary>
/// Nastavení cílů pro ukládání snímků do souboru a pro periodické snímání.
/// </summary>
public partial class FrmTargetSettings : Form
{
    /// <summary>
    /// Složka pro jednorázové uložení snímku.
    /// </summary>
    [System.ComponentModel.Browsable(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public string FileTarget { get; set; }

    /// <summary>
    /// Složka pro periodické snímání.
    /// </summary>
    [System.ComponentModel.Browsable(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public string PeriodicalTarget { get; set; }

    /// <summary>
    /// Interval periodického snímání [sec].
    /// </summary>
    [System.ComponentModel.Browsable(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public int PeriodicalTargetInterval { get; set; } = 10;

    public FrmTargetSettings()
    {
        InitializeComponent();

        ButBrowseFileTarget.Click += (sender, e) => Browse(TxtFileTarget);
        ButBrowsePeriodicalTarget.Click += (sender, e) => Browse(TxtPeriodicalTarget);

        ButOk.Click += ButOk_Click;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        TxtFileTarget.Text = FileTarget;
        TxtPeriodicalTarget.Text = PeriodicalTarget;
        ConInterval.Value = Math.Clamp(PeriodicalTargetInterval, (int)ConInterval.Minimum, (int)ConInterval.Maximum);
    }

    /// <summary>
    /// Výběr složky.
    /// </summary>
    private void Browse(TextBox target)
    {
        using (var dialog = new FolderBrowserDialog { Description = "Select target folder", UseDescriptionForTitle = true, SelectedPath = target.Text })
        {
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                target.Text = dialog.SelectedPath;
            }
        }
    }

    /// <summary>
    /// Kontrola zadaných cest, při chybě dialog zůstane otevřený.
    /// </summary>
    private void ButOk_Click(object sender, EventArgs e)
    {
        if (!TryGetFullPath(TxtFileTarget, out var fileTarget) || !TryGetFullPath(TxtPeriodicalTarget, out var periodicalTarget))
        {
            DialogResult = DialogResult.None;
            return;
        }

        FileTarget = fileTarget;
        PeriodicalTarget = periodicalTarget;
        PeriodicalTargetInterval = (int)ConInterval.Value;
    }

    private bool TryGetFullPath(TextBox textBox, out string fullPath)
    {
        fullPath = null;

        try
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                fullPath = Path.GetFullPath(textBox.Text.Trim());
                return true;
            }
        }
        catch (Exception ex) when (ex is ArgumentException || ex is NotSupportedException || ex is PathTooLongException)
        {
        }

        MessageBox.Show(this, "Enter a valid folder path.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        textBox.Focus();

        return false;
    }
}
