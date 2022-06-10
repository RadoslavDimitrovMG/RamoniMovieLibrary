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
    class UserContextText
    {
        private MovieLibraryContext dbContext;
        private UserContext userContext;
        DbContextOptionsBuilder builder;

        [SetUp]
        public void Setup()
        {
            builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            dbContext = new MovieLibraryContext(builder.Options);
            userContext = new UserContext(dbContext);
        }

        [Test]
        public void TestCreateUser()
        {
            int usersBefore = userContext.ReadAll().Count();

            userContext.Create(new User("Rady", 18, "Bulgaria"));

            int usersAfter = userContext.ReadAll().Count();

            Assert.IsTrue(usersBefore != usersAfter);
        }

        [Test]
        public void TestReadUser()
        {
            userContext.Create(new User("Rady", 18, "Bulgaria"));

            User user = userContext.Read(1);

            Assert.That(user != null, "There is no record with id 1!");
        }

        [Test]
        public void TestUpdateUser()
        {
            userContext.Create(new User("Rady", 18, "Bulgaria"));

            User user = userContext.Read(1);

            user.Name = "Monika";
            user.Age = 19;

            userContext.Update(user);

            User userNew = userContext.Read(1);

            Assert.IsTrue(userNew.Name == "Monika" && userNew.Age == 19, "User Update() does not change!");
        }

        [Test]
        public void TestDeleteUser()
        {
            userContext.Create(new User("Rady", 18, "Bulgaria"));

            int usersBeforeDelete = userContext.ReadAll().Count();

            userContext.Delete(1);

            int usersAfterDelete = userContext.ReadAll().Count();

            Assert.AreNotEqual(usersBeforeDelete, usersAfterDelete);
        }
    }
}
