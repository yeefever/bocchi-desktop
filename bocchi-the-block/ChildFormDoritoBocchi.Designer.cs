namespace bocchi_the_block
{
    partial class ChildFormDoritoBocchi
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.doritopictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doritopictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.InitialImage = global::bocchi_the_block.Properties.Resources.bocchi_dorito;
            this.pictureBox1.Location = new System.Drawing.Point(12, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(603, 564);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // doritopictureBox
            // 
            this.doritopictureBox.Image = global::bocchi_the_block.Properties.Resources.dorito;
            this.doritopictureBox.Location = new System.Drawing.Point(-59, -31);
            this.doritopictureBox.Name = "doritopictureBox";
            this.doritopictureBox.Size = new System.Drawing.Size(526, 469);
            this.doritopictureBox.TabIndex = 1;
            this.doritopictureBox.TabStop = false;
            // 
            // ChildFormDoritoBocchi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 578);
            this.Controls.Add(this.doritopictureBox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ChildFormDoritoBocchi";
            this.Text = "ChildFormDoritoBocchi";
            this.Load += new System.EventHandler(this.ChildFormDoritoBocchi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doritopictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox doritopictureBox;
    }
}