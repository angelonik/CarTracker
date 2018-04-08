using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

namespace UnitTests
{
    public class TestsBase : IDisposable
    {
        protected readonly DbContextOptions<ApplicationContext> _dbContextOptions;

        public TestsBase()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        protected void SetupMockDbContext<T>(T[] manufacturers)
            where T : class
        {
            using (var context = new ApplicationContext(_dbContextOptions))
            {
                context.Set<T>().AddRange(manufacturers);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            using (var context = new ApplicationContext(_dbContextOptions))
            {
                // Clean the database after each test run
                context.Database.EnsureDeleted();
            }
        }
    }
}
