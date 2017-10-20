using CoseanGE.Controller;
using CoseanGE.Theme;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoseanGE.AppScreen
{
    public partial class IntroScreen : Theme.CSForm
    {

        public IntroScreen()
        {
            InitializeComponent();
        }

        private void IntroScreen_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < ScreenController.openRecent.Count; i++)
                P_OpenRecent.Controls.Add(addButton(ScreenController.openRecent[i]));
        }

        private Button addButton(string name) {

            CSDesButton b = new CSDesButton();
            b.DisplayText = System.IO.Path.GetFileNameWithoutExtension(name);
            b.Text = System.IO.Path.GetFileNameWithoutExtension(name);
            b.DisplayDesc = ScreenController.pathSize(name,45);
            b.TextLocation_Y = 5;
            b.Height = 45;
            b.Dock = DockStyle.Top;
            b.Click += (s, e) => { ClickButton(name); };
            return b;
        }

        private void B_Open_Click2(object sender, EventArgs e)
        {
            
            OpenFileDialog opn = new OpenFileDialog();
            opn.Filter = "JPG|*.jpg;*.jpeg|BMP|*.bmp|GIF|*.gif|PNG|*.png|TIFF|*.tif;*.tiff|All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
            if (opn.ShowDialog() != System.Windows.Forms.DialogResult.OK){return;}
            
            ClickButton(opn.FileName);
        }

        private void ClickButton(string name) {
            if (File.Exists(name))
            {
                ScreenController.WriteOpenRecent(name);
                ScreenController.CloseIntro(name);
            }
            else {
                MessageBox.Show(name + " not fount","File Error");
            }
        }
    }
}
