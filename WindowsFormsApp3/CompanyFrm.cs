using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace WindowsFormsApp3
{
    public partial class CompanyFrm : Form
    {
        WorldSkillsAirPlanDbEntities context = new WorldSkillsAirPlanDbEntities();
        public CompanyFrm()
        {
            InitializeComponent();
            timer1.Start();
        }
        public void createImage()
        {
            #region create image
            int width = 5;
            int height = 10;
            Bitmap image = new Bitmap(width, height);
            Color backgroundColor = Color.White;
            Color otherColor = Color.Blue;
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.Clear(backgroundColor);
                Point[] points = new Point[] {
                    new Point(0, 0), new Point(width / 2, height), new Point(width, 0)
                };
                using (Brush brush = new SolidBrush(otherColor))
                {
                    graphics.FillPolygon(brush, points);
                }
            }
            string imagePath = "sample_image.png";
            image.Save(imagePath, ImageFormat.Png);
            Console.WriteLine("تصویر با موفقیت ساخته شد: " + imagePath);

            #endregion create image
        }
        private void CompanyFrm_Load(object sender, EventArgs e)
        {
            pickerAge.CustomFormat = "yyyy";
            /*data for user signup*/
            comboRull.DataSource = context.RullTbls.Where(x => x.RullId != 1 && x.RullId != 2).ToList();
            comboRull.DisplayMember = "RullLabel";
            comboRull.ValueMember = "RullId";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.label1.Text = $"{datetime.Hour}:{datetime.Minute}:{datetime.Second}";
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private bool checkNationalCode(string nationalCode)
        {
            try
            {
                string check = Regex.Replace(nationalCode, @"\s", "");
                string patt = $@"^\d{{{txtNationalCode.Text.Length}}}$";
                return Regex.IsMatch(check, patt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private bool checkYear(string year)
        {
            try
            {
                string check = Regex.Replace(year, @"\s", "");
                string patt = $@"^\d{{{txtYear.Text.Length}}}$";
                return Regex.IsMatch(check, patt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)comboRull.SelectedValue;
                if (id == 3)
                {
                    if (txtName.Text != string.Empty && txtNationalCode.Text != string.Empty && txtYear.Text != string.Empty && txtLastName.Text != string.Empty && pictureBox1.Image != null)
                    {
                        MainUserTbl user = new MainUserTbl();
                        user.Birthday = pickerAge.Value;
                        user.Rull = id;
                        user.Email = txtEmail.Text;
                        user.NationalCode = txtNationalCode.Text;
                        user.Name = txtName.Text;
                        user.LastName = txtLastName.Text;
                        user.Birthday = pickerAge.Value;
                        user.employmentDate = txtYear.Text;
                        context.MainUserTbls.Add(user);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("لطفا اطلاعات را کامل پر نمایید");
                    }
                }
                else
                {
                    if (txtName.Text != string.Empty && txtNationalCode.Text != string.Empty && txtYear.Text != string.Empty && txtLastName.Text != string.Empty && pictureBox1.Image != null)
                    {
                        MainUserTbl user = new MainUserTbl();
                        user.Birthday = pickerAge.Value;
                        user.Rull = id;
                        user.Email = txtEmail.Text;
                        user.NationalCode = txtNationalCode.Text;
                        user.Name = txtName.Text;
                        user.LastName = txtLastName.Text;
                        user.Birthday = pickerAge.Value;
                        user.employmentDate = txtYear.Text;
                        context.MainUserTbls.Add(user);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtNationalCode_TextChanged(object sender, EventArgs e)
        {
            if (checkNationalCode(txtNationalCode.Text))
            {

            }
            else
            {
                MessageBox.Show("کص ننویس");
                txtNationalCode.Text = string.Empty;
            }
        }
        public string photoName;
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog sd = new OpenFileDialog();
            sd.Filter = "All files (*.*)|*.*";
            sd.ShowDialog();
            File.Copy(sd.FileName, Path.Combine(@"C:\Users\mani\Desktop\WindowsFormsApp3\WindowsFormsApp3\bin\Debug", Path.GetFileName(sd.FileName)), true);
            pictureBox1.Load(sd.SafeFileName);
            photoName = sd.SafeFileName;
            textBox6.Text = sd.FileName;
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            if (checkYear(txtYear.Text))
            {

            }
            else
            {
                MessageBox.Show("کص ننویس");
                txtYear.Text = string.Empty;
            }
        }
    }
}
