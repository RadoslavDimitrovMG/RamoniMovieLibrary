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
    public partial class GenreForm : Form
    {
        private MovieLibraryContext dbContext;
        private GenreContext genreContext;
        private Genre selectedGenre;
        private List<Genre> genres;
        int selectedRowIndex = -1;

        public GenreForm()
        {
            InitializeComponent();
            dbContext = new MovieLibraryContext();
            genreContext = new GenreContext(dbContext);

            LoadHeaderRow();
            LoadGenres();
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

            selectedGenre = null;
            selectedRowIndex = -1;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedGenre != null)
                {
                    MessageBox.Show("You can't create duplicated genre!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (ValidateData())
                {
                    string name = txtName.Text;
                    Genre genre = new Genre(name);

                    genreContext.Create(genre);
                    MessageBox.Show("Genre was created successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CreateRowRefresh(genre);
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Name is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateData() && selectedGenre != null)
            {
                selectedGenre.Name = txtName.Text;
                genreContext.Update(selectedGenre);

                MessageBox.Show("Genre was updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UpdateRowRefresh();
                ClearData();
            }
            else
            {
                MessageBox.Show("Name is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedGenre != null)
            {
                genreContext.Delete(selectedGenre.ID);

                DeleteRowRefresh();
                ClearData();
            }
            else
            {
                MessageBox.Show("You must select a genre!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewGenre_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                int id = Convert.ToInt32(dataGridViewGenre.Rows[e.RowIndex].Cells[0].Value);
                string name = dataGridViewGenre.Rows[e.RowIndex].Cells[1].Value.ToString();

                selectedGenre = genres.Find(g => g.ID == id);

                txtName.Text = name;

                selectedRowIndex = e.RowIndex;
            }
        }
        private void LoadHeaderRow()
        {
            dataGridViewGenre.Columns.Add("id", "ID");
            dataGridViewGenre.Columns.Add("name", "Name");
            dataGridViewGenre.Columns.Add("movies", "Movies");
        }

        private void LoadGenres()
        {
            genres = genreContext.ReadAll().ToList();

            foreach (Genre item in genres)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewGenre.Rows[0].Clone();

                row.Cells[0].Value = item.ID;
                row.Cells[1].Value = item.Name;

                if (item.Movies != null)
                {
                    row.Cells[2].Value = string.Join(", ", item.Movies.Select(c => c.Name));
                }

                dataGridViewGenre.Rows.Add(row);
            }
        }
        private void CreateRowRefresh(Genre item)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridViewGenre.Rows[0].Clone();

            row.Cells[0].Value = item.ID;
            row.Cells[1].Value = item.Name;

            if (item.Movies != null)
            {
                row.Cells[2].Value = string.Join(", ", item.Movies);
            }

            dataGridViewGenre.Rows.Add(row);
            dataGridViewGenre.Refresh();

            genres.Add(item);
        }

        private void UpdateRowRefresh()
        {
            dataGridViewGenre.Rows[selectedRowIndex].Cells[0].Value = selectedGenre.ID;
            dataGridViewGenre.Rows[selectedRowIndex].Cells[1].Value = selectedGenre.Name;
        }

        private void DeleteRowRefresh()
        {
            dataGridViewGenre.Rows.RemoveAt(selectedRowIndex);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}
