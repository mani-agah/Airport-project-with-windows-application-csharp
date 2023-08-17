using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class LoginFrm : Form
    {
        public LoginFrm()
        {
            InitializeComponent();
            timer1.Start();
            timer1.Interval= 1000;
        }
        public string text;
        static string GenerateRandomCaptcha(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random rand = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        static void DrawRandomShapes(Graphics graphics, int width, int height, Random rand)
        {
            int numShapes = rand.Next(5, 10);

            for (int i = 0; i < numShapes; i++)
            {
                int shapeType = rand.Next(0, 3);
                int x1 = rand.Next(width);
                int y1 = rand.Next(height);
                int x2 = rand.Next(width);
                int y2 = rand.Next(height);

                switch (shapeType)
                {
                    case 0:
                        graphics.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                        break;
                    case 1:
                        graphics.DrawRectangle(new Pen(Color.Black), x1, y1, x2, y2);
                        break;
                    case 2:
                        graphics.DrawEllipse(new Pen(Color.Black), x1, y1, x2, y2);
                        break;
                }
            }
        }
        WorldSkillsAirPlanDbEntities context = new WorldSkillsAirPlanDbEntities();
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.label3.Text = $"{datetime.Hour}:{datetime.Minute}:{datetime.Second}";
            
        }
        public int count = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == text)
                {
                    SHA256 sha = SHA256.Create();
                    byte[] bytes = sha.ComputeHash(UTF8Encoding.UTF8.GetBytes(txtPassword.Text));
                    var pass = Convert.ToBase64String(bytes);
                    var getUser = (from a in context.UsersTbls
                                   where a.UserNameForProgram == txtUserName.Text && a.UserPassword == pass
                                   select a).FirstOrDefault();
                    var findUser = (from a in context.UsersTbls
                                    where a.UserNameForProgram == txtUserName.Text
                                    select a).FirstOrDefault();
                    if (findUser != null)
                    {
                        if (findUser.UserStatus != 1)
                        {
                            if (txtUserName.Text != string.Empty || txtPassword.Text != string.Empty)
                            {
                                if (getUser != null)
                                {
                                    if (getUser.UserRull == 1)
                                    {
                                        ManagerTbl manager = new ManagerTbl(getUser);
                                        manager.ShowDialog();
                                    }
                                    else if (getUser.UserRull == 2)
                                    {
                                        CompanyFrm com = new CompanyFrm();
                                        com.ShowDialog();
                                    }
                                }
                                else
                                {
                                    if (findUser != null)
                                    {
                                        MessageBox.Show("رمز و یا نام کاربری نادرست است");
                                    }
                                    else
                                    {

                                        SignUp signup = new SignUp(txtUserName.Text);
                                        signup.ShowDialog();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("لطفا فیلد هارا پر کنید");
                            }
                        }
                        else
                        {
                            MessageBox.Show("متن تصویر اشتباه است");
                        }
                    
                    }
                    else
                    {
                        SignUp signup = new SignUp(txtUserName.Text);
                        signup.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطایی در اجرا برنامه ایجاد شده است");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SignUp SU = new SignUp(txtUserName.Text);
                SU.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            int width = 200;
            int height = 80;
            string captchaCode = GenerateRandomCaptcha(4);

            using (Bitmap bitmap = new Bitmap(width, height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Random rand = new Random();
                Color backgroundColor = Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
                graphics.Clear(backgroundColor);
                using (Font font = new Font("Arial", 24, FontStyle.Bold))
                {
                    Color textColor = Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
                    int x = rand.Next(5, width - 100);
                    int y = rand.Next(5, height - 40);
                    DrawRandomShapes(graphics, width, height, rand);

                    graphics.DrawString(captchaCode, font, new SolidBrush(textColor), x, y);
                    text = captchaCode;
                    bitmap.Save("captcha.png", ImageFormat.Png);
                    pictureBox1.Load("C:\\Users\\mani\\Desktop\\WindowsFormsApp3\\WindowsFormsApp3\\bin\\Debug\\captcha.png");
                }
            }

        }
    }
}
