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
            this.PlayerBlack = new System.Windows.Forms.Button();
            this.PlayerWhite = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayerBlack
            // 
            this.PlayerBlack.Location = new System.Drawing.Point(27, 12);
            this.PlayerBlack.Name = "PlayerBlack";
            this.PlayerBlack.Size = new System.Drawing.Size(117, 44);
            this.PlayerBlack.TabIndex = 0;
            this.PlayerBlack.Text = "Black";
            this.PlayerBlack.UseVisualStyleBackColor = true;
            this.PlayerBlack.Click += new System.EventHandler(this.PlayerBlack_Click);
            // 
            // PlayerWhite
            // 
            this.PlayerWhite.Location = new System.Drawing.Point(27, 62);
            this.PlayerWhite.Name = "PlayerWhite";
            this.PlayerWhite.Size = new System.Drawing.Size(117, 44);
            this.PlayerWhite.TabIndex = 1;
            this.PlayerWhite.Text = "White";
            this.PlayerWhite.UseVisualStyleBackColor = true;
            this.PlayerWhite.Click += new System.EventHandler(this.PlayerWhite_Click);
            // 
            // PopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 161);
            this.Controls.Add(this.PlayerWhite);
            this.Controls.Add(this.PlayerBlack);
            this.Name = "PopUp";
            this.Text = "PopUp";
            this.ResumeLayout(false);
            //this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PlayerBlack;
        private System.Windows.Forms.Button PlayerWhite;
    }
}