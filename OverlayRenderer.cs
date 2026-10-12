using System;
using System.Drawing;

namespace UsbCam;

/// <summary>
/// Stav pomůcek, které se kreslí do obrazu (neměnný, lze jej bezpečně předat do jiného vlákna).
/// </summary>
internal readonly record struct OverlayState(PenSet PenSet, int Angle, int AngleMark, int CircleDiameter);

/// <summary>
/// Kreslení pomůcek (kříž, kružnice, úhly) do snímku. Nezávisí na formuláři.
/// </summary>
internal static class OverlayRenderer
{
    /// <summary>
    /// Domaluje pomůcky do snímku.
    /// </summary>
    public static void Draw(Bitmap bitmap, OverlayState state)
    {
        var penSet = state.PenSet;

        var width = bitmap.Width;
        var centerX = width / 2;
        var centerY = bitmap.Height / 2;

        using (var graphics = Graphics.FromImage(bitmap))
        {
            DrawAngleCross(graphics, penSet.PenBg, penSet.CrossPen, penSet.CrossPen, 0, centerX, centerY, width * 2);
            DrawCircle(graphics, penSet.CirclePen, centerX, centerY, 2 * state.CircleDiameter);

            if (state.Angle > 0)
            {
                DrawAngleCross(graphics, penSet.PenBg, penSet.AngleMasterPen, penSet.AngleSlavePen, state.Angle, centerX, centerY, width * 2);
            }

            if (state.AngleMark > 0)
            {
                DrawAngleLine(graphics, penSet.PenBg, penSet.AngleMarkPen, state.AngleMark, centerX, centerY, width * 2);
            }

            DrawArc(graphics, penSet.PenBg, penSet.AngleArcPen, state.Angle, state.AngleMark, centerX, centerY, 78);
        }
    }

    private static void DrawArc(Graphics g, Pen penBg, Pen pen, int startAngle, int endAngle, int x, int y, int size)
    {
        //Bez rozdílu úhlů není co kreslit.
        if (startAngle == endAngle)
            return;

        var arcRect = new Rectangle(x - size / 2, y - size / 2, size, size);

        g.DrawArc(penBg, arcRect, 360 - startAngle, startAngle - endAngle);
        g.DrawArc(pen, arcRect, 360 - startAngle, startAngle - endAngle);
    }

    /// <summary>
    /// Kreslíme kružnici o průměru d.
    /// </summary>
    private static void DrawCircle(Graphics g, Pen pen, int x, int y, int d)
    {
        if (d > 0)
        {
            g.DrawEllipse(pen, x - d / 2, y - d / 2, d, d);
        }
    }

    /// <summary>
    /// Nakreslí čáru pod úhlem.
    /// </summary>
    private static void DrawAngleLine(Graphics g, Pen penBg, Pen pen, int angle, int x, int y, int length)
    {
        double angleInRadians = angle * Math.PI / 180; // převod úhlu z stupňů na radiány

        int dx = (int)(length * Math.Cos(angleInRadians)); // x-ová souřadnice koncového bodu
        int dy = (int)(length * Math.Sin(angleInRadians)); // y-ová souřadnice koncového bodu

        g.DrawLine(penBg, x, y, x + dx, y - dy);
        g.DrawLine(pen, x, y, x + dx, y - dy);
    }

    /// <summary>
    /// Kreslíme kříž pod úhlem.
    /// </summary>
    private static void DrawAngleCross(Graphics g, Pen penBg, Pen pen, Pen penSlave, int angle, int x, int y, int length)
    {
        DrawAngleLine(g, penBg, pen, angle, x, y, length);
        DrawAngleLine(g, penBg, penSlave, angle + 90, x, y, length);
        DrawAngleLine(g, penBg, penSlave, angle + 180, x, y, length);
        DrawAngleLine(g, penBg, penSlave, angle + 270, x, y, length);
    }
}
