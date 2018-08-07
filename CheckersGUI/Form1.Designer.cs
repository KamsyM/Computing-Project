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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuGame = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNewGame = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.Location = new System.Drawing.Point(12, 23);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(326, 337);
            this.Grid.TabIndex = 0;
            this.Grid.TabStop = false;
            this.Grid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseClick);
            // 
            // Messages
            // 
            this.Messages.Location = new System.Drawing.Point(12, 366);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuGame});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuGame
            // 
            this.MenuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuNewGame});
            this.MenuGame.Name = "MenuGame";
            this.MenuGame.Size = new System.Drawing.Size(50, 20);
            this.MenuGame.Text = "&Game";
            // 
            // MenuNewGame
            // 
            this.MenuNewGame.Name = "MenuNewGame";
            this.MenuNewGame.Size = new System.Drawing.Size(180, 22);
            this.MenuNewGame.Text = "New Game";
            this.MenuNewGame.Click += new System.EventHandler(this.MenuNewGame_Click);
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
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Checkers";
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Grid;
        private System.Windows.Forms.RichTextBox Messages;
        private System.Windows.Forms.Button Start1PGame;
        private System.Windows.Forms.Button Start2PGame;
        private System.Windows.Forms.Button Quit;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuGame;
        private System.Windows.Forms.ToolStripMenuItem MenuNewGame;
    }
}

