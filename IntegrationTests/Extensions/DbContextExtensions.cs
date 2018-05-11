using System.Collections.Generic;
using DataAccess;
using IntegrationTests.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Extensions
{
    public static class DbContextExtensions
    {
        public static void EnsureSeeded(this ApplicationContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                context.PersistCollection(StaticData.Users);
                context.PersistCollection(StaticData.Manufacturers);
                context.PersistCollection(StaticData.Cars);
                context.PersistCollection(StaticData.UserCars);

                transaction.Commit();
            }
        }

#pragma warning disable EF1000 // Possible SQL injection vulnerability.
        public static void PersistCollection<T>(this ApplicationContext context, List<T> collection) where T : class
        {
            var setIdentityInsertOn = $"SET IDENTITY_INSERT {context.GetTableName<T>()} ON";
            var setIdentityInsertOff = $"SET IDENTITY_INSERT {context.GetTableName<T>()} OFF";
            context.Database.ExecuteSqlCommand(setIdentityInsertOn);
            context.Set<T>().AddRange(collection);
            context.SaveChanges();
            context.Database.ExecuteSqlCommand(setIdentityInsertOff);
        }
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
    }
}
