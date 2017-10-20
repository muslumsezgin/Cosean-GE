using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoseanGE.Theme
{
    class CSDesButton : Button
    {
        Color clr1;
        private Color color = Color.FromArgb(38,38,38);
        private Color m_hovercolor = Color.FromArgb(255, 83, 13);
        private Color clickcolor = Color.FromArgb(255, 53, 13);
        private int textX = 5;
        private int textY = 0;
        private String text = "text";
        private String desc = "desc";

        public String DisplayDesc
        {
            get { return desc; }
            set { desc = value; Invalidate(); }
        }

        public String DisplayText
        {
            get { return text; }
            set { text = value; Invalidate(); }
        }

        public Color BZBackColor
        {
            get { return color; }
            set { color = value; Invalidate(); }
        }

        public Color MouseHoverColor
        {
            get { return m_hovercolor; }
            set { m_hovercolor = value; Invalidate(); }
        }

        public Color MouseClickColor1
        {
            get { return clickcolor; }
            set { clickcolor = value; Invalidate(); }
        }

        public int TextLocation_X
        {
            get { return textX; }
            set { textX = value; Invalidate(); }
        }
        public int TextLocation_Y
        {
            get { return textY; }
            set { textY = value; Invalidate(); }
        }


        public CSDesButton()
        {
            this.Size = new System.Drawing.Size(220, 35);
            this.ForeColor = Color.White;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }
        
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            clr1 = color;
            color = m_hovercolor;
        }
        
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            color = clr1;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            color = clickcolor;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            color = clr1;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            text = this.Text;
            if (textX == 100 && textY == 25)
            {
                textX = ((this.Width) / 3) + 10;
                textY = (this.Height / 2) - 1;
            }

            Point p = new Point(textX, textY);
            Font f = new System.Drawing.Font("Microsoft YaHei UI", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))); ;
            pe.Graphics.FillRectangle(new SolidBrush(color), ClientRectangle);
            pe.Graphics.DrawString(text, this.Font, new SolidBrush(this.ForeColor), p);
            pe.Graphics.DrawString(desc, f, new SolidBrush(this.ForeColor), new Point(textX, textY + 17));
        }

    }
}