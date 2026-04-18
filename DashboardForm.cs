using System;
using System.Drawing;
using System.Windows.Forms;

namespace LostAndFoundApp
{
    public class DashboardForm : Form
    {
        private Button btnAddLost;
        private Button btnAddFound;
        private Button btnSearch;
        private Button btnViewAll;
        private Button btnLogout;
        private Label lblTitle;

        public DashboardForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Dashboard - Lost and Found";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label() { Text = "Dashboard", Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true };
            lblTitle.Location = new Point(20, 20);

            btnAddLost = new Button() { Text = "Add Lost Item", Location = new Point(20, 80), Size = new Size(160, 40) };
            btnAddFound = new Button() { Text = "Add Found Item", Location = new Point(200, 80), Size = new Size(160, 40) };
            btnSearch = new Button() { Text = "Search Item", Location = new Point(20, 140), Size = new Size(160, 40) };
            btnViewAll = new Button() { Text = "View All Items", Location = new Point(200, 140), Size = new Size(160, 40) };
            btnLogout = new Button() { Text = "Logout", Location = new Point(20, 200), Size = new Size(100, 30) };

            btnAddLost.Click += (s, e) => OpenAddItemForm("Lost");
            btnAddFound.Click += (s, e) => OpenAddItemForm("Found");
            btnSearch.Click += (s, e) => { new SearchItemForm().ShowDialog(); };
            btnViewAll.Click += (s, e) => { new ViewAllItemsForm().ShowDialog(); };
            btnLogout.Click += (s, e) => { this.Close(); var login = new LoginForm(); login.Show(); };

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnAddLost);
            this.Controls.Add(btnAddFound);
            this.Controls.Add(btnSearch);
            this.Controls.Add(btnViewAll);
            this.Controls.Add(btnLogout);
        }

        private void OpenAddItemForm(string status)
        {
            var addForm = new AddItemForm(status);
            addForm.ShowDialog();
        }
    }
}