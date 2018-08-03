namespace CheckersGUI
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
            this.Grid = new System.Windows.Forms.PictureBox();
            this.Messages = new System.Windows.Forms.RichTextBox();
            this.Start1PGame = new System.Windows.Forms.Button();
            this.Start2PGame = new System.Windows.Forms.Button();
            this.Quit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.Location = new System.Drawing.Point(12, 12);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(326, 337);
            this.Grid.TabIndex = 0;
            this.Grid.TabStop = false;
            this.Grid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseClick);
            // 
            // Messages
            // 
            this.Messages.Location = new System.Drawing.Point(12, 355);
            this.Messages.Name = "Messages";
            this.Messages.Size = new System.Drawing.Size(326, 83);
            this.Messages.TabIndex = 2;
            this.Messages.Text = "";
            // 
            // Start1PGame
            // 
            this.Start1PGame.Location = new System.Drawing.Point(475, 26);
            this.Start1PGame.Name = "Start1PGame";
            this.Start1PGame.Size = new System.Drawing.Size(90, 41);
            this.Start1PGame.TabIndex = 6;
            this.Start1PGame.Text = "Start 1P Game";
            this.Start1PGame.UseVisualStyleBackColor = true;
            this.Start1PGame.Click += new System.EventHandler(this.Start1PGame_Click);
            // 
            // Start2PGame
            // 
            this.Start2PGame.Location = new System.Drawing.Point(475, 130);
            this.Start2PGame.Name = "Start2PGame";
            this.Start2PGame.Size = new System.Drawing.Size(90, 41);
            this.Start2PGame.TabIndex = 7;
            this.Start2PGame.Text = "Start 2P Game";
            this.Start2PGame.UseVisualStyleBackColor = true;
            this.Start2PGame.Click += new System.EventHandler(this.Start2PGame_Click);
            // 
            // Quit
            // 
            this.Quit.Location = new System.Drawing.Point(475, 240);
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(90, 41);
            this.Quit.TabIndex = 8;
            this.Quit.Text = "Quit";
            this.Quit.UseVisualStyleBackColor = true;
            this.Quit.Click += new System.EventHandler(this.Quit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.Start2PGame);
            this.Controls.Add(this.Start1PGame);
            this.Controls.Add(this.Messages);
            this.Controls.Add(this.Grid);
            this.Name = "Form1";
            this.Text = "Checkers";
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Grid;
        private System.Windows.Forms.RichTextBox Messages;
        private System.Windows.Forms.Button Start1PGame;
        private System.Windows.Forms.Button Start2PGame;
        private System.Windows.Forms.Button Quit;
    }
}

