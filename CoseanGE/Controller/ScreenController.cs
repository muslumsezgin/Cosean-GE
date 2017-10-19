using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoseanGE.AppScreen;
using System.Windows.Forms;
using System.IO;

namespace CoseanGE.Controller
{
    public class ScreenController : Form
    {

        public static EditorScrenn editorSC;
        public static IntroScreen introSC;
        public static Splash splashSC;
        public static List<string> openRecent;
        public static string OPpath = @"pnrcn.cge";

        public static void ReadOpenRecent()
        {

            if (File.Exists(OPpath))
            {
                using (var reader = new StreamReader(OPpath))
                {
                    openRecent.Clear();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //openRecent.Add(line);
                        openRecent.Insert(0, line);
                    }
                    reader.Close();
                }
            }
        }

        public static void WriteOpenRecent(string data)
        {

            try { openRecent.Remove(data); }
            catch (Exception e) { }

            openRecent.Add(data);

            if (!File.Exists(OPpath))
                File.Create(OPpath).Dispose();

            using (TextWriter tw = new StreamWriter(OPpath))
            {
                int limit = openRecent.Count > 10 ? 10 : openRecent.Count;
                for (int i = 0; i < limit; i++)
                {
                    tw.WriteLine(openRecent[openRecent.Count - i - 1]);
                }
                tw.Close();
            }

            ReadOpenRecent();
        }


        public ScreenController()
        {
            InitializeComponent();
            openRecent = new List<string>();
            splashSC = new Splash();
            splashSC.ShowDialog();

        }

        public static void CloseSplash() {
            introSC = new IntroScreen();
            splashSC.Hide();
            splashSC.Close();
            introSC.ShowDialog();
            
        }

        public static void CloseEditor()
        {
            introSC = new IntroScreen();
            editorSC.Hide();
            editorSC.Close();
            introSC.ShowDialog();
            
        }

        public static void NewEditor(String path)
        {
            editorSC.Hide();
            editorSC.Close();
            editorSC = new EditorScrenn(path);
            editorSC.ShowDialog();

        }

        public static void CloseIntro(String file)
        {
            editorSC = new EditorScrenn(file);
            introSC.Hide();
            introSC.Close();
            editorSC.ShowDialog();
            
        }

        public static void CloseApp()
        {
            Application.Exit();

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ScreenController
            // 
            this.ClientSize = new System.Drawing.Size(0, 0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Cosean GE";
            this.Load += new System.EventHandler(this.ScreenController_Load);
            this.ResumeLayout(false);

        }

        private void ScreenController_Load(object sender, EventArgs e)
        {

        }




    }
}
