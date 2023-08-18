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
    public partial class store2 : Form
    {
      //  int sumhrary = 0;
        //int sumcitizen = 0;
        bool datefound = false;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\SQLEXPRESS;Initial Catalog=nozommedodb;Integrated Security=True");
       // SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EOHECS5\S2008;Initial Catalog=nozomdb;Integrated Security=True");
        public store2()
        {
            InitializeComponent();
            //checkifdatefound();
        }
     /*   void changetotalwhenupdate() {
            try
            {
                con.Open();
                string hraryquery = "select count(*) from store2 where date='"+dateTimePicker1.Value.Date+"'";
                SqlDataAdapter sda = new SqlDataAdapter(hraryquery, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sumhrary = (int.Parse(lblhrary.Text) - int.Parse(dt.Rows[0][0].ToString()))+int.Parse(txthrary.Text);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/
        void getalldata()
        {
            try {
                con.Open();
                string query = "select * from store2 order by date desc";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message, "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void newgetall()
        {
            try
            {
                con.Open();
                string queryhrary = "select sum(hrary) from store2";
                string querycitizen = "select sum(citizen) from store2";
                SqlDataAdapter sda = new SqlDataAdapter(queryhrary, con);
                SqlDataAdapter sda2 = new SqlDataAdapter(querycitizen, con);
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                sda.Fill(dt1);
                sda2.Fill(dt2);
                lblhrary.Text = dt1.Rows[0][0].ToString();
                lblcitizen.Text = dt2.Rows[0][0].ToString();
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
         void checkifdatefound() {
             try
             {
                 con.Open();
                 string query = "select count(*) from store2 where date='"+dateTimePicker1.Value.Date+"'";
                 SqlDataAdapter sda = new SqlDataAdapter(query,con);
                 DataTable dt = new DataTable();
                 sda.Fill(dt);
                 if (dt.Rows[0][0].ToString() == "1")
                 {
                     datefound = true;
                    // MessageBox.Show("هذا التاريخ غير موجود", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
                 else {
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
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            allstores all = new allstores();
            all.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkifdatefound();
            if (txthrary.Text.Trim() == "" || txtcitizen.Text.Trim() == "" )
            {
                MessageBox.Show("من فضلك أدخل جميع البيانات..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dateTimePicker1.Value.Date > DateTime.Now.Date)
            {
                // dateTimePicker1.Enabled = false;
                MessageBox.Show("من فضلك أدخل تاريخ صالح..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           /* else if (datefound == false)
            {
                try
                {
                    con.Open();
                    string queryhrary = "select sum(hrary) from store2 where date='"+dateTimePicker1.Value.Date+"'";
                    string querycitizen = "select sum(citizen) from store2 where date='" + dateTimePicker1.Value.Date + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(queryhrary, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(querycitizen, con);  
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    sda.Fill(dt1);
                    sda2.Fill(dt2);
                    string updatequery = "update store2 set hrary='" + (int.Parse(txthrary.Text) + int.Parse(dt1.Rows[0][0].ToString())) + "',citizen='" + (int.Parse(txtcitizen.Text) + int.Parse(dt2.Rows[0][0].ToString())) + "'where date='" + dateTimePicker1.Value.Date + "'";
                    SqlCommand cmd = new SqlCommand(updatequery,con);
                    lblhrary.Text = dt1.Rows[0][0].ToString();
                    lblcitizen.Text = dt2.Rows[0][0].ToString();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } */
                
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into store2 values('" + int.Parse(txthrary.Text) + "','" + int.Parse(txtcitizen.Text) + "','"+dateTimePicker1.Value.Date+"')";
                    SqlCommand cmd = new SqlCommand(query, con);
                   
                    DialogResult message = MessageBox.Show("هل تريد تأكيد اضافة بيانات المخزن", "Nozom Office", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (message == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else {
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
                    txtcitizen.Text = "";
                    txthrary.Text = "";
                    getalldata();
                    newgetall();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void store2_Load(object sender, EventArgs e)
        {
            getalldata();
           // getallhrary();
            newgetall();
            string con_string = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkifdatefound();
            if (txthrary.Text.Trim() == "" || txtcitizen.Text.Trim() == "" )
            {
                MessageBox.Show("من فضلك أدخل جميع البيانات..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            else if (dateTimePicker1.Value.Date>DateTime.Now.Date) {
               // dateTimePicker1.Enabled = false;
                MessageBox.Show("من فضلك أدخل تاريخ صالح..", "Nozom Office", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (datefound==false)
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
                    string query = "update store2 set hrary='" + int.Parse(txthrary.Text) + "',citizen='" + int.Parse(txtcitizen.Text) + "',date='"+dateTimePicker1.Value.Date+"'where date='"+dateTimePicker1.Value.Date+"'";
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
                    txtcitizen.Text = "";
                    txthrary.Text = "";
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
                    string query = "delete from store2 where date='" + dateTimePicker1.Value.Date + "'";
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            Graphics g = this.CreateGraphics();
            mybmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics mg = Graphics.FromImage(mybmp);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            printPreviewDialog1.ShowDialog();
           /* try
            {
                con.Open();
                string query = "update totalstore2 set totalhrary='" + int.Parse(lblhrary.Text) + "',totalcitizen='" + int.Parse(lblcitizen.Text) + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("تمت حفظ البيانات");
               
                con.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txthrary.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
          txtcitizen.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
           // dateTimePicker1.Value = dataGridView1.SelectedRows[0].Cells[2].ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (lblcitizen.Bounds.IntersectsWith(lbl.Bounds))
            {
                lbl.Left += 10;
            }
            if (lblhrary.Bounds.IntersectsWith(label1.Bounds))
            {
                label1.Left += 10;
            }
        }
        Bitmap mybmp;
        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.Bounds.Height);
            Graphics graph = Graphics.FromImage(bmp);
            graph.CopyFromScreen(0,0,0,0,bmp.Size);
            bmp.Save("ss.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Diagnostics.Process.Start("ss.jpg");
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lblcitizen_Click(object sender, EventArgs e)
        {

        }

        private void lblhrary_Click(object sender, EventArgs e)
        {

        }

        private void txtcitizen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txthrary_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(mybmp,0,0);
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
