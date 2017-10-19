using CoseanGE.Controller;
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

            Button b = new Button();
            b.Text = name;
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.Dock = DockStyle.Top;
            b.Height = 45;
            b.ForeColor = Color.White;
            b.BackColor = Color.Transparent;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 83, 13);
            b.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 53, 13);
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
