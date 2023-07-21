namespace bocchi_the_block
{
    partial class Form1
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
            this.startButton = new System.Windows.Forms.Button();
            this.freqLabel = new System.Windows.Forms.Label();
            this.freqBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.freqBar)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(40, 278);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(138, 82);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Bocchi";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // freqLabel
            // 
            this.freqLabel.AutoSize = true;
            this.freqLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freqLabel.Location = new System.Drawing.Point(51, 25);
            this.freqLabel.Name = "freqLabel";
            this.freqLabel.Size = new System.Drawing.Size(127, 29);
            this.freqLabel.TabIndex = 1;
            this.freqLabel.Text = "Frequency: 3";
            this.freqLabel.Click += new System.EventHandler(this.freqLabel_Click);
            // 
            // freqBar
            // 
            this.freqBar.Location = new System.Drawing.Point(40, 57);
            this.freqBar.Name = "freqBar";
            this.freqBar.Size = new System.Drawing.Size(138, 56);
            this.freqBar.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 400);
            this.Controls.Add(this.freqBar);
            this.Controls.Add(this.freqLabel);
            this.Controls.Add(this.startButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.freqBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label freqLabel;
        private System.Windows.Forms.TrackBar freqBar;
    }
}

