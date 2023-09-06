using DotnetTemplateWithDotnetIdentity.Data.Models;
using DotnetTemplateWithDotnetIdentity.Data.Util;
using Microsoft.EntityFrameworkCore;

namespace DotnetTemplateWithDotnetIdentity.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppMenuGroup> AppMenuGroups { get; set; }
        public DbSet<AppMenuItem> AppMenuItems { get; set; }
        public DbSet<AppRoleMenuMapping> AppRoleMenuMappings { get; set; }
        public DbSet<AppUserRoleMapping> AppUserRoleMappings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}