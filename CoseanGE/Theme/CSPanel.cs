using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace CoseanGE.Theme
{
    public class CSPanel : System.Windows.Forms.Panel
    {
        
        private Color bordercolor = Color.Teal;
        int bdrwidth = 1;

        public Color BorderColor{
            get { return bordercolor; }
            set { bordercolor = value; Invalidate(); }
        }

        public int BorderWidth
        {
            get { return bdrwidth; }
            set { bdrwidth = value; Invalidate(); }
        }

        public CSPanel() { }

        protected override void OnPaint(PaintEventArgs e){

            base.OnPaint(e);

            if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0){

                if (bdrwidth > 0) {

                    if (bdrwidth == 0)
                    {
                    }
                    else {
                        if (bdrwidth == 1)
                        {
                            e.Graphics.DrawRectangle(new Pen(bordercolor), 0, 0, this.Width - 1, this.Height - 1);
                        }
                        else {
                            for (int i = 0; i < bdrwidth; i++)
                            {
                                e.Graphics.DrawRectangle(new Pen(bordercolor), 0 + i, 0, this.Width, this.Height - 1);
                                e.Graphics.DrawRectangle(new Pen(bordercolor), 0, 0 + 1, this.Width, this.Height - 1);
                                e.Graphics.DrawRectangle(new Pen(bordercolor), 0, 0, this.Width - 2 - i, this.Height - 1);
                                e.Graphics.DrawRectangle(new Pen(bordercolor), 0, this.Height - 1 - i, this.Width - 1, this.Height - 1 - i);
                            }
                        }
                    }

                }

                try
                {
                    //e.Graphics.DrawIcon(new Icon("favicon.ico"), new Rectangle(6, 5, 22, 22));
                }
                catch { }

            }
            
        }

    }
}