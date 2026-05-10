using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{

    public partial class SignUp : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBoxPhoto.Image = new Bitmap(open.FileName);
                pictureBoxPhoto.Tag = open.FileName;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            string connString = "Server=.\\SQLEXPRESS; Database=FinalProjectJDRENTALS;; Trusted_Connection=True; TrustServerCertificate=True;";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"INSERT INTO Users (Email, PasswordHash, FullName, FirstName, MiddleName, LastName, Gender, Birthday, ImagePath, Role, Status) 
                        VALUES (@email, @pass, @full, @first, @mid, @last, @gender, @birth, @img, 'Staff', 'Active')";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                cmd.Parameters.AddWithValue("@full", $"{txtFirstName.Text} {txtLastName.Text}");
                cmd.Parameters.AddWithValue("@first", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@mid", txtMiddleName.Text.Trim());
                cmd.Parameters.AddWithValue("@last", txtLastName.Text.Trim());

                cmd.Parameters.AddWithValue("@gender", rbMale.Checked ? "Male" : "Female");

                cmd.Parameters.AddWithValue("@birth", dtpBirthday.Value);

                cmd.Parameters.AddWithValue("@img", pictureBoxPhoto.Tag ?? "");

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Created Successfully!");

                    LogIn login = new LogIn();
                    login.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LogIn LogIn = new LogIn();
            LogIn.Show();
            this.Close();
        }

        private void btnReturnToLogIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LogIn LogIn = new LogIn();
            LogIn.Show();
            this.Close();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            LogIn LogIn = new LogIn();
            LogIn.Show();
            this.Close();
        }
    }
}
