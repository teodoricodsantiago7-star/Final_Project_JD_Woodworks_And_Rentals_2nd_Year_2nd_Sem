using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices; 
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class LogIn : Form
    {
        
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public LogIn()
        {
            InitializeComponent();

            
            this.FormBorderStyle = FormBorderStyle.None;

            
            MainPanel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, MainPanel1.Width, MainPanel1.Height, 25, 25));
            Panel_IN1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Panel_IN1.Width, Panel_IN1.Height, 25, 25));
            txtB_Email.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, txtB_Email.Width, txtB_Email.Height, 25, 25));
            txtB_Password.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, txtB_Password.Width, txtB_Password.Height, 25, 25));
        }

        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        
        private void LogIn_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (txtB_Email.Text == "Email")
            {
                txtB_Email.Text = "";
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (txtB_Email.Text == "")
            {
                txtB_Email.Text = "Email";
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (txtB_Password.Text == "Password")
            {
                txtB_Password.Text = "";

                
                if (Chbox1.Checked == false)
                {
                    txtB_Password.PasswordChar = '*';
                }
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (txtB_Password.Text == "")
            {
                
                txtB_Password.PasswordChar = '\0';
                txtB_Password.Text = "Password";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (Chbox1.Checked == true)
            {
                txtB_Password.PasswordChar = '\0'; 
            }
            else
            {
                
                if (txtB_Password.Text != "Password")
                {
                    txtB_Password.PasswordChar = '*';
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogIn_Shown(object sender, EventArgs e)
        {
            MainPanel1.Focus();
        }
    }
}