using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoseanGE.AppScreen;
using System.Windows.Forms;

namespace CoseanGE.Controller
{
    public class ScreenController : Form
    {

        public static EditorScrenn editorSC;
        public static IntroScreen introSC;
        public static Splash splashSC;


        public ScreenController()
        {
            InitializeComponent();
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
