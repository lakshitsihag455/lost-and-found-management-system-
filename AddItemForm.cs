using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LostAndFoundApp
{
    public class AddItemForm : Form
    {
        private TextBox txtItemID;
        private TextBox txtName;
        private TextBox txtDescription;
        private TextBox txtCategory;
        private TextBox txtLocation;
        private DateTimePicker dtDate;
        private ComboBox cmbStatus;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;

        private string initialStatus;

        public AddItemForm(string status)
        {
            initialStatus = status;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = initialStatus == "Lost" ? "Add Lost Item" : "Add Found Item";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            var lblTitle = new Label()
            {
                Text = this.Text,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true
            };
            lblTitle.Location = new Point(20, 20);

            var labels = new[] { "Item ID:", "Name:", "Description:", "Category:", "Location:", "Date:", "Status:" };

            int y = 70;
            txtItemID = new TextBox() { Location = new Point(120, y), Width = 300 };
            var lblID = new Label() { Text = labels[0], Location = new Point(20, y + 3), AutoSize = true };

            y += 40;
            txtName = new TextBox() { Location = new Point(120, y), Width = 300 };
            var lblName = new Label() { Text = labels[1], Location = new Point(20, y + 3), AutoSize = true };

            y += 40;
            txtDescription = new TextBox() { Location = new Point(120, y), Width = 300 };
            var lblDesc = new Label() { Text = labels[2], Location = new Point(20, y + 3), AutoSize = true };

            y += 40;
            txtCategory = new TextBox() { Location = new Point(120, y), Width = 300 };
            var lblCat = new Label() { Text = labels[3], Location = new Point(20, y + 3), AutoSize = true };

            y += 40;
            txtLocation = new TextBox() { Location = new Point(120, y), Width = 300 };
            var lblLoc = new Label() { Text = labels[4], Location = new Point(20, y + 3), AutoSize = true };

            y += 40;
            dtDate = new DateTimePicker() { Location = new Point(120, y), Width = 300, Format = DateTimePickerFormat.Short };
            var lblDate = new Label() { Text = labels[5], Location = new Point(20, y + 3), AutoSize = true };

            y += 40;
            cmbStatus = new ComboBox() { Location = new Point(120, y), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new string[] { "Lost", "Found" });
            cmbStatus.SelectedItem = initialStatus;
            var lblStatus = new Label() { Text = labels[6], Location = new Point(20, y + 3), AutoSize = true };

            y += 60;
            btnAdd = new Button() { Text = "Add", Location = new Point(20, y), Size = new Size(100, 30) };
            btnUpdate = new Button() { Text = "Update", Location = new Point(140, y), Size = new Size(100, 30) };
            btnDelete = new Button() { Text = "Delete", Location = new Point(260, y), Size = new Size(100, 30) };
            btnClear = new Button() { Text = "Clear", Location = new Point(380, y), Size = new Size(100, 30) };

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += BtnClear_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblID);
            this.Controls.Add(txtItemID);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblDesc);
            this.Controls.Add(txtDescription);
            this.Controls.Add(lblCat);
            this.Controls.Add(txtCategory);
            this.Controls.Add(lblLoc);
            this.Controls.Add(txtLocation);
            this.Controls.Add(lblDate);
            this.Controls.Add(dtDate);
            this.Controls.Add(lblStatus);
            this.Controls.Add(cmbStatus);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnClear);
        }

        private bool ValidateInputs(bool requireId = false)
        {
            if (requireId && string.IsNullOrWhiteSpace(txtItemID.Text))
            {
                MessageBox.Show("Item ID is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtCategory.Text) ||
                string.IsNullOrWhiteSpace(txtLocation.Text) ||
                cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all required fields", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            string query = "INSERT INTO Items (ItemName, Description, Category, Location, Date, Status) VALUES (@name, @desc, @cat, @loc, @date, @status)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@name", txtName.Text),
                new SqlParameter("@desc", txtDescription.Text),
                new SqlParameter("@cat", txtCategory.Text),
                new SqlParameter("@loc", txtLocation.Text),
                new SqlParameter("@date", dtDate.Value.Date),
                new SqlParameter("@status", cmbStatus.SelectedItem.ToString())
            };

            try
            {
                int rows = DatabaseHelper.ExecuteNonQuery(query, parameters);
                MessageBox.Show(rows > 0 ? "Item added successfully" : "No rows inserted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(true))
                return;

            string query = "UPDATE Items SET ItemName=@name, Description=@desc, Category=@cat, Location=@loc, Date=@date, Status=@status WHERE ItemID=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@name", txtName.Text),
                new SqlParameter("@desc", txtDescription.Text),
                new SqlParameter("@cat", txtCategory.Text),
                new SqlParameter("@loc", txtLocation.Text),
                new SqlParameter("@date", dtDate.Value.Date),
                new SqlParameter("@status", cmbStatus.SelectedItem.ToString()),
                new SqlParameter("@id", int.Parse(txtItemID.Text))
            };

            try
            {
                int rows = DatabaseHelper.ExecuteNonQuery(query, parameters);
                MessageBox.Show(rows > 0 ? "Item updated" : "No rows updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(true))
                return;

            string query = "DELETE FROM Items WHERE ItemID=@id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", int.Parse(txtItemID.Text))
            };

            try
            {
                int rows = DatabaseHelper.ExecuteNonQuery(query, parameters);
                MessageBox.Show(rows > 0 ? "Item deleted" : "No rows deleted", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtItemID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtCategory.Text = "";
            txtLocation.Text = "";
            dtDate.Value = DateTime.Now;
            cmbStatus.SelectedItem = initialStatus;
        }
    }
}