﻿namespace CoseanGE.AppScreen
{
    partial class IntroScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntroScreen));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.B_Open = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(1, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 471);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(83)))), ((int)(((byte)(13)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(221, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2, 471);
            this.panel2.TabIndex = 5;
            // 
            // B_Open
            // 
            this.B_Open.BackColor = System.Drawing.Color.Transparent;
            this.B_Open.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B_Open.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(83)))), ((int)(((byte)(13)))));
            this.B_Open.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(63)))), ((int)(((byte)(3)))));
            this.B_Open.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(83)))), ((int)(((byte)(13)))));
            this.B_Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.B_Open.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10F, System.Drawing.FontStyle.Bold);
            this.B_Open.ForeColor = System.Drawing.Color.Transparent;
            this.B_Open.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.B_Open.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.B_Open.Location = new System.Drawing.Point(391, 391);
            this.B_Open.Margin = new System.Windows.Forms.Padding(0);
            this.B_Open.Name = "B_Open";
            this.B_Open.Size = new System.Drawing.Size(160, 40);
            this.B_Open.TabIndex = 15;
            this.B_Open.Text = "Open File";
            this.B_Open.UseVisualStyleBackColor = false;
            this.B_Open.Click += new System.EventHandler(this.B_Open_Click2);
            // 
            // IntroScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 502);
            this.Controls.Add(this.B_Open);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IntroScreen";
            this.Text = "IntroScreen";
            this.Load += new System.EventHandler(this.IntroScreen_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.B_Open, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button B_Open;
    }
}