using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DataLayer;
using BusinessLayer;

namespace TestingLayer
{
    [TestFixture]
    class UserTests
    {
        private UserContext context = new UserContext(SetupFixture.dbContext);
        private User user;
        private Game game1;
        private Game game2;

        [SetUp]
        public void CreateUser()
        {
            user = new User("Bai", "Tosho", 22, "tato", "qwert", "tosho@bkp.bg");
            game1 = new Game("Angry Birds");
            game2 = new Game("Subway Surfers");

            user.Games.Add(game1);
            user.Games.Add(game2);

            context.Create(user);
        }

        [TearDown]
        public void DropUser()
        {
            foreach(User user in SetupFixture.dbContext.Users)
            {
                SetupFixture.dbContext.Users.Remove(user);
            }
            SetupFixture.dbContext.SaveChanges();
        }

        [Test]
        public void Create()
        {
            int before = SetupFixture.dbContext.Users.Count();
            context.Create(new User("Nqkoy", "Chovek", 17, "tqwn", "1234", "aloou@gmail.com"));

            int after = SetupFixture.dbContext.Users.Count();
            Assert.IsTrue(before + 1 == after, "An issue was found in Create()");
        }

        [Test]
        public void Read()
        {
            Assert.AreEqual(user, context.Read(user.Id), "An issue was found in Read()");
        }

        [Test]
        public void ReadWithNavigationalProperties()
        {
            User incomingUser = context.Read(user.Id, true);
            Assert.That(incomingUser.Games.Contains(game1), "game1 is not in the list");
            Assert.That(incomingUser.Games.Contains(game2), "game2 is not in the list");
        }

        [Test]
        public void ReadAll()
        {
            List<User> users = (List<User>)context.ReadAll();
            Assert.That(users.Count > 0, "An issue was found in ReadAll()");
        }

        [Test]
        public void Update()
        {
            User updatedUser = context.Read(user.Id);
            updatedUser.FirstName = "O" + user.FirstName;
            updatedUser.Email = "nov@gmail.com";

            context.Update(updatedUser);
            Assert.AreEqual(updatedUser, user, "An issue was found in Update()");
        }

        [Test]
        public void Delete()
        {
            int before = SetupFixture.dbContext.Users.Count();

            context.Delete(user.Id);
            int after = SetupFixture.dbContext.Users.Count();

            Assert.IsTrue(before - 1 == after, "An issue was found in Delete()");
        }
    }
}
