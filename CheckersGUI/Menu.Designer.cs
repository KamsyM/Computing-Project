namespace CheckersGUI
{
    partial class Menu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.Gamemode = new System.Windows.Forms.TabControl();
            this.Tab1P = new System.Windows.Forms.TabPage();
            this.Name1p = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Difficulty = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PlayerPieceType = new System.Windows.Forms.ComboBox();
            this.Tab2P = new System.Windows.Forms.TabPage();
            this.Highlight = new System.Windows.Forms.CheckBox();
            this.Name2P2 = new System.Windows.Forms.TextBox();
            this.Name2P1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TabCG = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.PlaySpeed = new System.Windows.Forms.TextBox();
            this.CG2Diff = new System.Windows.Forms.ComboBox();
            this.CG1Diff = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.NameCG2 = new System.Windows.Forms.TextBox();
            this.NameCG1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TabOnline = new System.Windows.Forms.TabPage();
            this.HostIpInfo = new System.Windows.Forms.Label();
            this.HostPiecetype = new System.Windows.Forms.ComboBox();
            this.HostPiecetypeLabel = new System.Windows.Forms.Label();
            this.PortNo = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.Advanced = new System.Windows.Forms.CheckBox();
            this.HostLabel = new System.Windows.Forms.Label();
            this.HostID = new System.Windows.Forms.TextBox();
            this.JoinGame = new System.Windows.Forms.CheckBox();
            this.HostGame = new System.Windows.Forms.CheckBox();
            this.OnlineID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pieceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Gamemode.SuspendLayout();
            this.Tab1P.SuspendLayout();
            this.Tab2P.SuspendLayout();
            this.TabCG.SuspendLayout();
            this.TabOnline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pieceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Gamemode
            // 
            this.Gamemode.Controls.Add(this.Tab1P);
            this.Gamemode.Controls.Add(this.Tab2P);
            this.Gamemode.Controls.Add(this.TabCG);
            this.Gamemode.Controls.Add(this.TabOnline);
            this.Gamemode.Location = new System.Drawing.Point(4, 12);
            this.Gamemode.Name = "Gamemode";
            this.Gamemode.SelectedIndex = 0;
            this.Gamemode.Size = new System.Drawing.Size(600, 324);
            this.Gamemode.TabIndex = 0;
            // 
            // Tab1P
            // 
            this.Tab1P.Controls.Add(this.Name1p);
            this.Tab1P.Controls.Add(this.label3);
            this.Tab1P.Controls.Add(this.Difficulty);
            this.Tab1P.Controls.Add(this.label2);
            this.Tab1P.Controls.Add(this.label1);
            this.Tab1P.Controls.Add(this.PlayerPieceType);
            this.Tab1P.Location = new System.Drawing.Point(4, 22);
            this.Tab1P.Name = "Tab1P";
            this.Tab1P.Padding = new System.Windows.Forms.Padding(3);
            this.Tab1P.Size = new System.Drawing.Size(592, 298);
            this.Tab1P.TabIndex = 0;
            this.Tab1P.Text = "Single Player";
            this.Tab1P.UseVisualStyleBackColor = true;
            // 
            // Name1p
            // 
            this.Name1p.Location = new System.Drawing.Point(111, 29);
            this.Name1p.Name = "Name1p";
            this.Name1p.Size = new System.Drawing.Size(152, 20);
            this.Name1p.TabIndex = 5;
            this.Name1p.Text = "Player";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Player Name";
            // 
            // Difficulty
            // 
            this.Difficulty.FormattingEnabled = true;
            this.Difficulty.Location = new System.Drawing.Point(205, 181);
            this.Difficulty.Name = "Difficulty";
            this.Difficulty.Size = new System.Drawing.Size(137, 21);
            this.Difficulty.TabIndex = 3;
            this.Difficulty.Items.AddRange(BotList().ToArray());
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Difficuty";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Player Piece";
            // 
            // PlayerPieceType
            // 
            this.PlayerPieceType.FormattingEnabled = true;
            this.PlayerPieceType.Items.AddRange(new object[] {
            "Black",
            "White"});
            this.PlayerPieceType.Location = new System.Drawing.Point(205, 119);
            this.PlayerPieceType.Name = "PlayerPieceType";
            this.PlayerPieceType.Size = new System.Drawing.Size(137, 21);
            this.PlayerPieceType.TabIndex = 0;
            // 
            // Tab2P
            // 
            this.Tab2P.Controls.Add(this.Highlight);
            this.Tab2P.Controls.Add(this.Name2P2);
            this.Tab2P.Controls.Add(this.Name2P1);
            this.Tab2P.Controls.Add(this.label5);
            this.Tab2P.Controls.Add(this.label4);
            this.Tab2P.Location = new System.Drawing.Point(4, 22);
            this.Tab2P.Name = "Tab2P";
            this.Tab2P.Padding = new System.Windows.Forms.Padding(3);
            this.Tab2P.Size = new System.Drawing.Size(592, 298);
            this.Tab2P.TabIndex = 1;
            this.Tab2P.Text = "Two-Player";
            this.Tab2P.UseVisualStyleBackColor = true;
            // 
            // Highlight
            // 
            this.Highlight.AutoSize = true;
            this.Highlight.Checked = true;
            this.Highlight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Highlight.Location = new System.Drawing.Point(440, 35);
            this.Highlight.Name = "Highlight";
            this.Highlight.Size = new System.Drawing.Size(125, 17);
            this.Highlight.TabIndex = 4;
            this.Highlight.Text = "Highlight Movements";
            this.Highlight.UseVisualStyleBackColor = true;
            // 
            // Name2P2
            // 
            this.Name2P2.Location = new System.Drawing.Point(143, 72);
            this.Name2P2.Name = "Name2P2";
            this.Name2P2.Size = new System.Drawing.Size(144, 20);
            this.Name2P2.TabIndex = 3;
            this.Name2P2.Text = "Player 2";
            // 
            // Name2P1
            // 
            this.Name2P1.Location = new System.Drawing.Point(143, 32);
            this.Name2P1.Name = "Name2P1";
            this.Name2P1.Size = new System.Drawing.Size(144, 20);
            this.Name2P1.TabIndex = 2;
            this.Name2P1.Text = "Player 1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Player 2 Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Player 1 Name";
            // 
            // TabCG
            // 
            this.TabCG.Controls.Add(this.label10);
            this.TabCG.Controls.Add(this.PlaySpeed);
            this.TabCG.Controls.Add(this.CG2Diff);
            this.TabCG.Controls.Add(this.CG1Diff);
            this.TabCG.Controls.Add(this.label9);
            this.TabCG.Controls.Add(this.label8);
            this.TabCG.Controls.Add(this.NameCG2);
            this.TabCG.Controls.Add(this.NameCG1);
            this.TabCG.Controls.Add(this.label7);
            this.TabCG.Controls.Add(this.label6);
            this.TabCG.Location = new System.Drawing.Point(4, 22);
            this.TabCG.Name = "TabCG";
            this.TabCG.Padding = new System.Windows.Forms.Padding(3);
            this.TabCG.Size = new System.Drawing.Size(592, 298);
            this.TabCG.TabIndex = 2;
            this.TabCG.Text = "Comp-Game";
            this.TabCG.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(215, 213);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Seconds Between Play";
            // 
            // PlaySpeed
            // 
            this.PlaySpeed.Location = new System.Drawing.Point(208, 241);
            this.PlaySpeed.Name = "PlaySpeed";
            this.PlaySpeed.Size = new System.Drawing.Size(144, 20);
            this.PlaySpeed.TabIndex = 13;
            this.PlaySpeed.Text = "0.8";
            // 
            // CG2Diff
            // 
            this.CG2Diff.FormattingEnabled = true;
            this.CG2Diff.Location = new System.Drawing.Point(305, 170);
            this.CG2Diff.Name = "CG2Diff";
            this.CG2Diff.Size = new System.Drawing.Size(137, 21);
            this.CG2Diff.TabIndex = 12;
            this.CG2Diff.Items.AddRange(BotList().ToArray());
            // 
            // CG1Diff
            // 
            this.CG1Diff.FormattingEnabled = true;
            this.CG1Diff.Location = new System.Drawing.Point(85, 170);
            this.CG1Diff.Name = "CG1Diff";
            this.CG1Diff.Size = new System.Drawing.Size(137, 21);
            this.CG1Diff.TabIndex = 11;
            this.CG1Diff.Items.AddRange(BotList().ToArray());
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(302, 134);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Player 2 Difficulty (White)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(82, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Player 1 Difficulty (Black)";
            // 
            // NameCG2
            // 
            this.NameCG2.Location = new System.Drawing.Point(172, 60);
            this.NameCG2.Name = "NameCG2";
            this.NameCG2.Size = new System.Drawing.Size(144, 20);
            this.NameCG2.TabIndex = 8;
            this.NameCG2.Text = "Player 2";
            // 
            // NameCG1
            // 
            this.NameCG1.Location = new System.Drawing.Point(172, 28);
            this.NameCG1.Name = "NameCG1";
            this.NameCG1.Size = new System.Drawing.Size(144, 20);
            this.NameCG1.TabIndex = 7;
            this.NameCG1.Text = "Player 1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "(White) Computer Player 2 Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "(Black) Computer Player 1 Name";
            // 
            // TabOnline
            // 
            this.TabOnline.Controls.Add(this.HostIpInfo);
            this.TabOnline.Controls.Add(this.HostPiecetype);
            this.TabOnline.Controls.Add(this.HostPiecetypeLabel);
            this.TabOnline.Controls.Add(this.PortNo);
            this.TabOnline.Controls.Add(this.PortLabel);
            this.TabOnline.Controls.Add(this.Advanced);
            this.TabOnline.Controls.Add(this.HostLabel);
            this.TabOnline.Controls.Add(this.HostID);
            this.TabOnline.Controls.Add(this.JoinGame);
            this.TabOnline.Controls.Add(this.HostGame);
            this.TabOnline.Controls.Add(this.OnlineID);
            this.TabOnline.Controls.Add(this.label11);
            this.TabOnline.Location = new System.Drawing.Point(4, 22);
            this.TabOnline.Name = "TabOnline";
            this.TabOnline.Padding = new System.Windows.Forms.Padding(3);
            this.TabOnline.Size = new System.Drawing.Size(592, 298);
            this.TabOnline.TabIndex = 3;
            this.TabOnline.Text = "Online";
            this.TabOnline.UseVisualStyleBackColor = true;
            // 
            // HostIpInfo
            // 
            this.HostIpInfo.AutoSize = true;
            this.HostIpInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HostIpInfo.Location = new System.Drawing.Point(436, 194);
            this.HostIpInfo.MaximumSize = new System.Drawing.Size(100, 0);
            this.HostIpInfo.Name = "HostIpInfo";
            this.HostIpInfo.Size = new System.Drawing.Size(100, 39);
            this.HostIpInfo.TabIndex = 18;
            this.HostIpInfo.Text = "*Enter \"localhost\" if both players are on the same network";
            // 
            // HostPiecetype
            // 
            this.HostPiecetype.FormattingEnabled = true;
            this.HostPiecetype.Items.AddRange(new object[] {
            "Black",
            "White"});
            this.HostPiecetype.Location = new System.Drawing.Point(262, 149);
            this.HostPiecetype.Name = "HostPiecetype";
            this.HostPiecetype.Size = new System.Drawing.Size(137, 21);
            this.HostPiecetype.TabIndex = 17;
            this.HostPiecetype.Visible = false;
            // 
            // HostPiecetypeLabel
            // 
            this.HostPiecetypeLabel.AutoSize = true;
            this.HostPiecetypeLabel.Location = new System.Drawing.Point(163, 152);
            this.HostPiecetypeLabel.Name = "HostPiecetypeLabel";
            this.HostPiecetypeLabel.Size = new System.Drawing.Size(66, 13);
            this.HostPiecetypeLabel.TabIndex = 16;
            this.HostPiecetypeLabel.Text = "Player Piece";
            this.HostPiecetypeLabel.Visible = false;
            // 
            // PortNo
            // 
            this.PortNo.Location = new System.Drawing.Point(262, 247);
            this.PortNo.Name = "PortNo";
            this.PortNo.Size = new System.Drawing.Size(152, 20);
            this.PortNo.TabIndex = 15;
            this.PortNo.Text = "2020";
            this.PortNo.Visible = false;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(163, 250);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(43, 13);
            this.PortLabel.TabIndex = 14;
            this.PortLabel.Text = "Port No";
            this.PortLabel.Visible = false;
            // 
            // Advanced
            // 
            this.Advanced.AutoSize = true;
            this.Advanced.Location = new System.Drawing.Point(480, 19);
            this.Advanced.Name = "Advanced";
            this.Advanced.Size = new System.Drawing.Size(75, 17);
            this.Advanced.TabIndex = 13;
            this.Advanced.Text = "Advanced";
            this.Advanced.UseVisualStyleBackColor = true;
            this.Advanced.CheckStateChanged += new System.EventHandler(this.Advanced_CheckStateChanged);
            // 
            // HostLabel
            // 
            this.HostLabel.AutoSize = true;
            this.HostLabel.Location = new System.Drawing.Point(163, 197);
            this.HostLabel.Name = "HostLabel";
            this.HostLabel.Size = new System.Drawing.Size(83, 13);
            this.HostLabel.TabIndex = 12;
            this.HostLabel.Text = "Host IP Address";
            // 
            // HostID
            // 
            this.HostID.Location = new System.Drawing.Point(262, 194);
            this.HostID.Name = "HostID";
            this.HostID.Size = new System.Drawing.Size(152, 20);
            this.HostID.TabIndex = 11;
            // 
            // JoinGame
            // 
            this.JoinGame.AutoSize = true;
            this.JoinGame.Checked = true;
            this.JoinGame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.JoinGame.Location = new System.Drawing.Point(91, 110);
            this.JoinGame.Name = "JoinGame";
            this.JoinGame.Size = new System.Drawing.Size(76, 17);
            this.JoinGame.TabIndex = 10;
            this.JoinGame.Text = "Join Game";
            this.JoinGame.UseVisualStyleBackColor = true;
            this.JoinGame.CheckStateChanged += new System.EventHandler(this.JoinGame_CheckStateChanged);
            // 
            // HostGame
            // 
            this.HostGame.AutoSize = true;
            this.HostGame.Location = new System.Drawing.Point(413, 110);
            this.HostGame.Name = "HostGame";
            this.HostGame.Size = new System.Drawing.Size(79, 17);
            this.HostGame.TabIndex = 9;
            this.HostGame.Text = "Host Game";
            this.HostGame.UseVisualStyleBackColor = true;
            this.HostGame.CheckStateChanged += new System.EventHandler(this.HostGame_CheckStateChanged);
            // 
            // OnlineID
            // 
            this.OnlineID.Location = new System.Drawing.Point(262, 42);
            this.OnlineID.Name = "OnlineID";
            this.OnlineID.Size = new System.Drawing.Size(152, 20);
            this.OnlineID.TabIndex = 6;
            this.OnlineID.Text = "Player";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(163, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Player Name";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(352, 342);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 36);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(475, 342);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 36);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pieceBindingSource
            // 
            this.pieceBindingSource.DataSource = typeof(Checkers.Model.Piece);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 387);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.Gamemode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.Gamemode.ResumeLayout(false);
            this.Tab1P.ResumeLayout(false);
            this.Tab1P.PerformLayout();
            this.Tab2P.ResumeLayout(false);
            this.Tab2P.PerformLayout();
            this.TabCG.ResumeLayout(false);
            this.TabCG.PerformLayout();
            this.TabOnline.ResumeLayout(false);
            this.TabOnline.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pieceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Gamemode;
        private System.Windows.Forms.TabPage Tab1P;
        private System.Windows.Forms.ComboBox PlayerPieceType;
        private System.Windows.Forms.TabPage Tab2P;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Name1p;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Difficulty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Name2P2;
        private System.Windows.Forms.TextBox Name2P1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox Highlight;
        private System.Windows.Forms.TabPage TabCG;
        private System.Windows.Forms.TextBox NameCG2;
        private System.Windows.Forms.TextBox NameCG1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CG2Diff;
        private System.Windows.Forms.ComboBox CG1Diff;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.BindingSource pieceBindingSource;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox PlaySpeed;
        private System.Windows.Forms.TabPage TabOnline;
        private System.Windows.Forms.Label HostLabel;
        private System.Windows.Forms.TextBox HostID;
        private System.Windows.Forms.CheckBox JoinGame;
        private System.Windows.Forms.CheckBox HostGame;
        private System.Windows.Forms.TextBox OnlineID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox Advanced;
        private System.Windows.Forms.TextBox PortNo;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.ComboBox HostPiecetype;
        private System.Windows.Forms.Label HostPiecetypeLabel;
        private System.Windows.Forms.Label HostIpInfo;
    }
}