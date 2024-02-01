using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsbCam
{
    internal class PenSet
    {
        public string Key { get; set; }
        public Keys KeyCode { get; set; }
        public string Name { get; set; }
        public Pen CrossPen {get; set;}
        public Pen CirclePen {get; set;}
        public Pen AngleMasterPen {get; set;}
        public Pen AngleSlavePen {get; set;}
        public Pen AngleMarkPen {get; set;}
        public Pen AngleArcPen {get; set;}
        public Pen PenBg { get; set; }
    }

    internal class PenSetDict : Dictionary<string, PenSet>
    {
        public void Add(PenSet penSet)
        {
            this.Add(penSet.Key, penSet);
        }

        /// <summary>
        /// Vrací zadaný set nebo první v pořadí.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public PenSet GetSet(string key)
        {
            if (this.TryGetValue(key, out var penSet))
            {
                return penSet;
            }

            return this.First().Value;
        }

        /// <summary>
        /// Vrací zadaný set nebo první v pořadí.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public PenSet GetSet(Keys KeyCode) => this.Values.FirstOrDefault(it => it.KeyCode == KeyCode);

        /// <summary>
        /// Inicializuje na výchozí hodnoty.
        /// </summary>
        public void DefaultInit()
        {
            Add(GetDefault());
            Add(GetAsMonoColor(Color.Transparent, "0", System.Windows.Forms.Keys.NumPad0));
            Add(GetAsMonoColor(Color.Blue, "1", System.Windows.Forms.Keys.NumPad1));
            Add(GetAsMonoColor(Color.Black, "2", System.Windows.Forms.Keys.NumPad2));
            Add(GetAsMonoColor(Color.White, "3", System.Windows.Forms.Keys.NumPad3));
            Add(GetAsMonoColor(Color.Magenta, "4", System.Windows.Forms.Keys.NumPad4));
            Add(GetAsMonoColor(Color.Cyan, "5", System.Windows.Forms.Keys.NumPad5));
            Add(GetAsMonoColor(Color.Red, "6", System.Windows.Forms.Keys.NumPad6));
            Add(GetAsMonoColor(Color.Orange, "7", System.Windows.Forms.Keys.NumPad7));
        }

        private static PenSet GetDefault()
        {
            float[] crossPenDashPattern = { 5, 5 };
            float[] arcPenDashPattern = { 4, 4 };
            float[] angleMarkPenDashPattern = { 4, 4 };

            var penSet = new PenSet()
            {
                Key = "default",
                KeyCode = System.Windows.Forms.Keys.Decimal,
                Name = ".. Default",
                CrossPen = new Pen(Color.White, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Custom, DashPattern = crossPenDashPattern, DashOffset = 5 },
                CirclePen = new Pen(Color.Yellow, 1),
                AngleSlavePen = new Pen(Color.Gray, 1),
                AngleMasterPen = new Pen(Color.Yellow, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Custom, DashPattern = angleMarkPenDashPattern },
                AngleMarkPen = new Pen(Color.Orange, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Custom, DashPattern = angleMarkPenDashPattern },
                AngleArcPen = new Pen(Color.Yellow, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Custom, DashPattern = arcPenDashPattern },
                PenBg = new Pen(Color.DarkCyan, 1)
            };

            return penSet;
        }
        
        
        private static PenSet GetAsMonoColor(Color color, string namePrefix, Keys keyCode)
        {
            var prefix = (string.IsNullOrEmpty(namePrefix) ? string.Empty : $"{namePrefix}. ");

            return new PenSet()
            {
                Key = color.Name,
                KeyCode = keyCode,
                Name = $"{prefix}{color.Name}",
                CrossPen = new Pen(color, 1),
                CirclePen = new Pen(color, 1),
                AngleSlavePen = new Pen(color, 1),
                AngleMasterPen = new Pen(color, 1),
                AngleMarkPen = new Pen(color, 1),
                AngleArcPen = new Pen(color, 2),
                PenBg = new Pen(color, 1)
            };
        }

    }

}
