using NUnit.Framework;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using BusinessLayer;

namespace NUnitTests
{
    public class GenreContextTest
    {
        private MovieLibraryContext dbContext;
        private GenreContext genreContext;
        DbContextOptionsBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            dbContext = new MovieLibraryContext(builder.Options);
            genreContext = new GenreContext(dbContext);
        }

        [Test]
        public void TestCreateGenre()
        {
            int genresBefore = genreContext.ReadAll().Count();

            genreContext.Create(new Genre("comedy"));

            int genresAfter = genreContext.ReadAll().Count();

            Assert.IsTrue(genresBefore != genresAfter);
        }

        [Test]
        public void TestReadGenre()
        {
            genreContext.Create(new Genre("comedy"));

            Genre genre = genreContext.Read(1);

            Assert.That(genre != null, "There is no record with id 1!");
        }

        [Test]
        public void TestUpdateGenre()
        {
            genreContext.Create(new Genre("comedy"));

            Genre genre = genreContext.Read(1);

            genre.Name = "drama";

            genreContext.Update(genre);

            Genre genreNew = genreContext.Read(1);

            Assert.IsTrue(genreNew.Name == "drama", "Genre Update() does not change name!");
        }

        [Test]
        public void TestDeleteGenre()
        {
            genreContext.Create(new Genre("Delete genre"));

            int genresBeforeDelete = genreContext.ReadAll().Count();

            genreContext.Delete(1);

            int genresAfterDelete = genreContext.ReadAll().Count();

            Assert.AreNotEqual(genresAfterDelete, genresBeforeDelete);
        }
    }
}