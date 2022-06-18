using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class UserForm : Form
    {
        private MovieLibraryContext dbContext;
        private UserContext userContext;
        private User selectedUser;
        private List<User> users;
        int selectedRowIndex = -1;
        public UserForm()
        {
            InitializeComponent();
            dbContext = new MovieLibraryContext();
            userContext = new UserContext(dbContext);

            LoadHeaderRow();
            LoadUsers();
        }
        private bool ValidateData()
        {
            if (txtName.Text != string.Empty)
            {
                return true;
            }

            return false;
        }

        private void ClearData()
        {
            txtName.Text = string.Empty;

            selectedUser = null;
            selectedRowIndex = -1;
        }
      

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedUser != null)
                {
                    MessageBox.Show("You can't create duplicated users!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (ValidateData())
                {
                    string name = txtName.Text;
                    int age = (int)numAge.Value;
                    string country = txtCountry.Text;
                    User user = new User(name, age, country);

                    userContext.Create(user);
                    MessageBox.Show("User was created successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CreateRowRefresh(user);
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Name is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Age is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Country is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadHeaderRow()
        {
            dataGridViewUser.Columns.Add("id", "ID");
            dataGridViewUser.Columns.Add("name", "Name");
            dataGridViewUser.Columns.Add("age", "Age");
            dataGridViewUser.Columns.Add("country", "Country");            
        }

        private void LoadUsers()
        {
            users = userContext.ReadAll().ToList();

            foreach (User item in users)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewUser.Rows[0].Clone();

                row.Cells[0].Value = item.ID;
                row.Cells[1].Value = item.Name;
                row.Cells[2].Value = item.Age;
                row.Cells[3].Value = item.Country;

                /*if (item.Movies != null)
                {
                    row.Cells[2].Value = string.Join(", ", item.Movies.Select(c => c.Name));
                }*/

                dataGridViewUser.Rows.Add(row);
            }
        }
        private void CreateRowRefresh(User item)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridViewUser.Rows[0].Clone();

            row.Cells[0].Value = item.ID;
            row.Cells[1].Value = item.Name;
            row.Cells[2].Value = item.Age;
            row.Cells[3].Value = item.Country;

            /*if (item.Movies != null)
            {
                row.Cells[2].Value = string.Join(", ", item.Movies);
            }*/

            dataGridViewUser.Rows.Add(row);
        }

        private void UpdateRowRefresh()
        {
            dataGridViewUser.Rows[selectedRowIndex].Cells[0].Value = selectedUser.ID;
            dataGridViewUser.Rows[selectedRowIndex].Cells[1].Value = selectedUser.Name;
            dataGridViewUser.Rows[selectedRowIndex].Cells[1].Value = selectedUser.Age;
            dataGridViewUser.Rows[selectedRowIndex].Cells[1].Value = selectedUser.Country;
        }

        private void DeleteRowRefresh()
        {
            dataGridViewUser.Rows.RemoveAt(selectedRowIndex);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateData() && selectedUser != null)
            {
                selectedUser.Name = txtName.Text;
                selectedUser.Age = (int)numAge.Value;
                selectedUser.Country = txtCountry.Text;
                userContext.Update(selectedUser);

                MessageBox.Show("User was updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UpdateRowRefresh();
                ClearData();
            }
            else
            {
                MessageBox.Show("Name, age and country are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedUser != null)
            {
                userContext.Delete(selectedUser.ID);

                DeleteRowRefresh();
                ClearData();
            }
            else
            {
                MessageBox.Show("You must select a user!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                int id = Convert.ToInt32(dataGridViewUser.Rows[e.RowIndex].Cells[0].Value);
                string name = dataGridViewUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                int age  = (int)dataGridViewUser.Rows[e.RowIndex].Cells[2].Value;
                string country = dataGridViewUser.Rows[e.RowIndex].Cells[3].Value.ToString();

                selectedUser = users.First(u => u.ID == id);

                txtName.Text = name;
                numAge.Value = age;
                txtCountry.Text = country;

                selectedRowIndex = e.RowIndex;
            }
        }
    }
}
