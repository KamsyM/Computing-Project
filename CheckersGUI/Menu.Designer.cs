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
            this.Name2P2 = new System.Windows.Forms.TextBox();
            this.Name2P1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Gamemode.SuspendLayout();
            this.Tab1P.SuspendLayout();
            this.Tab2P.SuspendLayout();
            this.SuspendLayout();
            // 
            // Gamemode
            // 
            this.Gamemode.Controls.Add(this.Tab1P);
            this.Gamemode.Controls.Add(this.Tab2P);
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
            this.Difficulty.Items.AddRange(new object[] {
            "Beginner",
            "Intermediate",
            "Advanced"});
            this.Difficulty.Location = new System.Drawing.Point(205, 181);
            this.Difficulty.Name = "Difficulty";
            this.Difficulty.Size = new System.Drawing.Size(137, 21);
            this.Difficulty.TabIndex = 3;
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
    }
}