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
    public partial class loadchat : Form
    {
        int startpoint = 0;
        public loadchat()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint++;
            if(startpoint==20){
                startpoint = 0;
                timer1.Stop();
                this.Hide();
                chat mychat = new chat();
                mychat.Show();
            }
        }
    }
}
