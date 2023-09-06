using DotnetTemplateWithDotnetIdentity.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetTemplateWithDotnetIdentity.Data.Util
{
    public static class InitalDataSets
    {
        public static List<User> GetUserInitialDataSet()
        {
            List<User> users = new List<User>();
            users.Add(new User
            {
                UserId = 1,
                UserName = "ps-admin@padmasekhar.com",
                FirstName = "Admin",
                LastName = "User",
                Email = "ps-admin@padmasekhar.com",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "seeder"
            });
            users.Add(new User
            {
                UserId = 2,
                UserName = "app-support@padmasekhar.com",
                FirstName = "Padmasekhar",
                LastName = "Pottepalem",
                Email = "app-support@padmasekhar.com",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "seeder"
            });
            users.Add(new User
            {
                UserId = 3,
                UserName = "padmasekhar.ps@outlook.com",
                FirstName = "Padmasekhar",
                LastName = "Pottepalem",
                Email = "padmasekhar.ps@outlook.com",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "seeder"
            });
            users.Add(new User
            {
                UserId = 4,
                UserName = "PADMASEKHAR-NEW\\padma",
                FirstName = "Padmasekhar",
                LastName = "Windows",
                Email = "PADMASEKHAR-NEW\\padma",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "seeder"
            });
            users.Add(new User
            {
                UserId = 5,
                UserName = "ayush-ss@padmasekhar.com",
                FirstName = "Ayush",
                LastName = "Jah",
                Email = "ayush-ss@padmasekhar.com",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "seeder"
            });
            users.Add(new User
            {
                UserId = 6,
                UserName = "sumuk@padmasekhar.com",
                FirstName = "Sumukh",
                LastName = "S",
                Email = "sumuk@padmasekhar.com",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "seeder"
            });

            return users;
        }

        public static List<AppRole> GetAppRoleInitalDataSet()
        {
            List<AppRole> roles = new List<AppRole>();

            roles.Add(new AppRole
            {
                AppRoleId = 1,
                IsActive = true,
                Role = "Admin",
                RoleDesc = "Application Administrator"
            });
            roles.Add(new AppRole
            {
                AppRoleId = 2,
                IsActive = true,
                Role = "Support",
                RoleDesc = "Application Support team"
            });
            roles.Add(new AppRole
            {
                AppRoleId = 3,
                IsActive = true,
                Role = "Default",
                RoleDesc = "Default User"
            });

            return roles;
        }

        public static List<AppMenuGroup> GetAppMenuGroupInitalDataSet()
        {
            List<AppMenuGroup> appMenuGroups = new List<AppMenuGroup>();

            appMenuGroups.Add(new AppMenuGroup
            {
                AppMenuGroupId = 1,
                IsActive = true,
                AppMenuGroupName = "Admin"
            });

            appMenuGroups.Add(new AppMenuGroup
            {
                AppMenuGroupId = 2,
                IsActive = true,
                AppMenuGroupName = "Support"
            });

            appMenuGroups.Add(new AppMenuGroup
            {
                AppMenuGroupId = 3,
                IsActive = true,
                AppMenuGroupName = "User"
            });

            return appMenuGroups;
        }

        public static List<AppMenuItem> GetAppMenuItemInitalDataSet()
        {
            List<AppMenuItem> appMenuItems = new List<AppMenuItem>();

            appMenuItems.Add(new AppMenuItem
            {
                AppMenuItemId = 1,
                IsActive = true,
                MenuItem = "User",
                MenuItemDesc = "User information will be displayed",
                AppMenuGroupId = 3
            });

            appMenuItems.Add(new AppMenuItem
            {
                AppMenuItemId = 2,
                IsActive = true,
                MenuItem = "AppConfig",
                MenuItemDesc = "App config information will be avaliable",
                AppMenuGroupId = 1
            });

            appMenuItems.Add(new AppMenuItem
            {
                AppMenuItemId = 3,
                IsActive = true,
                MenuItem = "ActivityLogs",
                MenuItemDesc = "Activity Logs",
                AppMenuGroupId = 2
            });

            appMenuItems.Add(new AppMenuItem
            {
                AppMenuItemId = 4,
                IsActive = true,
                MenuItem = "ErrorLogs",
                MenuItemDesc = "Error Logs",
                AppMenuGroupId = 2
            });

            return appMenuItems;
        }

        public static List<AppRoleMenuMapping> GetAppRoleMenuMappingInitalDataSet()
        {
            List<AppRoleMenuMapping> lst = new List<AppRoleMenuMapping>();

            #region Admin
            lst.Add(new AppRoleMenuMapping
            {
                AppRoleMenuMappingId = 1,
                AppRoleId = 1,
                AppMenuGroupId = 1,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });
            lst.Add(new AppRoleMenuMapping
            {
                AppRoleMenuMappingId = 2,
                AppRoleId = 1,
                AppMenuGroupId = 2,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });
            lst.Add(new AppRoleMenuMapping
            {
                AppRoleMenuMappingId = 3,
                AppRoleId = 1,
                AppMenuGroupId = 3,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });
            #endregion

            #region Support
            lst.Add(new AppRoleMenuMapping
            {
                AppRoleMenuMappingId = 4,
                AppRoleId = 2,
                AppMenuGroupId = 2,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });
            lst.Add(new AppRoleMenuMapping
            {
                AppRoleMenuMappingId = 5,
                AppRoleId = 2,
                AppMenuGroupId = 3,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });
            #endregion

            #region User
            lst.Add(new AppRoleMenuMapping
            {
                AppRoleMenuMappingId = 6,
                AppRoleId = 3,
                AppMenuGroupId = 3,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });
            #endregion

            return lst;
        }

        public static List<AppUserRoleMapping> GetAppUserRoleMappingInitalDataSet()
        {
            List<AppUserRoleMapping> lst = new List<AppUserRoleMapping>();

            lst.Add(new AppUserRoleMapping
            {
                AppUserRoleMappingId = 1,
                AppRoleId = 1,
                UserId = 1,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });

            lst.Add(new AppUserRoleMapping
            {
                AppUserRoleMappingId = 2,
                AppRoleId = 2,
                UserId = 2,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });

            lst.Add(new AppUserRoleMapping
            {
                AppUserRoleMappingId = 3,
                AppRoleId = 3,
                UserId = 3,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });

            lst.Add(new AppUserRoleMapping
            {
                AppUserRoleMappingId = 4,
                AppRoleId = 1,
                UserId = 4,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });

            lst.Add(new AppUserRoleMapping
            {
                AppUserRoleMappingId = 5,
                AppRoleId = 1,
                UserId = 5,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });

            lst.Add(new AppUserRoleMapping
            {
                AppUserRoleMappingId = 6,
                AppRoleId = 1,
                UserId = 6,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow,
            });

            return lst;
        }

        public static List<AppConfig> GetAppConfigInitalDataSet()
        {
            List<AppConfig> appConfigs = new List<AppConfig>();

            appConfigs.Add(new AppConfig
            {
                ConfigId = 1,
                ConfigKey = "AuthType",
                ConfigValue = "NTLM",
                IsActive = true,
                UpdatedBy = "seeder",
                UpdatedDate = DateTime.UtcNow
            });
            return appConfigs;
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(GetUserInitialDataSet());
            modelBuilder.Entity<AppRole>().HasData(GetAppRoleInitalDataSet());
            modelBuilder.Entity<AppMenuGroup>().HasData(GetAppMenuGroupInitalDataSet());
            modelBuilder.Entity<AppMenuItem>().HasData(GetAppMenuItemInitalDataSet());
            modelBuilder.Entity<AppRoleMenuMapping>().HasData(GetAppRoleMenuMappingInitalDataSet());
            modelBuilder.Entity<AppUserRoleMapping>().HasData(GetAppUserRoleMappingInitalDataSet());
            modelBuilder.Entity<AppConfig>().HasData(GetAppConfigInitalDataSet());
        }

        public static void SeedAppDatabase(this IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var appDbContext = scope.ServiceProvider.GetService<AppDbContext>();
            if (appDbContext != null)
            {
                appDbContext.Database.EnsureCreated();
                appDbContext.Database.Migrate();

                //if (!appDbContext.Users.Any())
                //{
                //    appDbContext.Users.AddRange(GetUserInitialDataSet());
                //}

                //if(!appDbContext.AppRoles.Any())
                //{
                //    appDbContext.AppRoles.AddRange(GetAppRoleInitalDataSet());
                //}

                //if (!appDbContext.AppMenuGroups.Any())
                //{
                //    appDbContext.AppMenuGroups.AddRange(GetAppMenuGroupInitalDataSet());
                //}

                //if (!appDbContext.AppMenuItems.Any())
                //{
                //    appDbContext.AppMenuItems.AddRange(GetAppMenuItemInitalDataSet());
                //}

                //if (!appDbContext.AppRoleMenuMappings.Any())
                //{
                //    appDbContext.AppRoleMenuMappings.AddRange(GetAppRoleMenuMappingInitalDataSet());
                //}

                //if (!appDbContext.AppUserRoleMappings.Any())
                //{
                //    appDbContext.AppUserRoleMappings.AddRange(GetAppUserRoleMappingInitalDataSet());
                //}

                //if (!appDbContext.AppConfigs.Any())
                //{
                //    appDbContext.AppConfigs.AddRange(GetAppConfigInitalDataSet());
                //}

                //appDbContext.SaveChanges();
            }
        }
    }
}
