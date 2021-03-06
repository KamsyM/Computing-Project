﻿namespace CheckersGUI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuGame = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CLIversion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightPiecesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OnHighlight = new System.Windows.Forms.ToolStripMenuItem();
            this.OffHighlight = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OnTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.OffTextBox = new System.Windows.Forms.ToolStripMenuItem();
            this.matchLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OnMatchLog = new System.Windows.Forms.ToolStripMenuItem();
            this.OffMatchLog = new System.Windows.Forms.ToolStripMenuItem();
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
            this.PlayPause = new System.Windows.Forms.Button();
            this.ChatMessage = new System.Windows.Forms.TextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.WhoTurn = new System.Windows.Forms.Label();
            this.MatchLog = new System.Windows.Forms.RichTextBox();
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
            this.Messages.ReadOnly = true;
            this.Messages.Size = new System.Drawing.Size(326, 83);
            this.Messages.TabIndex = 2;
            this.Messages.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuGame,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuGame
            // 
            this.MenuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuNewGame,
            this.saveGameToolStripMenuItem,
            this.loadGameToolStripMenuItem,
            this.endGameToolStripMenuItem,
            this.toolStripSeparator1,
            this.CLIversion,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.MenuGame.Name = "MenuGame";
            this.MenuGame.Size = new System.Drawing.Size(50, 20);
            this.MenuGame.Text = "&Game";
            // 
            // MenuNewGame
            // 
            this.MenuNewGame.Name = "MenuNewGame";
            this.MenuNewGame.Size = new System.Drawing.Size(134, 22);
            this.MenuNewGame.Text = "&New Game";
            this.MenuNewGame.Click += new System.EventHandler(this.MenuNewGame_Click);
            // 
            // saveGameToolStripMenuItem
            // 
            this.saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            this.saveGameToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.saveGameToolStripMenuItem.Text = "Save Game";
            this.saveGameToolStripMenuItem.Click += new System.EventHandler(this.saveGameToolStripMenuItem_Click);
            // 
            // loadGameToolStripMenuItem
            // 
            this.loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            this.loadGameToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.loadGameToolStripMenuItem.Text = "Load Game";
            this.loadGameToolStripMenuItem.Click += new System.EventHandler(this.loadGameToolStripMenuItem_Click);
            // 
            // endGameToolStripMenuItem
            // 
            this.endGameToolStripMenuItem.Name = "endGameToolStripMenuItem";
            this.endGameToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.endGameToolStripMenuItem.Text = "&End Game";
            this.endGameToolStripMenuItem.Click += new System.EventHandler(this.endGameToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // CLIversion
            // 
            this.CLIversion.Name = "CLIversion";
            this.CLIversion.Size = new System.Drawing.Size(134, 22);
            this.CLIversion.Text = "&CLI Version";
            this.CLIversion.Click += new System.EventHandler(this.CLIversion_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(131, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highlightPiecesToolStripMenuItem,
            this.textBoxToolStripMenuItem,
            this.matchLogToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // highlightPiecesToolStripMenuItem
            // 
            this.highlightPiecesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OnHighlight,
            this.OffHighlight});
            this.highlightPiecesToolStripMenuItem.Name = "highlightPiecesToolStripMenuItem";
            this.highlightPiecesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.highlightPiecesToolStripMenuItem.Text = "&Highlight Pieces";
            // 
            // OnHighlight
            // 
            this.OnHighlight.Name = "OnHighlight";
            this.OnHighlight.Size = new System.Drawing.Size(91, 22);
            this.OnHighlight.Text = "&On";
            this.OnHighlight.Click += new System.EventHandler(this.OnHighlight_Click);
            // 
            // OffHighlight
            // 
            this.OffHighlight.Checked = true;
            this.OffHighlight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OffHighlight.Name = "OffHighlight";
            this.OffHighlight.Size = new System.Drawing.Size(91, 22);
            this.OffHighlight.Text = "&Off";
            this.OffHighlight.Click += new System.EventHandler(this.OffHighlight_Click);
            // 
            // textBoxToolStripMenuItem
            // 
            this.textBoxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OnTextBox,
            this.OffTextBox});
            this.textBoxToolStripMenuItem.Name = "textBoxToolStripMenuItem";
            this.textBoxToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.textBoxToolStripMenuItem.Text = "&Text Box";
            // 
            // OnTextBox
            // 
            this.OnTextBox.Checked = true;
            this.OnTextBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OnTextBox.Name = "OnTextBox";
            this.OnTextBox.Size = new System.Drawing.Size(91, 22);
            this.OnTextBox.Text = "&On";
            this.OnTextBox.Click += new System.EventHandler(this.OnTextBox_Click);
            // 
            // OffTextBox
            // 
            this.OffTextBox.Name = "OffTextBox";
            this.OffTextBox.Size = new System.Drawing.Size(91, 22);
            this.OffTextBox.Text = "&Off";
            this.OffTextBox.Click += new System.EventHandler(this.OffTextBox_Click);
            // 
            // matchLogToolStripMenuItem
            // 
            this.matchLogToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OnMatchLog,
            this.OffMatchLog});
            this.matchLogToolStripMenuItem.Name = "matchLogToolStripMenuItem";
            this.matchLogToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.matchLogToolStripMenuItem.Text = "&Match Log";
            // 
            // OnMatchLog
            // 
            this.OnMatchLog.Checked = true;
            this.OnMatchLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OnMatchLog.Name = "OnMatchLog";
            this.OnMatchLog.Size = new System.Drawing.Size(91, 22);
            this.OnMatchLog.Text = "&On";
            this.OnMatchLog.Click += new System.EventHandler(this.onToolStripMenuItem_Click);
            // 
            // OffMatchLog
            // 
            this.OffMatchLog.Name = "OffMatchLog";
            this.OffMatchLog.Size = new System.Drawing.Size(91, 22);
            this.OffMatchLog.Text = "&Off";
            this.OffMatchLog.Click += new System.EventHandler(this.offToolStripMenuItem_Click);
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
            this.BlackPiecePic.Image = global::CheckersGUI.Properties.Resources.Black_Checker_Piece;
            this.BlackPiecePic.Location = new System.Drawing.Point(518, 77);
            this.BlackPiecePic.Name = "BlackPiecePic";
            this.BlackPiecePic.Size = new System.Drawing.Size(83, 83);
            this.BlackPiecePic.TabIndex = 18;
            this.BlackPiecePic.TabStop = false;
            // 
            // WhitePiecePic
            // 
            this.WhitePiecePic.Image = global::CheckersGUI.Properties.Resources.White_Checker_Piece;
            this.WhitePiecePic.Location = new System.Drawing.Point(518, 250);
            this.WhitePiecePic.Name = "WhitePiecePic";
            this.WhitePiecePic.Size = new System.Drawing.Size(83, 83);
            this.WhitePiecePic.TabIndex = 19;
            this.WhitePiecePic.TabStop = false;
            // 
            // PlayPause
            // 
            this.PlayPause.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PlayPause.Image = global::CheckersGUI.Properties.Resources.PlayPause;
            this.PlayPause.Location = new System.Drawing.Point(488, 390);
            this.PlayPause.Name = "PlayPause";
            this.PlayPause.Size = new System.Drawing.Size(39, 31);
            this.PlayPause.TabIndex = 21;
            this.PlayPause.UseVisualStyleBackColor = true;
            this.PlayPause.Visible = false;
            this.PlayPause.Click += new System.EventHandler(this.PlayPause_Click);
            // 
            // ChatMessage
            // 
            this.ChatMessage.Location = new System.Drawing.Point(12, 455);
            this.ChatMessage.Name = "ChatMessage";
            this.ChatMessage.Size = new System.Drawing.Size(326, 20);
            this.ChatMessage.TabIndex = 22;
            this.ChatMessage.Visible = false;
            this.ChatMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatMessage_KeyDown);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(344, 452);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 23;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Visible = false;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click_1);
            // 
            // WhoTurn
            // 
            this.WhoTurn.AutoSize = true;
            this.WhoTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.WhoTurn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.WhoTurn.Location = new System.Drawing.Point(448, 37);
            this.WhoTurn.Name = "WhoTurn";
            this.WhoTurn.Size = new System.Drawing.Size(0, 18);
            this.WhoTurn.TabIndex = 24;
            // 
            // MatchLog
            // 
            this.MatchLog.Location = new System.Drawing.Point(344, 366);
            this.MatchLog.Name = "MatchLog";
            this.MatchLog.ReadOnly = true;
            this.MatchLog.Size = new System.Drawing.Size(138, 80);
            this.MatchLog.TabIndex = 25;
            this.MatchLog.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 499);
            this.Controls.Add(this.MatchLog);
            this.Controls.Add(this.WhoTurn);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ChatMessage);
            this.Controls.Add(this.PlayPause);
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
            this.Controls.Add(this.Messages);
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
        private System.Windows.Forms.ToolStripMenuItem endGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highlightPiecesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OnHighlight;
        private System.Windows.Forms.ToolStripMenuItem OffHighlight;
        private System.Windows.Forms.ToolStripMenuItem textBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OnTextBox;
        private System.Windows.Forms.ToolStripMenuItem OffTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem CLIversion;
        private System.Windows.Forms.Button PlayPause;
        private System.Windows.Forms.ToolStripMenuItem saveGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadGameToolStripMenuItem;
        private System.Windows.Forms.TextBox ChatMessage;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Label WhoTurn;
        private System.Windows.Forms.ToolStripMenuItem matchLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OnMatchLog;
        private System.Windows.Forms.ToolStripMenuItem OffMatchLog;
        public System.Windows.Forms.RichTextBox MatchLog;
    }
}

