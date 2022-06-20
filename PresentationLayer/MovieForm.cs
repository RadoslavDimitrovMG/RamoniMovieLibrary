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
    public partial class MovieForm : Form
    {
        private MovieLibraryContext dbContext;
        private GenreContext genreContext;
        private Genre selectedGenre;
        //private List<Genre> genres;

        private MovieContext movieContext;
        private Movie selectedMovie;
        private List<Movie> movies;

        int selectedRowIndex = -1;

        public MovieForm()
        {
            InitializeComponent();
            dbContext = new MovieLibraryContext();
            genreContext = new GenreContext(dbContext);
            movieContext = new MovieContext(dbContext);

            LoadHeaderRow();
            LoadMovies();
            BindComboBox();
        }

        private bool ValidateData()
        {
            if (txtName.Text != string.Empty && txtDescr.Text != string.Empty)
            {
                return true;
            }

            return false;
        }

        private void ClearData()
        {
            txtName.Text = string.Empty;
            txtDescr.Text = string.Empty;
            numYear.Value = numYear.Minimum;
            numLength.Value = numLength.Minimum;

            selectedMovie = null;
            selectedGenre = null;
            selectedRowIndex = -1;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            

        }
        private void BindComboBox()
        {
            comboBox1.DataSource = genreContext.ReadAll();

            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                selectedGenre = comboBox1.SelectedItem as Genre;
            }
        }

        
        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }


        private void dataGridViewMovie_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                int id = Convert.ToInt32(dataGridViewMovie.Rows[e.RowIndex].Cells[0].Value);
                string name = dataGridViewMovie.Rows[e.RowIndex].Cells[1].Value.ToString();
                string descr = dataGridViewMovie.Rows[e.RowIndex].Cells[4].Value.ToString();
                int year = Convert.ToInt32(dataGridViewMovie.Rows[e.RowIndex].Cells[2].Value);
                int length = Convert.ToInt32(dataGridViewMovie.Rows[e.RowIndex].Cells[3].Value);

                selectedMovie = movies.Find(m => m.ID == id);

                txtName.Text = name;
                txtDescr.Text = descr;
                numLength.Value = length;
                numYear.Value = year;

                selectedRowIndex = e.RowIndex;
            }
        }
        private void LoadHeaderRow()
        {
            dataGridViewMovie.Columns.Add("id", "ID");
            dataGridViewMovie.Columns.Add("name", "Name");
            dataGridViewMovie.Columns.Add("releaseyear", "Release Year");
            dataGridViewMovie.Columns.Add("length", "Length");
            dataGridViewMovie.Columns.Add("description", "Description");
            dataGridViewMovie.Columns.Add("genres", "Genres");
        }

        private void LoadMovies()
        {
            movies = movieContext.ReadAll().ToList();

            foreach (Movie item in movies)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridViewMovie.Rows[0].Clone();

                row.Cells[0].Value = item.ID;
                row.Cells[1].Value = item.Name;
                row.Cells[2].Value = item.ReleaseYear;
                row.Cells[3].Value = item.Length;
                row.Cells[4].Value = item.Description;

                if (item.Genres != null)
                {
                    row.Cells[5].Value = string.Join(", ", item.Genres.Select(g => g.Name));
                }

                dataGridViewMovie.Rows.Add(row);
            }
        }

        private void CreateRowRefresh(Movie item)
        {
            DataGridViewRow row = (DataGridViewRow)dataGridViewMovie.Rows[0].Clone();

            row.Cells[0].Value = item.ID;
            row.Cells[1].Value = item.Name;
            row.Cells[2].Value = item.ReleaseYear;
            row.Cells[3].Value = item.Length;
            row.Cells[4].Value = item.Description;

            if (item.Genres != null)
            {
                row.Cells[5].Value = string.Join(", ", item.Genres.Select(g => g.Name));
            }

            dataGridViewMovie.Rows.Add(row);
        }

        private void UpdateRowRefresh()
        {
            dataGridViewMovie.Rows[selectedRowIndex].Cells[0].Value = selectedMovie.ID;
            dataGridViewMovie.Rows[selectedRowIndex].Cells[1].Value = selectedMovie.Name;
            dataGridViewMovie.Rows[selectedRowIndex].Cells[2].Value = selectedMovie.ReleaseYear;
            dataGridViewMovie.Rows[selectedRowIndex].Cells[3].Value = selectedMovie.Length;
            dataGridViewMovie.Rows[selectedRowIndex].Cells[4].Value = selectedMovie.Description;
            dataGridViewMovie.Rows[selectedRowIndex].Cells[5].Value = string.Join(", ", selectedMovie.Genres.Select(g => g.Name));
        }

        private void DeleteRowRefresh()
        {
            dataGridViewMovie.Rows.RemoveAt(selectedRowIndex);
        }

        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (selectedMovie != null)
                {
                    MessageBox.Show("You can't create duplicated movie!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (ValidateData())
                {
                    string name = txtName.Text;
                    string descr = txtDescr.Text;
                    int length = Convert.ToInt32(numLength.Value);
                    int year = Convert.ToInt32(numYear.Value);
                    List<Genre> genres = new List<Genre> { selectedGenre };
                    Movie movie = new Movie(name, year, descr, length, genres);
                    movieContext.Create(movie);

                    MessageBox.Show("Movie was created successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CreateRowRefresh(movie);
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Name and descreption are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateData() && selectedMovie != null)
            {
                selectedMovie.Name = txtName.Text;
                selectedMovie.Description = txtDescr.Text;
                selectedMovie.Length = Convert.ToInt32(numLength.Value);
                selectedMovie.ReleaseYear = Convert.ToInt32(numYear.Value);
                List<Genre> genres = new List<Genre> { selectedGenre };
                selectedMovie.Genres = genres;
                movieContext.Update(selectedMovie);

                MessageBox.Show("Movie was updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            if (selectedMovie != null)
            {
                movieContext.Delete(selectedMovie.ID);

                DeleteRowRefresh();
                ClearData();
            }
            else
            {
                MessageBox.Show("You must select a movie!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MovieForm_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}
