using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class SignUp : Form
    {
        public string pas;
        public SignUp(string password)
        {
            InitializeComponent();
            timer1.Start();
            pas = password;
        }
        WorldSkillsAirPlanDbEntities context = new WorldSkillsAirPlanDbEntities();

        private void SignUp_Load(object sender, EventArgs e)
        {
            try
            {
                txtUserName.Text = pas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.label7.Text = $"{datetime.Hour}:{datetime.Minute}:{datetime.Second}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text == string.Empty)
                {
                    txtUserName.BackColor = Color.Red;
                }
                if (txtPassword.Text == string.Empty)
                {
                    txtPassword.BackColor = Color.Red;
                }
                if (txtName.Text == string.Empty)
                {
                    txtName.BackColor = Color.Red;
                }
                if (txtLastName.Text == string.Empty)
                {
                    txtLastName.BackColor = Color.Red;
                }
                if (txtConfirmPassword.Text == string.Empty)
                {
                    txtConfirmPassword.BackColor = Color.Red;
                }
                if (DateTime.Now.Subtract(dateTimePicker1.Value).TotalDays < 6570)
                {
                    MessageBox.Show("سن شما مناسب نیست");

                }
                else
                {
                    var checkUser = (from a in context.UsersTbls
                                     where a.UserNameForProgram == txtUserName.Text
                                     select a).FirstOrDefault();
                    if (checkUser == null)
                    {
                        if (txtPassword.Text == txtConfirmPassword.Text && txtUserName.Text.Length > 4 && txtUserName.Text != string.Empty || txtPassword.Text != string.Empty)
                        {
                            SHA256 sha = SHA256.Create();
                            byte[] password = sha.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Text));
                            string pass = Convert.ToBase64String(password);
                            UsersTbl user = new UsersTbl();
                            user.UserPassword = pass;
                            user.UserName = txtName.Text;
                            user.UserLastName = txtLastName.Text;
                            user.UserNameForProgram = txtUserName.Text;
                            user.UserStatus = 0;
                            user.UserBirhtday = dateTimePicker1.Value;
                            user.UserRull = 2;
                            user.UserPhoto = photoName;
                            context.UsersTbls.Add(user);
                            context.SaveChanges();
                            MessageBox.Show("کاربر با موفقیت ثبت شد");
                            txtUserName.Text = null;
                            txtPassword.Text = null;
                            txtName.Text = null;
                            txtLastName.Text = null;
                            txtConfirmPassword.Text = null;
                            dateTimePicker1.Value = DateTime.Now;

                        }
                        else
                        {
                            MessageBox.Show("اطلاعات وارد شده غلط است");
                        }
                    }
                    else
                    {
                        MessageBox.Show("این نام کاربری توسط ");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ثبت کاربر با مشکل رو به رو شد");
            }
        }
        static void Main()
        {
            string password = GeneratePassword();
            Console.WriteLine("رمز عبور تولید شده: " + password);
        }

        static string GeneratePassword()
        {
            string pattern = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()-_=+[]{}|;:,.<>?";

            Random random = new Random();
            int passwordLength = 8;
            char[] passwordChars = new char[passwordLength];

            bool hasRepeatingPattern = true;

            while (hasRepeatingPattern)
            {
                hasRepeatingPattern = false;

                for (int i = 0; i < passwordLength; i++)
                {
                    passwordChars[i] = pattern[random.Next(pattern.Length)];

                    if (i > 0 && i < passwordLength - 1 && passwordChars[i] == passwordChars[i - 1])
                    {
                        hasRepeatingPattern = true;
                        break;
                    }
                }
            }

            string password = new string(passwordChars);

            while (password == GetClipboardText())
            {
                for (int i = 0; i < passwordLength; i++)
                {
                    passwordChars[i] = pattern[random.Next(pattern.Length)];
                }

                password = new string(passwordChars);
            }

            return password;
        }

        static string GetClipboardText()
        {
            return "SomeClipboardText";
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
        }
    }

}
