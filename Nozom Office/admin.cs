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
using System.Configuration;
namespace Nozom_Office
{
    public partial class admin : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\SQLEXPRESS;Initial Catalog=nozommedodb;Integrated Security=True");
       // SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\S2008;Initial Catalog=nozomdb;Integrated Security=True");
        public admin()
        {
            InitializeComponent();
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            login log = new login();
            log.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            login log = new login();
            log.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtadminpass.Text == "54321")
            {
                panel2.Visible = false;
                panel1.Visible = true;
            }
            else {
                MessageBox.Show("خطأ في كلمة السر", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtusername.Text.Trim() == "" || txtpass.Text.Trim() == "")
            {
                MessageBox.Show("من فضلك أدخل جميع البيانات..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update login set username='"+txtusername.Text+"',pass='"+txtpass.Text+"'";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("لقد تم تحديث اسم المستخدم وكلمة المرور");
                    con.Close();
                    this.Hide();
                    login log = new login();
                    log.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void admin_Load(object sender, EventArgs e)
        {
            string con_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        }
    }
}
