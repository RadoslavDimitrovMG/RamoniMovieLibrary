﻿using System;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnGenre_Click(object sender, EventArgs e)
        {
            GenreForm genre = new GenreForm();
            genre.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMovie_Click(object sender, EventArgs e)
        {
            MovieForm movie = new MovieForm();
            movie.ShowDialog();
        }
    }
}
