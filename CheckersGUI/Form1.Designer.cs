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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Grid = new System.Windows.Forms.PictureBox();
            this.Messages = new System.Windows.Forms.RichTextBox();
            this.Quit = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuGame = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.lblNameP1 = new System.Windows.Forms.Label();
            this.lblNameP2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.P1remain = new System.Windows.Forms.RichTextBox();
            this.P2remain = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BlackPiecePic = new System.Windows.Forms.PictureBox();
            this.WhitePiecePic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlackPiecePic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhitePiecePic)).BeginInit();
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
            // Quit
            // 
            this.Quit.Location = new System.Drawing.Point(502, 408);
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
            this.MenuNewGame.Size = new System.Drawing.Size(132, 22);
            this.MenuNewGame.Text = "New Game";
            this.MenuNewGame.Click += new System.EventHandler(this.MenuNewGame_Click);
            // 
            // lblNameP1
            // 
            this.lblNameP1.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameP1.Location = new System.Drawing.Point(375, 107);
            this.lblNameP1.Name = "lblNameP1";
            this.lblNameP1.Size = new System.Drawing.Size(129, 17);
            this.lblNameP1.TabIndex = 10;
            this.lblNameP1.Text = "Player";
            // 
            // lblNameP2
            // 
            this.lblNameP2.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameP2.Location = new System.Drawing.Point(375, 271);
            this.lblNameP2.Name = "lblNameP2";
            this.lblNameP2.Size = new System.Drawing.Size(124, 16);
            this.lblNameP2.TabIndex = 11;
            this.lblNameP2.Text = "Opponent";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(375, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Remaining:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(375, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Remaining:";
            // 
            // P1remain
            // 
            this.P1remain.Location = new System.Drawing.Point(451, 137);
            this.P1remain.Name = "P1remain";
            this.P1remain.ReadOnly = true;
            this.P1remain.Size = new System.Drawing.Size(48, 23);
            this.P1remain.TabIndex = 14;
            this.P1remain.Text = "";
            // 
            // P2remain
            // 
            this.P2remain.Location = new System.Drawing.Point(451, 300);
            this.P2remain.Name = "P2remain";
            this.P2remain.ReadOnly = true;
            this.P2remain.Size = new System.Drawing.Size(48, 22);
            this.P2remain.TabIndex = 15;
            this.P2remain.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(375, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "PLAYER 1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(375, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "PLAYER 2";
            // 
            // BlackPiecePic
            // 
            this.BlackPiecePic.Image = ((System.Drawing.Image)(resources.GetObject("BlackPiecePic.Image")));
            this.BlackPiecePic.Location = new System.Drawing.Point(518, 87);
            this.BlackPiecePic.Name = "BlackPiecePic";
            this.BlackPiecePic.Size = new System.Drawing.Size(37, 37);
            this.BlackPiecePic.TabIndex = 18;
            this.BlackPiecePic.TabStop = false;
            // 
            // WhitePiecePic
            // 
            this.WhitePiecePic.Image = ((System.Drawing.Image)(resources.GetObject("WhitePiecePic.Image")));
            this.WhitePiecePic.Location = new System.Drawing.Point(518, 250);
            this.WhitePiecePic.Name = "WhitePiecePic";
            this.WhitePiecePic.Size = new System.Drawing.Size(37, 37);
            this.WhitePiecePic.TabIndex = 19;
            this.WhitePiecePic.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.WhitePiecePic);
            this.Controls.Add(this.BlackPiecePic);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.P2remain);
            this.Controls.Add(this.P1remain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNameP2);
            this.Controls.Add(this.lblNameP1);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.Messages);
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Checkers";
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlackPiecePic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhitePiecePic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Grid;
        private System.Windows.Forms.RichTextBox Messages;
        private System.Windows.Forms.Button Quit;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuGame;
        private System.Windows.Forms.ToolStripMenuItem MenuNewGame;
        private System.Windows.Forms.Label lblNameP1;
        private System.Windows.Forms.Label lblNameP2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox P1remain;
        private System.Windows.Forms.RichTextBox P2remain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox BlackPiecePic;
        private System.Windows.Forms.PictureBox WhitePiecePic;
    }
}

