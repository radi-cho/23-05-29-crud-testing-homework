using DataLayer;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;

namespace TestingLayer
{
    [SetUpFixture]
    public static class SetupFixture
    {
        public static GameDBContext dbContext;

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            dbContext = new GameDBContext(builder.Options);
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            dbContext.Dispose();
        }
    }
}