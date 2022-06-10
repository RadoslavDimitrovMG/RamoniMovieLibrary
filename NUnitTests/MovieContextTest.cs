using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests
{
    class MovieContextTest
    {
        private MovieLibraryContext dbContext;
        private MovieContext movieContext;
        DbContextOptionsBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            dbContext = new MovieLibraryContext(builder.Options);
            movieContext = new MovieContext(dbContext);
        }

        [Test]
        public void TestCreateMovie()
        {
            List<Genre> genres = new List<Genre>() { new Genre("Sci-fi"), new Genre("Action"), new Genre("Thriller") };
            
            int moviesBefore = movieContext.ReadAll().Count();

            movieContext.Create(new Movie("Star Wars", 1974, "The most epic sci-fi saga of all time", 168, genres));

            int moviesAfter = movieContext.ReadAll().Count();

            Assert.IsTrue(moviesBefore != moviesAfter);
        }

        [Test]
        public void TestReadMovie()
        {
            List<Genre> genres = new List<Genre>() { new Genre("Sci-fi"), new Genre("Action"), new Genre("Thriller") };
            movieContext.Create(new Movie("Star Wars", 1974, "The most epic sci-fi saga of all time", 168, genres));

            Movie movie = movieContext.Read(1);

            Assert.That(movie != null, "There is no record with id 1!");
        }

        [Test]
        public void TestUpdateMovie()
        {
            List<Genre> genres = new List<Genre>() { new Genre("Sci-fi"), new Genre("Action"), new Genre("Thriller") };
            movieContext.Create(new Movie("Star Wars", 1974, "The most epic sci-fi saga of all time", 168, genres));

            Movie movie = movieContext.Read(1);

            movie.Name = "Star Wars2";
            movie.Genres = new List<Genre>() { new Genre("Comedy"), new Genre("Action"), new Genre("Thriller") };


            movieContext.Update(movie);

            Movie movieNew = movieContext.Read(1);

            Assert.IsTrue(movieNew.Name == "Star Wars2" && movieNew.Genres.First().Name == "Comedy", "Movie Update() does not change!");
        }

        [Test]
        public void TestDeleteMovie()
        {
            List<Genre> genres = new List<Genre>() { new Genre("Sci-fi"), new Genre("Action"), new Genre("Thriller") };
            movieContext.Create(new Movie("Star Wars", 1974, "The most epic sci-fi saga of all time", 168, genres));

            int moviesBeforeDelete = movieContext.ReadAll().Count();

            movieContext.Delete(1);

            int moviesAfterDelete = movieContext.ReadAll().Count();

            Assert.AreNotEqual(moviesAfterDelete, moviesBeforeDelete);
        }
    }
}
