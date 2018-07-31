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
            this.Column = new System.Windows.Forms.ComboBox();
            this.Messages = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Row = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
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
            // 
            // Column
            // 
            this.Column.FormattingEnabled = true;
            this.Column.Location = new System.Drawing.Point(475, 52);
            this.Column.Name = "Column";
            this.Column.Size = new System.Drawing.Size(90, 21);
            this.Column.TabIndex = 1;
            // 
            // Messages
            // 
            this.Messages.Location = new System.Drawing.Point(12, 355);
            this.Messages.Name = "Messages";
            this.Messages.Size = new System.Drawing.Size(326, 83);
            this.Messages.TabIndex = 2;
            this.Messages.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(472, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Column";
            // 
            // Row
            // 
            this.Row.FormattingEnabled = true;
            this.Row.Location = new System.Drawing.Point(475, 109);
            this.Row.Name = "Row";
            this.Row.Size = new System.Drawing.Size(90, 21);
            this.Row.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(472, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Row";
            // 
            // Start1PGame
            // 
            this.Start1PGame.Location = new System.Drawing.Point(475, 247);
            this.Start1PGame.Name = "Start1PGame";
            this.Start1PGame.Size = new System.Drawing.Size(90, 41);
            this.Start1PGame.TabIndex = 6;
            this.Start1PGame.Text = "Start 1P Game";
            this.Start1PGame.UseVisualStyleBackColor = true;
            this.Start1PGame.Click += new System.EventHandler(this.Start1PGame_Click);
            // 
            // Start2PGame
            // 
            this.Start2PGame.Location = new System.Drawing.Point(475, 308);
            this.Start2PGame.Name = "Start2PGame";
            this.Start2PGame.Size = new System.Drawing.Size(90, 41);
            this.Start2PGame.TabIndex = 7;
            this.Start2PGame.Text = "Start 2P Game";
            this.Start2PGame.UseVisualStyleBackColor = true;
            // 
            // Quit
            // 
            this.Quit.Location = new System.Drawing.Point(475, 397);
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(90, 41);
            this.Quit.TabIndex = 8;
            this.Quit.Text = "Quit";
            this.Quit.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.Start2PGame);
            this.Controls.Add(this.Start1PGame);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Row);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Messages);
            this.Controls.Add(this.Column);
            this.Controls.Add(this.Grid);
            this.Name = "Form1";
            this.Text = "Checkers";
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Grid;
        private System.Windows.Forms.ComboBox Column;
        private System.Windows.Forms.RichTextBox Messages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Row;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Start1PGame;
        private System.Windows.Forms.Button Start2PGame;
        private System.Windows.Forms.Button Quit;
    }
}

