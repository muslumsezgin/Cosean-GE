using CoseanGE.Theme;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoseanGE.AppScreen
{
    public partial class CoseanHelp : CSForm
    {
        public CoseanHelp()
        {
            InitializeComponent();
        }

        private void CoseanHelp_Load(object sender, EventArgs e)
        {

        }

        private void button0_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Bitmap.FromFile("help\\generall.jpg");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Bitmap.FromFile("help\\drawer.jpg");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Bitmap.FromFile("help\\screen.jpg");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Bitmap.FromFile("help\\filter.jpg");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Bitmap.FromFile("help\\props.jpg");
        }
    }
}
