using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using SMSapplication;
namespace Nozom_Office
{
    public partial class sendsms : Form
    {
        public string sms, phone;
        clsSMS cls = new clsSMS();
        SerialPort myport = new SerialPort();
        public sendsms()
        {
            InitializeComponent();
        }
        void load() {
            string[] ports = SerialPort.GetPortNames();
            foreach (string item in ports)
            {
                comboBox1.Items.Add(item);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void sendsms_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                load();
                for (int i = 0; i <= comboBox1.Items.Count - 1;i++ ) {
                    comboBox1.SelectedIndex = i;
                    myport = cls.OpenPort(comboBox1.Text,Convert.ToInt32(9600),Convert.ToInt32(8),Convert.ToInt32(300),Convert.ToInt32(300));
                    phone = txtphone.Text;
                    sms = txtmessage.Text;
                    try {
                        if (cls.sendMsg(myport, phone, sms))
                        {
                            cls.ClosePort(myport);
                        }
                    }
                    catch(Exception ex){
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            
            }
        }
    }
}
