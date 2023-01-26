
namespace Slider
{
    partial class Instructions
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
            this.lblInstr = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblInstr
            // 
            this.lblInstr.BackColor = System.Drawing.SystemColors.Control;
            this.lblInstr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInstr.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstr.Location = new System.Drawing.Point(219, 35);
            this.lblInstr.Name = "lblInstr";
            this.lblInstr.Size = new System.Drawing.Size(562, 630);
            this.lblInstr.TabIndex = 0;
            this.lblInstr.Click += new System.EventHandler(this.lblInstr_Click);
            // 
            // label1
            // 
            this.label1.Image = global::Slider.Properties.Resources.images_removebg_preview;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 134);
            this.label1.TabIndex = 3;
            // 
            // Instructions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(833, 690);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblInstr);
            this.DoubleBuffered = true;
            this.Name = "Instructions";
            this.Text = "Instructions";
            this.Load += new System.EventHandler(this.Instructions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInstr;
        private System.Windows.Forms.Label label1;
    }
}