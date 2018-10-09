using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pgmBilledeViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            string fileName;
            //fileName = "feep_p2_plain.pgm";
            //fileName = "mountain.ascii.pgm";
            fileName = "mountain.reg.pgm";
            //fileName = "casablanca.ascii.pgm";

            pictureBox1.Image = Program.indlæsBillede(@"..\..\demobilleder\" + fileName);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
