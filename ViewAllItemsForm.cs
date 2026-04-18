using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LostAndFoundApp
{
    public class ViewAllItemsForm : Form
    {
        private DataGridView dgvItems;
        private Button btnRefresh;

        public ViewAllItemsForm()
        {
            InitializeComponent();
            LoadItems();
        }

        private void InitializeComponent()
        {
            this.Text = "All Items";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            dgvItems = new DataGridView() { Location = new Point(20, 60), Size = new Size(740, 380), ReadOnly = true, AllowUserToAddRows = false };
            var lbl = new Label() { Text = "Items", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            btnRefresh = new Button() { Text = "Refresh", Location = new Point(680, 20), Size = new Size(80, 30) };
            btnRefresh.Click += (s, e) => LoadItems();

            this.Controls.Add(lbl);
            this.Controls.Add(dgvItems);
            this.Controls.Add(btnRefresh);
        }

        private void LoadItems()
        {
            try
            {
                var dt = DatabaseHelper.ExecuteSelect("SELECT * FROM Items");
                dgvItems.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
