namespace CheckersGUI
{
    partial class PopUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUp));
            this.btnOK = new System.Windows.Forms.Button();
            this.WinningPic = new System.Windows.Forms.PictureBox();
            this.WinningText = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.WinningPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(313, 250);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 36);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            // 
            // WinningPic
            // 
            this.WinningPic.Location = new System.Drawing.Point(192, 12);
            this.WinningPic.Name = "WinningPic";
            this.WinningPic.Size = new System.Drawing.Size(83, 83);
            this.WinningPic.TabIndex = 5;
            this.WinningPic.TabStop = false;
            // 
            // WinningText
            // 
            this.WinningText.AllowDrop = true;
            this.WinningText.BackColor = System.Drawing.SystemColors.Window;
            this.WinningText.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WinningText.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.WinningText.Location = new System.Drawing.Point(12, 111);
            this.WinningText.Multiline = true;
            this.WinningText.Name = "WinningText";
            this.WinningText.ReadOnly = true;
            this.WinningText.Size = new System.Drawing.Size(368, 133);
            this.WinningText.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(71, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(83, 82);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // PopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 288);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.WinningText);
            this.Controls.Add(this.WinningPic);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Winner!!!";
            ((System.ComponentModel.ISupportInitialize)(this.WinningPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox WinningPic;
        private System.Windows.Forms.TextBox WinningText;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}