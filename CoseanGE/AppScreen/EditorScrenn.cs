using CoseanGE.Controller;
using CoseanGE.ImageProsesing;
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

        bool HistogramChange = true;

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

        private void EditorScrenn_Load(object sender, EventArgs e)
        {
            Bitmap temp = new Bitmap(Image.FromFile(pathSource));
            imgOriginal = new Bitmap(temp);
            imgPreview = new Bitmap(temp);
            temp.Dispose();
            temp = null;

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
            previewPB.Image = negative;
            imgPreview = previewPB.Image;
            RGBChannelChanged();
        }

        private void B_RotateL_Click(object sender, EventArgs e)
        {

            Bitmap image = new Bitmap(imgPreview);
            Bitmap negative = ImageProsesing.Rotate.build(image, Rotate.LEFT);
            previewPB.Image = negative;
            imgPreview = previewPB.Image;
            RGBChannelChanged();
        }

        private void B_Negative_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(imgPreview);
            Bitmap negative = Negative.build(image);
            previewPB.Image = negative;
            imgPreview = previewPB.Image;
            RGBChannelChanged();
        }

        private void B_BlackWhite_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(imgPreview);
            Bitmap gri = BlackWhite.build(image);
            previewPB.Image = gri;
            imgPreview = previewPB.Image;
            RGBChannelChanged();
        }

        private void B_Mirror_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(imgPreview);
            Bitmap mirror = Mirror.build(image);
            previewPB.Image = mirror;
            imgPreview = previewPB.Image;
            RGBChannelChanged();
        }

        private void B_Clean_Click(object sender, EventArgs e)
        {
            C_Zoom.SelectedIndex = 3;
            previewPB.Image = imgOriginal;
            imgPreview = imgOriginal;
            RGBChannelChanged();

        }

        private void C_Zoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            zoomPicture();
        }

        private void B_ScalaApply_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(imgPreview);
            previewPB.Image = Scala.build(image, Double.Parse(T_Width.Text),Double.Parse(T_Height.Text));
            imgPreview = previewPB.Image;

            zoomPicture();
        }

        private void zoomPicture(){
            Double zoom = Double.Parse(C_Zoom.SelectedValue.ToString());
            originalPB.Image = Scala.Zoom(imgOriginal, zoom);
            RGBChannelChanged();
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
            OpenFileDialog opn = new OpenFileDialog();
            opn.Filter = "JPG|*.jpg;*.jpeg|BMP|*.bmp|GIF|*.gif|PNG|*.png|TIFF|*.tiff|All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tiff;*.gif";
            if (opn.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            pathSource = opn.FileName;
            Image temp = Image.FromFile(pathSource);
            imgOriginal = new Bitmap(temp);
            imgPreview = Image.FromFile(pathSource);
            previewPB.Image = imgOriginal;
            originalPB.Image = imgOriginal;
            C_Zoom.SelectedIndex = 3;
            RGBChannelChanged();
        }

        private void B_SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "JPG|*.jpg;*.jpeg|BMP|*.bmp|GIF|*.gif|PNG|*.png|TIFF|*.tiff|All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tiff;*.gif";
            if (save.ShowDialog() == DialogResult.OK){
                Bitmap bmp = new Bitmap(imgPreview);
                bmp.Save(save.FileName, StringtoFormat(save.Filter));
            }
        }

        private void B_Save_Click(object sender, EventArgs e)
        {
                      
            
        }

        public ImageFormat StringtoFormat(String type) {
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

        private void RGBChannelChanged() {
             Bitmap image = new Bitmap(imgPreview);
             Bitmap rgb = RGBChannel.build(image, RChannel, GChannel, BChannel);
             Double zoom = Double.Parse(C_Zoom.SelectedValue.ToString());
             previewPB.Image = Scala.Zoom(rgb, zoom);
             //imgPreview = previewPB.Image;
             //drawHistogram();
        }

        private void B_HistogramRefresh_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(imgPreview);
            Image image2 = Histogram.build(image);
            PB_Histogram.Image = new Bitmap(image2, 210, 130);
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            
            
        }



       
    }
}
