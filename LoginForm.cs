using System;
using System.Drawing;
using System.Windows.Forms;

namespace LostAndFoundApp
{
    public class LoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblTitle;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Lost and Found - Login";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label()
            {
                Text = "Login",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true
            };
            lblTitle.Location = new Point(30, 20);

            var lblUser = new Label()
            {
                Text = "Username:",
                Location = new Point(30, 70),
                AutoSize = true
            };

            var lblPass = new Label()
            {
                Text = "Password:",
                Location = new Point(30, 110),
                AutoSize = true
            };

            txtUsername = new TextBox()
            {
                Location = new Point(120, 68),
                Width = 200
            };

            txtPassword = new TextBox()
            {
                Location = new Point(120, 108),
                Width = 200,
                UseSystemPasswordChar = true
            };

            btnLogin = new Button()
            {
                Text = "Login",
                Location = new Point(120, 150),
                Width = 100
            };

            btnLogin.Click += BtnLogin_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblUser);
            this.Controls.Add(lblPass);
            this.Controls.Add(txtUsername);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
        }

        // Fixed here (removed ?)
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter username and password", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dashboard = new DashboardForm();
            dashboard.FormClosed += (s, args) => this.Close();
            dashboard.Show();
            this.Hide();
        }
    }
}