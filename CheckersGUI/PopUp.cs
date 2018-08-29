using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checkers.DataFixture;
using Checkers.Model;
using CheckersGUI;

namespace CheckersGUI
{
    public partial class PopUp : Form
    {
        public PopUp(string txt, Bitmap image)
        {
            InitializeComponent();
            WinningText.Text = txt;
            WinningPic.Image = image;
            System.Media.SoundPlayer WinnerSound = new System.Media.SoundPlayer(Properties.Resources.achievement_mp3_sound__online_audio_converter_com_);           
            WinnerSound.Load();
            WinnerSound.PlaySync();
        }
    }
}
