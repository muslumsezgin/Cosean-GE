using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using CoseanGE.Controller;


namespace CoseanGE.AppScreen
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            
        }

        System.Windows.Forms.Timer t;

        private void Splash_Load(object sender, EventArgs e)
        {
            t = new System.Windows.Forms.Timer();

            t.Interval = 1000; // specify interval time as you want
            t.Tick += new EventHandler(timer_Tick);
            t.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            
                t.Stop();
                ScreenController.ReadOpenRecent();
                ScreenController.CloseSplash();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
       
       
    }
}
