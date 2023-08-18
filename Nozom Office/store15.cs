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
    public partial class store15 : Form
    {
        bool datefound = false;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\SQLEXPRESS;Initial Catalog=nozommedodb;Integrated Security=True");
       // SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\S2008;Initial Catalog=nozomdb;Integrated Security=True");
        public store15()
        {
            InitializeComponent();
        }
        void getalldata()
        {
            try
            {
                con.Open();
                string query = "select * from store15 order by date desc";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void newgetall()
        {
            try
            {
                con.Open();
                string queryantena = "select sum(antena) from store15";
                string queryscanner = "select sum(scanner) from store15";
                string queryheadphones = "select sum(headphones) from store15";
                string querylaserprinter = "select sum(laserprinter) from store15";
                SqlDataAdapter sda = new SqlDataAdapter(queryantena, con);
                SqlDataAdapter sda2 = new SqlDataAdapter(queryscanner, con);
                SqlDataAdapter sda3 = new SqlDataAdapter(queryheadphones, con);
                SqlDataAdapter sda4 = new SqlDataAdapter(querylaserprinter, con);
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataTable dt4 = new DataTable();
                sda.Fill(dt1);
                sda2.Fill(dt2);
                sda3.Fill(dt3);
                sda4.Fill(dt4);
                lblantenna.Text = dt1.Rows[0][0].ToString();
                lblscanner.Text = dt2.Rows[0][0].ToString();
                lblheadphones.Text = dt3.Rows[0][0].ToString();
                lbllaserprint.Text = dt4.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /* void getallhrary(){
             try
             {
                 con.Open();
                 string queryhrary = "select totalhrary from totalstore2";
                 string querycitizen = "select totalcitizen from totalstore2";
                 SqlDataAdapter sda = new SqlDataAdapter(queryhrary,con);
                 SqlDataAdapter sda2 = new SqlDataAdapter(querycitizen, con);
                 DataTable dt1 = new DataTable();
                 DataTable dt2 = new DataTable();
                 sda.Fill(dt1);
                 sda2.Fill(dt2);
                 lblhrary.Text = dt1.Rows[0][0].ToString();
                 lblcitizen.Text = dt2.Rows[0][0].ToString();
                 con.Close();
             }
             catch(Exception ex){
                 MessageBox.Show(ex.Message);
             }
         }*/
        void checkifdatefound()
        {
            try
            {
                con.Open();
                string query = "select count(*) from store15 where date='" + dateTimePicker1.Value.Date + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    datefound = true;
                    // MessageBox.Show("هذا التاريخ غير موجود", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    datefound = false;
                    // MessageBox.Show("هذا التاريخ  موجود", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtantena.Text.Trim() == "" || txtscanner.Text.Trim() == "" || txtheadphones.Text.Trim() == "" || txtlaserprint.Text == "")
            {
                MessageBox.Show("من فضلك أدخل جميع البيانات..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                // dateTimePicker1.Enabled = false;
                MessageBox.Show("من فضلك أدخل تاريخ صالح..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            else
            {
                try
                {
                    con.Open();
                    string query = "insert into store15 values('" + int.Parse(txtantena.Text) + "','" + int.Parse(txtscanner.Text) + "','" + int.Parse(txtheadphones.Text) + "','" + int.Parse(txtlaserprint.Text) + "','" + dateTimePicker1.Value.Date + "')";
                    SqlCommand cmd = new SqlCommand(query, con);

                    DialogResult message = MessageBox.Show("هل تريد تأكيد اضافة بيانات المخزن", "Nozom Office", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (message == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.Cancel();
                    }
                    /*if (int.Parse(lblhrary.Text) == 0)
                    {
                        sumhrary += int.Parse(txthrary.Text);
                    }
                    else {
                        sumhrary = int.Parse(txthrary.Text) + sumhrary + int.Parse(lblhrary.Text);
                    }*/
                    /* sumhrary = int.Parse(txthrary.Text) + sumhrary + int.Parse(lblhrary.Text);
                     lblhrary.Text = sumhrary.ToString();
                     sumcitizen += int.Parse(txtcitizen.Text) + sumcitizen + int.Parse(lblcitizen.Text);
                     lblcitizen.Text = sumcitizen.ToString();*/
                    con.Close();
                    txtantena.Text = "";
                    txtscanner.Text = "";
                    txtheadphones.Text = "";
                    txtlaserprint.Text = "";
                    getalldata();
                    newgetall();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            allstores all = new allstores();
            all.Show();
        }

        private void store15_Load(object sender, EventArgs e)
        {
            getalldata();
            // getallhrary();
            newgetall();
            string con_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkifdatefound();
            if (txtantena.Text.Trim() == "" || txtscanner.Text.Trim() == "" || txtheadphones.Text.Trim() == "" || txtscanner.Text == "")
            {
                MessageBox.Show("من فضلك أدخل جميع البيانات..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                // dateTimePicker1.Enabled = false;
                MessageBox.Show("من فضلك أدخل تاريخ صالح..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (datefound == false)
            {
                MessageBox.Show("هذا التاريخ غير موجود", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                /* try
                 {
                     con.Open();
                     string query = "select count(*) from store2 where date='" + dateTimePicker1.Value.Date + "'";
                     SqlDataAdapter sda = new SqlDataAdapter(query, con);
                     DataTable dt = new DataTable();
                     sda.Fill(dt);
                     if (dt.Rows[0][0].ToString()=="1")
                     {
                         try
                         {
                            con.Open();
                             string updatequery = "update store2 set hrary='" + int.Parse(txthrary.Text) + "',citizen='" + int.Parse(txtcitizen.Text) + "',date='" + dateTimePicker1.Value.Date + "'where date='" + dateTimePicker1.Value.Date + "'";
                             SqlCommand cmd = new SqlCommand(updatequery, con);
                             cmd.ExecuteNonQuery();
                             MessageBox.Show("تم تحديث بيانات المخزن ");
                             con.Close();
                             getalldata();
                            // changetotalwhenupdate();
                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show(ex.Message);
                         }
                     }
                     else
                     {
                         MessageBox.Show("هذا التاريخ غير موجود", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     con.Close();
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }*/
                try
                {
                    con.Open();
                    string query = "update store15 set antena='" + int.Parse(txtantena.Text) + "',scanner='" + int.Parse(txtscanner.Text) + "',headphones='" + int.Parse(txtheadphones.Text) + "',laserprinter='" + int.Parse(txtlaserprint.Text) + "',date='" + dateTimePicker1.Value.Date + "'where date='" + dateTimePicker1.Value.Date + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    DialogResult message = MessageBox.Show("هل تريد تأكيد تحديث بيانات المخزن", "Nozom Office", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (message == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.Cancel();
                    }

                    con.Close();
                    txtantena.Text = "";
                    txtscanner.Text = "";
                    txtheadphones.Text = "";
                    txtlaserprint.Text = "";
                    getalldata();
                    newgetall();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkifdatefound();
            if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                // dateTimePicker1.Enabled = false;
                MessageBox.Show("من فضلك أدخل تاريخ صالح..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (datefound == false)
            {
                MessageBox.Show("هذا التاريخ غير موجود", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                /* try
                 {
                     con.Open();
                     string query = "select count(*) from store2 where date='" + dateTimePicker1.Value.Date + "'";
                     SqlDataAdapter sda = new SqlDataAdapter(query, con);
                     DataTable dt = new DataTable();
                     sda.Fill(dt);
                     if (dt.Rows[0][0].ToString() == "1")
                     {
                         try
                         {
                            // con.Open();
                             string deletequery = "delete from store2 where date='" + dateTimePicker1.Value.Date + "'";
                             SqlCommand cmd = new SqlCommand(deletequery, con);
                             cmd.ExecuteNonQuery();
                             MessageBox.Show("تم حذف بيانات المخزن");
                             sumhrary = int.Parse(lblhrary.Text) - int.Parse(txthrary.Text);
                             lblhrary.Text = sumhrary.ToString();
                             sumcitizen = int.Parse(lblcitizen.Text) - int.Parse(txtcitizen.Text);
                             lblcitizen.Text = sumcitizen.ToString();
                             this.Hide();
                             store2 s2 = new store2();
                             s2.Show();
                            // con.Close();
                             getalldata();
                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show(ex.Message);
                         }
                     }
                     else
                     {
                         MessageBox.Show("هذا التاريخ غير موجود", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     con.Close();
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }*/
                try
                {
                    con.Open();
                    string query = "delete from store15 where date='" + dateTimePicker1.Value.Date + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    DialogResult message = MessageBox.Show("هل تريد تأكيد حذف بيانات المخزن", "Nozom Office", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (message == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.Cancel();
                    }

                    con.Close();
                    getalldata();
                    newgetall();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graph = Graphics.FromImage(bmp);
            graph.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            bmp.Save("ss.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Diagnostics.Process.Start("ss.jpg");
        }
        Bitmap mybmp;
        private void button5_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            mybmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics mg = Graphics.FromImage(mybmp);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(mybmp, 0, 0);
        }
    }
}
