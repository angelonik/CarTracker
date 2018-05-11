using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace DataAccess
{
    public class ApplicationContext : DbContext
    {
        public static LoggerFactory ConsoleLoggerFactory = new LoggerFactory(new[]
        {
            new ConsoleLoggerProvider((category, level) => 
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information, true)
        });

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public string GetTableName<T>()
        {
            return $"dbo.{Model.FindEntityType(typeof(T)).Relational().TableName}";
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<UserCar> UserCars { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
    }
}
