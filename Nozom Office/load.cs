using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
namespace Nozom_Office
{
    public partial class load : Form
    {
        WindowsMediaPlayer mplayer = new WindowsMediaPlayer();
        int startpoint = 0;
        public load()
        {
            InitializeComponent();
            mplayer.URL = "just-relax-11157.mp3";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint++;
            progressBar1.Value = startpoint;
            if(startpoint==100){
                startpoint = 0;
                progressBar1.Value = 0;
                timer1.Enabled = false;
                this.Hide();
                login log = new login();
                log.Show();
            }
        }

        private void load_Load(object sender, EventArgs e)
        {
          
            mplayer.controls.play();
        }
    }
}
