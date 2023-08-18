using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nozom_Office
{
    public partial class allstores : Form
    {
        public allstores()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            store2 s2 = new store2();
            s2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            store3 s3 = new store3();
            s3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            store5 s5 = new store5();
            s5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            store6 s6 = new store6();
            s6.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            store11 s11 = new store11();
            s11.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            store14 s14 = new store14();
            s14.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            store15 s15 = new store15();
            s15.Show();
        }

        private void allstores_Load(object sender, EventArgs e)
        {
            string con_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        }
    }
}
