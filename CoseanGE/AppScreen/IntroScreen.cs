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

        List<String> data;
        string filename = "C:\\test\\test.txt";

        [Serializable]
        public struct MyStructData
        {
            public string Name;
        }

        public IntroScreen()
        {
            InitializeComponent();
        }

        private void IntroScreen_Load(object sender, EventArgs e)
        {
            data = new List<String>();
        }

        private void B_Open_Click(object sender, EventArgs e)
        {
            
            using (var file = File.OpenWrite(filename))
            {
                var writer = new BinaryFormatter();
                writer.Serialize(file, data); // Writes the entire list.
            }

            
        }

        private void B_Open_Click2(object sender, EventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            opn.Filter = "JPG|*.jpg;*.jpeg|BMP|*.bmp|GIF|*.gif|PNG|*.png|TIFF|*.tif;*.tiff|All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
            if (opn.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            if (data.Contains(opn.FileName))
                data.Add(opn.FileName);

            //ScreenController.CloseIntro(opn.FileName);
        }

        private void openEditor(String path)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var file = File.OpenRead(filename))
            {
                var reader = new BinaryFormatter();
                data = (List<String>)reader.Deserialize(file); // Reads the entire list.
            }

            foreach (var item in data)
                Console.WriteLine(item);
        }

    }
}
