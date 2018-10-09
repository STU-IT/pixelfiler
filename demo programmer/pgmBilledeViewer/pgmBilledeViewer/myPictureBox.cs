using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace pgmBilledeViewer
{
    class myPictureBox : PictureBox
    {

        public InterpolationMode InterpolationMode { get; set; }
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (this.InterpolationMode == InterpolationMode.Default)
                pe.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            else
                pe.Graphics.InterpolationMode = this.InterpolationMode;

            base.OnPaint(pe);
        }
    }
}
