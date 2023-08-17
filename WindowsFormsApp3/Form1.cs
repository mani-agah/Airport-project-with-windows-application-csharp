using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime today = DateTime.Today;
                string dateFormat = "yyyy MMM d";
                string formattedDate = today.ToString(dateFormat);
                label2.Text = formattedDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.label1.Text = $"{datetime.Hour}:{datetime.Minute}:{datetime.Second}";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                LoginFrm frm = new LoginFrm();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
