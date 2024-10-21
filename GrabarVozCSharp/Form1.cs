using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrabarVozCSharp
{
    public partial class Form1 : Form
    {
        string _fileName;
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        public Form1()
        {
            InitializeComponent();
        }

        private void grabar()
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Media (.wav)|*.wav" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    _fileName = sfd.FileName;
                    mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
                    mciSendString("record recsound", "", 0, 0);
                    btnDetener.Enabled = true;
                    btnGrabar.Enabled = false;
                }
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            grabar();
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            mciSendString("save recsound " + _fileName, "", 0, 0);
            mciSendString("close recsound", "", 0, 0);
            btnGrabar.Enabled = true;
            btnDetener.Enabled = false;
        }

        private void btnReproducir_Click(object sender, EventArgs e)
        {
            SoundPlayer soundPlayer = new SoundPlayer(_fileName);
            soundPlayer.Play();
        }
    }
}
