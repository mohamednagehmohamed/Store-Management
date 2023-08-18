using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Data.SqlClient;

namespace Nozom_Office
{
    public partial class Form1 : Form
    {
  
        SpeechSynthesizer ssynthize = new SpeechSynthesizer();
        Color[] mylabels = { Color.White, Color.MidnightBlue };
        Color[] mytimerlabels = { Color.Yellow, Color.YellowGreen };
        Random rand = new Random();
      //  int mydatehour = DateTime.Now.Hour;
       // int mydateminute = DateTime.Now.Minute;
        DateTime mydate = DateTime.Now;
        string aborawashtime = "01:45 PM";
        string esharatime = "01:30 PM";
       // int count = 0;
        public Form1()
        {
            InitializeComponent();
  
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ssynthize.SpeakAsync("Bye Bye My Friend");
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = rand.Next(mylabels.Length);
            lblnozom.ForeColor=mylabels[x];
            lblfarag.ForeColor = mylabels[x];
            lblwatania.ForeColor = mylabels[x];
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            soliderdata solider = new soliderdata();
            solider.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            allstores all = new allstores();
            all.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            loadchat mychat = new loadchat();
            mychat.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer3.Enabled = true;
           // MessageBox.Show(DateTime.Now.ToShortTimeString());
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToShortTimeString()== aborawashtime)
            {
                timer2.Enabled = true;
                //mplayer.controls.play();
                int x = rand.Next(mylabels.Length);
                lblaborawash.ForeColor = mytimerlabels[x];
                
            }
            else if (DateTime.Now.ToShortTimeString() == esharatime)
            {
                timer2.Enabled = true;
               // mplayer2.controls.play();
                int x = rand.Next(mylabels.Length);
                lbleshara.ForeColor = mytimerlabels[x];
            }
            else {
               
                timer2.Enabled=false;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            sendsms sms = new sendsms();
            sms.Show();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToString();
        }
    }
}
