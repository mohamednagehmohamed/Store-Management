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
namespace Nozom_Office
{
    public partial class soliderdata : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\SQLEXPRESS;Initial Catalog=nozommedodb;Integrated Security=True");
       // SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\S2008;Initial Catalog=nozomdb;Integrated Security=True");
        public soliderdata()
        {
            InitializeComponent();
        }
        void getalldate() {
            con.Open();
            string query = "select * from soliders";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource=ds.Tables[0];
            con.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtid.Text.Trim() == "" || txtname.Text.Trim() == ""||txtaddress.Text.Trim() == "" || txtage.Text.Trim() == ""||cmbdofaa.SelectedItem==null)
            {
                MessageBox.Show("من فضلك أدخل جميع البيانات..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into soliders values('" + txtid.Text + "','" + txtname.Text + "','" + int.Parse(txtage.Text) + "','"+txtaddress.Text+"','"+cmbdofaa.SelectedItem+"')";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تمت اضافة بيانات الجندي");
                    con.Close();
                    getalldate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void soliderdata_Load(object sender, EventArgs e)
        {
            getalldate();
            string con_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtid.Text.Trim()=="") {
                MessageBox.Show("من فضلك أدخل الرقم التعريفي للجندي..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtid.Text.Trim() == "" || txtname.Text.Trim() == "" || txtaddress.Text.Trim() == "" || txtage.Text.Trim() == "" || cmbdofaa.SelectedItem == null)
            {
                MessageBox.Show("من فضلك أدخل جميع البيانات..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update soliders set id='" +int.Parse (txtid.Text) + "',name='" + txtname.Text + "',age='" +int.Parse (txtage.Text) + "',address='" + txtaddress.Text + "',dofaa='" + cmbdofaa.SelectedItem + "'where id='" + int.Parse(txtid.Text) + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم تحديث بيانات الجندي بيانات الجندي", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    getalldate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtid.Text.Trim() == "" )
            {
                MessageBox.Show("من فضلك أدخل الرقم التعريفي للجندي..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from soliders where id='"+int.Parse(txtid.Text)+"'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم حذف بيانات الجندي ");
                    con.Close();
                    getalldate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.Bounds.Height);
            Graphics graph = Graphics.FromImage(bmp);
            graph.CopyFromScreen(0,0,0,0,bmp.Size);
            bmp.Save("ss.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Diagnostics.Process.Start("ss.jpg");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                txtid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtage.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtaddress.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
         
            //cmbdofaa.SelectedItem = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            try {
                con.Open();
                string query = "select * from ";
                con.Close();
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
    }
}
