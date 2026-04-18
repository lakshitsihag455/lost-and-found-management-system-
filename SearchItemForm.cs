using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LostAndFoundApp
{
    public class SearchItemForm : Form
    {
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnClear;
        private DataGridView dgvResults;

        public SearchItemForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Search Items";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            var lbl = new Label()
            {
                Text = "Search by Name/Category/Location:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            txtSearch = new TextBox()
            {
                Location = new Point(240, 18),
                Width = 300
            };

            btnSearch = new Button()
            {
                Text = "Search",
                Location = new Point(560, 16),
                Size = new Size(80, 26)
            };

            btnClear = new Button()
            {
                Text = "Clear",
                Location = new Point(560, 46),
                Size = new Size(80, 26)
            };

            dgvResults = new DataGridView()
            {
                Location = new Point(20, 80),
                Size = new Size(640, 360),
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            btnSearch.Click += BtnSearch_Click;
            btnClear.Click += BtnClear_Click;

            this.Controls.Add(lbl);
            this.Controls.Add(txtSearch);
            this.Controls.Add(btnSearch);
            this.Controls.Add(btnClear);
            this.Controls.Add(dgvResults);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a search term", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT * FROM Items WHERE ItemName LIKE @term OR Category LIKE @term OR Location LIKE @term";
            SqlParameter[] param =
            {
                new SqlParameter("@term", "%" + txtSearch.Text + "%")
            };

            try
            {
                DataTable dt = DatabaseHelper.ExecuteSelect(query, param);
                dgvResults.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            dgvResults.DataSource = null;
        }
    }
}