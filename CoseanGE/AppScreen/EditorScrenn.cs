
using CoseanGE.Controller;
using CoseanGE.ImageProsesing;
using CoseanGE.Theme;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CoseanGE.AppScreen
{
    public partial class EditorScrenn : Theme.CSSizableForm
    {
        bool orgPanelVisible = true;
        bool propPanelVisible = true;
        bool drawerPanelVisible = false;

        bool RChannel = true;
        bool GChannel = true;
        bool BChannel = true;

        List<Bitmap> imageList;

        String pathSource;
        PictureBox originalPB;
        PictureBox previewPB;
        

        Image imgOriginal;
        Image imgPreview;

        public EditorScrenn(String path)
        {
            InitializeComponent();
            pathSource = path;
            
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Z | Keys.Control:
                    Action_Undo();
                    return true;
                case Keys.O | Keys.Control:
                    Action_Open();
                    return true;
                case Keys.S | Keys.Control:
                    Action_Save();
                    return true;
                case Keys.S | Keys.Control | Keys.Shift:
                    Action_SaveAs();
                    return true;
                case Keys.W | Keys.Control:
                    ScreenController.CloseEditor();
                    return true;
              
            }

            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void EditorScrenn_Load(object sender, EventArgs e)
        {
            imageList = new List<Bitmap>();
            Image temp = Bitmap.FromFile(pathSource);
            imgOriginal = new Bitmap(temp);
            imgPreview = new Bitmap(temp);
            temp.Dispose();
            temp = null;
            imageColor();
            originalPB = new PictureBox();
            previewPB = new PictureBox();

            previewPB.Image = imgOriginal;
            
            previewPB.SizeMode = PictureBoxSizeMode.AutoSize;
            previewPB.BackgroundImageLayout = ImageLayout.Stretch;
            P_Preview.Controls.Add(previewPB);


            originalPB.Image = imgPreview;
            originalPB.SizeMode = PictureBoxSizeMode.AutoSize;
            originalPB.BackgroundImageLayout = ImageLayout.Stretch;
            P_Original.Controls.Add(originalPB);

            imageList.Add(new Bitmap(imgPreview));

            C_Zoom.DisplayMember = "Text";
            C_Zoom.ValueMember = "Value";

            var items = new[] { 
                new { Text = "%200", Value = "2" }, 
                new { Text = "%175", Value = "1.75" }, 
                new { Text = "%150", Value = "1.5" }, 
                new { Text = "%100", Value = "1" }, 
                new { Text = "%75", Value = "0.75" }, 
                new { Text = "%50", Value = "0.5" }, 
                new { Text = "%25", Value = "0.25" }, 
                new { Text = "%10", Value = "0.1" },
            };

            C_Zoom.DataSource = items;
            C_Zoom.SelectedIndex = 3;

            initializeToolTip();
            initializeTheme();

            for (int i = 0; i < ScreenController.openRecent.Count; i++)
                P_OpenRecentButtons.Controls.Add(addButton(ScreenController.openRecent[i]));


        }

        private Button addButton(string name)
        {
            Console.WriteLine(name);
            CSDesButton b = new CSDesButton();
            b.DisplayText = System.IO.Path.GetFileNameWithoutExtension(name);
            b.Text = System.IO.Path.GetFileNameWithoutExtension(name);
            b.DisplayDesc =ScreenController.pathSize(name, 36);
            b.Height = 35;
            b.BZBackColor = Color.FromArgb(ThemeColor.Default.Top.A, Math.Abs(ThemeColor.Default.Top.R ), Math.Abs(ThemeColor.Default.Top.G), Math.Abs(ThemeColor.Default.Top.B));
            b.MouseClickColor1 = Color.FromArgb(ThemeColor.Default.Top.A, Math.Abs(ThemeColor.Default.Top.R - 15), Math.Abs(ThemeColor.Default.Top.G - 15), Math.Abs(ThemeColor.Default.Top.B - 15));
            b.MouseHoverColor = Color.FromArgb(ThemeColor.Default.Top.A, Math.Abs(ThemeColor.Default.Top.R + 15), Math.Abs(ThemeColor.Default.Top.G + 15), Math.Abs(ThemeColor.Default.Top.B + 15));
            b.Dock = DockStyle.Top;

            b.Click += (s, e) => { ClickReOpenButton(name); };
            return b;
        }


        private void ClickReOpenButton(string name)
        {

            if (File.Exists(name))
            {
                ScreenController.WriteOpenRecent(name);
                ScreenController.NewEditor(name);
            }
            else
            {
                MessageBox.Show("There is no image file in ' " + name +" '", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void initializeToolTip() {
            ToolTip ToolTip = new ToolTip();

            // Set up the delays for the ToolTip.
            ToolTip.AutoPopDelay = 5000;
            ToolTip.InitialDelay = 100;
            ToolTip.ReshowDelay = 50;
            // Force the ToolTip text to be displayed whether or not the form is active.
            ToolTip.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            ToolTip.SetToolTip(_CloseButton, "Exit");
            ToolTip.SetToolTip(B_RefHis, "Refresh");
            ToolTip.SetToolTip(B_RotateR, "Rotate Right");
            ToolTip.SetToolTip(B_RotateL, "Rotate Left");
            ToolTip.SetToolTip(B_Mirror, "Mirror");
            ToolTip.SetToolTip(B_BlackWhite, "Black White Color");
            ToolTip.SetToolTip(B_Negative, "Negative");
            ToolTip.SetToolTip(B_Clean, "Clean");
            
        }

        public void reSize()
        {
            int kolon = 40;

            int fullSize = this.Width;
            fullSize = drawerPanelVisible ? fullSize - 220 : fullSize;
            P_Picture.Width = fullSize;
            fullSize = propPanelVisible ? fullSize - 250 : fullSize - 24;
            fullSize = fullSize - kolon;

            P_Drawer.Width = drawerPanelVisible ? 220 : 0;
            P_Properties.Width = propPanelVisible ? 250 : 23;

            P_Original.Width = orgPanelVisible ? fullSize / 2 : 2;
            P_Preview.Width = orgPanelVisible ? fullSize / 2 : fullSize - 2;


        }

        private void B_Drawer_Click(object sender, EventArgs e)
        {
            drawerPanelVisible = !drawerPanelVisible;
            P_Help.Width = 0;
            P_Open.Width = 0;
            reSize();
        }

        private void P_Picture_SizeChanged(object sender, EventArgs e)
        {
            reSize();
        }

        private void orgButton_Click(object sender, EventArgs e)
        {
            orgPanelVisible = !orgPanelVisible;
            reSize();
        }

        private void B_Props_Click(object sender, EventArgs e)
        {
            propPanelVisible = !propPanelVisible;
            reSize();
        }

        private void B_Histogram_Click(object sender, EventArgs e)
        {
            P_Histogram.Height = P_Histogram.Height == 180 ? 50 : 180;
        }

        private void B_Channels_Click(object sender, EventArgs e)
        {
            P_Channels.Height = P_Channels.Height == 180 ? 50 : 180;
        }

        private void B_Scala_Click(object sender, EventArgs e)
        {
            P_Scala.Height = P_Scala.Height == 180 ? 50 : 180;
        }

        private void B_RotateR_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(imgPreview);
            Bitmap negative = Rotate.build(image, Rotate.RIGHT);
            //previewPB.Image = negative;
            //imgPreview = previewPB.Image;
            imgPreview = negative;
            RGBChannelChanged();
        }

        private void B_RotateL_Click(object sender, EventArgs e)
        {

            Bitmap image = new Bitmap(imgPreview);
            Bitmap negative = ImageProsesing.Rotate.build(image, Rotate.LEFT);
            //previewPB.Image = negative;
            //imgPreview = previewPB.Image;
            imgPreview = negative;
            RGBChannelChanged();
        }

        private void B_Negative_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(imgPreview);
            Bitmap negative = Negative.build(image);
            //previewPB.Image = negative;
            //imgPreview = previewPB.Image;
            imgPreview = negative;
            RGBChannelChanged();
            B_RefHis.Visible = true;
        }

        private void B_BlackWhite_Click(object sender, EventArgs e)
        {
           
            Bitmap image = new Bitmap(imgPreview);
            Bitmap gri = BlackWhite.build(image);
            //previewPB.Image = gri;
            //imgPreview = previewPB.Image;
            imgPreview = gri;
            RGBChannelChanged();
            B_RefHis.Visible = true;

        }

        private void B_Mirror_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(imgPreview);
            Bitmap mirror = Mirror.build(image);
            //previewPB.Image = mirror;
            //imgPreview = previewPB.Image;
            imgPreview = mirror;
            RGBChannelChanged();
        }

        private void B_Clean_Click(object sender, EventArgs e)
        {
            C_Zoom.SelectedIndex = 3;
            //previewPB.Image = imgOriginal;
            imgPreview = imgOriginal;
            RGBChannelChanged();
            B_RefHis.Visible = true;
            
        }

        private void C_Zoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Double zoom = Double.Parse(C_Zoom.SelectedValue.ToString());
            originalPB.Image = Scala.Zoom(imgOriginal, zoom);
            RGBChannelChanged();
            
        }

        private void B_ScalaApply_Click(object sender, EventArgs e)
        {
            try
            {
                int newWidth =  Convert.ToInt32(T_Width.Text); 
                int newHeight = Convert.ToInt32(T_Height.Text);

                if(newHeight > 0 && newWidth > 0){
                    Bitmap image = new Bitmap(imgPreview);
                    Bitmap scala = Scala.build(image,newWidth,newHeight);
                    imgPreview = scala;
                    RGBChannelChanged();
                    B_RefHis.Visible = true;
                }
                else{
                    MessageBox.Show("New dimensions cannot be zero for scaling!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex){
                MessageBox.Show("New dimensions cannot be zero for scaling!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void B_OpenRecent_Click(object sender, EventArgs e)
        {
            P_Open.Width = 220;
        }

        private void B_OpenBack_Click(object sender, EventArgs e)
        {
            P_Open.Width = 0;
        }

        private void B_Open_Click(object sender, EventArgs e)
        {
            Action_Open();
        }

        private void B_SaveAs_Click(object sender, EventArgs e)
        {
            Action_SaveAs();
        }

        private void B_Save_Click(object sender, EventArgs e)
        {
            Action_Save();
        }

        public ImageFormat StringtoFormat(String type)
        {
            ImageFormat format = ImageFormat.Jpeg;
            switch (type)
            {
                case ".gif":

                    format = ImageFormat.Gif;
                    break;

                case ".bmp":

                    format = ImageFormat.Bmp;
                    break;
                case ".png":

                    format = ImageFormat.Png;
                    break;

                case ".tiff":

                    format = ImageFormat.Tiff;
                    break;
            }

            return format;

        }

        private void B_CloseProject_Click(object sender, EventArgs e)
        {
            ScreenController.CloseEditor();
        }

        private void Action_Open() {
            OpenFileDialog opn = new OpenFileDialog();
            opn.Filter = "JPG|*.jpg;*.jpeg|BMP|*.bmp|GIF|*.gif|PNG|*.png|TIFF|*.tiff|All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tiff;*.gif";
            if (opn.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            pathSource = opn.FileName;
            ScreenController.WriteOpenRecent(opn.FileName);
            opn.Dispose();
            imageColor();
            RefreshPage();
        }

        private void Action_Save() {
            Bitmap bmp = new Bitmap(imgPreview);
            bmp.Save(pathSource, StringtoFormat(pathSource.Substring(pathSource.IndexOf("."))));
        }

        private void Action_SaveAs() {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "JPG|*.jpg;*.jpeg|BMP|*.bmp|GIF|*.gif|PNG|*.png|TIFF|*.tiff|All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tiff;*.gif";
            if (save.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(imgPreview);
                bmp.Save(save.FileName, StringtoFormat(save.Filter));
                save.Dispose();
            }
        }
        


        private void Red_CheckedChanged(object sender, EventArgs e)
        {
            RChannel = Red.Checked;
            RGBChannelChanged();
        }

        private void Green_CheckedChanged(object sender, EventArgs e)
        {
            GChannel = Green.Checked;
            RGBChannelChanged();
        }

        private void Blue_CheckedChanged(object sender, EventArgs e)
        {
            BChannel = Blue.Checked;
            RGBChannelChanged();
        }

        private void RGBChannelChanged()
        {
            Bitmap image = new Bitmap(imgPreview);
            Bitmap rgb = RGBChannel.build(image, RChannel, GChannel, BChannel);
            Double zoom = Double.Parse(C_Zoom.SelectedValue.ToString());
            previewPB.Image = Scala.Zoom(rgb, zoom);
            //imgPreview = previewPB.Image;
            //drawHistogram();
            if (imageList.Count == 15)
                imageList.RemoveAt(0);
            imageList.Add(new Bitmap(imgPreview));
            GC.Collect();
            
        }

        private void B_HistogramRefresh_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(imgPreview);
            Image image2 = Histogram.build(image);
            PB_Histogram.Image = new Bitmap(image2, 210, 130);
            GC.Collect();
            B_RefHis.Visible = false;
        }

        private void changeColor()
        {
            this.P_Drawer.BackColor = ThemeColor.Default.Top;
            this.panel3.BackColor = ThemeColor.Default.Panel;
            this.panel1.BackColor=ThemeColor.Default.Panel;
            this.P_Open.BackColor = ThemeColor.Default.Panel;
        }

        private void imageColor() 
        {
            var colorThief = new ColorThiefDotNet.ColorThief();
            Bitmap bmp = new Bitmap(pathSource);
            try
            {
                ColorThiefDotNet.QuantizedColor color = colorThief.GetColor(bmp);
                Color c = Color.FromArgb(color.Color.A, color.Color.R, color.Color.G, color.Color.B);
                ThemeColor.Default.Top = c;
                ThemeColor.Default.Drawer = c;
                ThemeColor.Default.Panel = c;
                ThemeColor.Default.Save();
            }
            catch (Exception ex){}
 
        }
        private void RefreshPage()
        {
            ScreenController.NewEditor(pathSource); 
        }

            private void initializeTheme() 
            {
                this.P_Drawer.BackColor = ThemeColor.Default.Drawer;
                this.P_Help.BackColor = ThemeColor.Default.Panel;
                this.panel1.BackColor = ThemeColor.Default.Panel;
                this.panel3.BackColor = ThemeColor.Default.Panel;
                this.panel6.BackColor = ThemeColor.Default.Panel;
                this.P_Open.BackColor = ThemeColor.Default.Drawer;
                this.TopPanel.BackColor = ThemeColor.Default.Top;
                this.LeftBorderPanel.BackColor = ThemeColor.Default.Top;
                this.RightBorderPanel.BackColor = ThemeColor.Default.Top;
                this.TopBorderPanel.BackColor = ThemeColor.Default.Top;
                this.BottomBorderPanel.BackColor = ThemeColor.Default.Top;
                MouseDownOverColor(B_Open);
                MouseDownOverColor(B_Save);
                MouseDownOverColor(B_SaveAs);
                MouseDownOverColor(B_OpenRecent);
                MouseDownOverColor(B_OpenBack);
                MouseDownOverColor(B_Histogram);
                MouseDownOverColor(B_BlackWhite);
                MouseDownOverColor(B_Channels);
                MouseDownOverColor(B_Clean);
                MouseDownOverColor(B_CloseProject);
                MouseDownOverColor(B_Drawer);
                MouseDownOverColor(B_Mirror);
                MouseDownOverColor(B_Negative);
                MouseDownOverColor(B_Props);
                MouseDownOverColor(B_RefHis);
                MouseDownOverColor(B_RotateL);
                MouseDownOverColor(B_RotateR);
                MouseDownOverColor(B_Scala);
                MouseDownOverColor(B_ScalaApply);
                MouseDownOverColor(orgButton);
                MouseDownOverColor(B_Undo);
                MouseDownOverColor(B_Help);
                MouseDownOverColor(B_Shortcut);
                MouseDownOverColor(B_CheckForUpdate);
                MouseDownOverColor(B_CoseanHelp);
                MouseDownOverColor(B_HelpBack);
                MouseDownOverColor(B_About);
                _MinButton.MouseClickColor1 = Color.FromArgb(ThemeColor.Default.Top.A, Math.Abs(ThemeColor.Default.Top.R - 15), Math.Abs(ThemeColor.Default.Top.G - 15), Math.Abs(ThemeColor.Default.Top.B - 15));
                _MinButton.MouseHoverColor = Color.FromArgb(ThemeColor.Default.Top.A, Math.Abs(ThemeColor.Default.Top.R + 15), Math.Abs(ThemeColor.Default.Top.G + 15), Math.Abs(ThemeColor.Default.Top.B + 15));
                _MaxButton.MouseClickColor1 = Color.FromArgb(ThemeColor.Default.Top.A, Math.Abs(ThemeColor.Default.Top.R - 15), Math.Abs(ThemeColor.Default.Top.G - 15), Math.Abs(ThemeColor.Default.Top.B - 15));
                _MaxButton.MouseHoverColor = Color.FromArgb(ThemeColor.Default.Top.A, Math.Abs(ThemeColor.Default.Top.R + 15), Math.Abs(ThemeColor.Default.Top.G + 15), Math.Abs(ThemeColor.Default.Top.B + 15));
           
            }

            private void MouseDownOverColor(Button btn) {
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(ThemeColor.Default.Top.A, Math.Abs(ThemeColor.Default.Top.R - 15), Math.Abs(ThemeColor.Default.Top.G - 15), Math.Abs(ThemeColor.Default.Top.B - 15));
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(ThemeColor.Default.Top.A, Math.Abs(ThemeColor.Default.Top.R + 15), Math.Abs(ThemeColor.Default.Top.G + 15), Math.Abs(ThemeColor.Default.Top.B + 15));
            
            }

            private void B_Undo_Click(object sender, EventArgs e)
            {
                Action_Undo();
            }

            private void Action_Undo() {
                if (imageList.Count > 1)
                {
                    imageList.Remove(imageList.Last());
                    imgPreview = imageList.Last();
                    Bitmap image = new Bitmap(imgPreview);
                    Bitmap rgb = RGBChannel.build(image, RChannel, GChannel, BChannel);
                    Double zoom = Double.Parse(C_Zoom.SelectedValue.ToString());
                    previewPB.Image = Scala.Zoom(rgb, zoom);
                    GC.Collect();
                    B_RefHis.Visible = true;
                }
            }

            private void T_Width_KeyPress(object sender, KeyPressEventArgs e)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            private void T_Height_KeyPress(object sender, KeyPressEventArgs e)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            private void B_Help_Click(object sender, EventArgs e)
            {
                P_Help.Width = P_Help.Width == 0 ? 220 : 0;
            }

            private void B_HelpBack_Click(object sender, EventArgs e)
            {
                P_Help.Width = 0;
            }

            private void B_CheckForUpdate_Click(object sender, EventArgs e)
            {
                MessageBox.Show("You are using the latest version.", "Check for Updates", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            private void B_Shortcut_Click(object sender, EventArgs e)
            {
                new Shortcut().ShowDialog();
            }

            private void B_CoseanHelp_Click(object sender, EventArgs e)
            {

            }

            private void B_About_Click(object sender, EventArgs e)
            {
                new About().ShowDialog();
            }


    }
}
