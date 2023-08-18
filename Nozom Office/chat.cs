using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using WMPLib;
using System.Data.SqlClient;
namespace Nozom_Office
{
    public partial class chat : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\SQLEXPRESS;Initial Catalog=nozommedodb;Integrated Security=True");
       // SoundPlayer s = new SoundPlayer();
        WindowsMediaPlayer mplayer = new WindowsMediaPlayer();
        Socket sck;
        EndPoint eplocal, epremote;
        bool isconnected = false;
        byte[] buffer;
        public chat()
        {
            InitializeComponent();
            mplayer.URL = "mixkit-doorbell-single-press-333.mp3";
        }
        string Getlocalip() {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in host.AddressList){
            if(ip.AddressFamily==AddressFamily.InterNetwork){
                return ip.ToString();
            }
            }

            return "127.0.0.1";
        }
        void getalldata()
        {
            try
            {
                con.Open();
                string query = "select message from mychat";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                listbox1.Items.Add(dt.Rows[0][0].ToString());
               /* for (int i = 0; i < dt.Rows.Count;i++ ) {
                    listbox1.Items.Add(dt.Rows[0][0].ToString());
                }*/
                //SqlCommandBuilder builder = new SqlCommandBuilder(sda);
               // var ds = new DataSet();
               // sda.Fill(ds);
               // listbox1.Items.Add(ds.Tables[0]);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void messagecallback(IAsyncResult aresult) {
            try {
                byte[] recievedata = new byte[1500];
                recievedata = (byte[])aresult.AsyncState;
                ASCIIEncoding encoding = new ASCIIEncoding();
                string recievemessage = encoding.GetString(recievedata);
                listbox1.Items.Add("صديقي::> " + recievemessage);
                buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(messagecallback), buffer);
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void chat_Load(object sender, EventArgs e)
        {
            sck = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReuseAddress,true);
            txtlocalip.Text = Getlocalip();
            txtforignip.Text = Getlocalip();
          //  getalldata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtlocalport.Text.Trim() == "" || txtlocalip.Text.Trim() == "" || txtforignip.Text.Trim() == "" || txtforignport.Text.Trim() == "")
            {
                MessageBox.Show("من فضلك أدخل جميع البيانات", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else {
                try {
                    isconnected = true;
                    eplocal = new IPEndPoint(IPAddress.Parse(txtlocalip.Text), Convert.ToInt32(txtlocalport.Text));
                    sck.Bind(eplocal);
                    epremote = new IPEndPoint(IPAddress.Parse(txtforignip.Text), Convert.ToInt32(txtforignport.Text));
                    sck.Connect(epremote);
                    buffer = new byte[1500];
                    sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(messagecallback), buffer);
                }
                catch(Exception ex){
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtsend.Text.Trim()==""){
                MessageBox.Show("من فضلك أدخل الرساله أولا", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                else if(isconnected==false){
                    MessageBox.Show("من فضلك تأكد من الاتصال أولا", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else{
                try
                {
                    ASCIIEncoding aencoding = new ASCIIEncoding();
                    byte[] sendingmessage = new byte[1500];
                    sendingmessage = aencoding.GetBytes(txtsend.Text);
                    sck.Send(sendingmessage);
                    mplayer.controls.play();
                    listbox1.Items.Add("أنا::>" + txtsend.Text);
                   /* txtsend.Text = "";
                    con.Open();
                    string query = "insert into mychat values('"+txtsend.Text+"')";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    con.Close();*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            chat c = new chat();
            c.Show();
        }
    }
}
