using DotnetTemplateWithDotnetIdentity.Logging.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotnetTemplateWithDotnetIdentity.Logging.Database
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public DbSet<ErrorType> ErrorTypes { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<AppLogLevel> AppLogLevels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conStr = Configuration.GetConnectionString("AppLogDbConnection");
            optionsBuilder.UseSqlite(conStr);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
