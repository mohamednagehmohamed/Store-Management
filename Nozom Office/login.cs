using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using WMPLib;
using System.Configuration;
namespace Nozom_Office
{
    public partial class login : Form
    {
        WindowsMediaPlayer mplayer = new WindowsMediaPlayer();
        WindowsMediaPlayer mplayer2 = new WindowsMediaPlayer();
        DateTime mydate = DateTime.Now;
        string aborawashtime = "01:45 PM";
        string esharatime = "01:30 PM";
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\SQLEXPRESS;Initial Catalog=nozommedodb;Integrated Security=True");
       // SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\S2008;Initial Catalog=nozomdb;Integrated Security=True");
        public login()
        {
            InitializeComponent();
            mplayer.URL = "4chan Timer.mp3";
            mplayer2.URL = "Iphone Timer.mp3";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtusername.Text = "";
            txtpass.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtusername.Text.Trim() == "" || txtpass.Text.Trim() == "")
            {
                MessageBox.Show("من فضلك أدخل جميع البيانات..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                try
                {
                    con.Open();
                    string query = "select count(*) from login where username='"+txtusername.Text+"'and pass='"+txtpass.Text+"'";
                    SqlDataAdapter sda = new SqlDataAdapter(query,con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.Show();
                    }
                    else {
                        MessageBox.Show("خطأ في اسم المستخدم وكلمة السر", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
                catch(Exception ex){
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin ad = new admin();
            ad.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToShortTimeString() == aborawashtime)
            {
               // timer1.Enabled = true;
                mplayer.controls.play();
                

            }
            else if (DateTime.Now.ToShortTimeString() == esharatime)
            {
                timer1.Enabled = true;
                mplayer2.controls.play();
                
            }
            else
            {
                mplayer.controls.stop();
                mplayer2.controls.stop();
                timer1.Enabled = false;
            }
        }

        private void login_Load(object sender, EventArgs e)
        {
            string con_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        }
    }
}
