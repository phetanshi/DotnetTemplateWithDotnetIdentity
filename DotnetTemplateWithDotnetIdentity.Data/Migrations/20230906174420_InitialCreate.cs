using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotnetTemplateWithDotnetIdentity.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAppConfigs",
                columns: table => new
                {
                    ConfigId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConfigKey = table.Column<string>(type: "TEXT", nullable: false),
                    ConfigValue = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppConfigs", x => x.ConfigId);
                });

            migrationBuilder.CreateTable(
                name: "tblAppMenuGroups",
                columns: table => new
                {
                    AppMenuGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    AppMenuGroupName = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppMenuGroups", x => x.AppMenuGroupId);
                });

            migrationBuilder.CreateTable(
                name: "tblAppRoles",
                columns: table => new
                {
                    AppRoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    RoleDesc = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppRoles", x => x.AppRoleId);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ManagerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_tblUsers_tblUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "tblUsers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "tblAppMenuItems",
                columns: table => new
                {
                    AppMenuItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuItem = table.Column<string>(type: "TEXT", nullable: false),
                    MenuItemDesc = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    AppMenuGroupId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppMenuItems", x => x.AppMenuItemId);
                    table.ForeignKey(
                        name: "FK_tblAppMenuItems_tblAppMenuGroups_AppMenuGroupId",
                        column: x => x.AppMenuGroupId,
                        principalTable: "tblAppMenuGroups",
                        principalColumn: "AppMenuGroupId");
                });

            migrationBuilder.CreateTable(
                name: "tblAppRoleMenuMappings",
                columns: table => new
                {
                    AppRoleMenuMappingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppMenuGroupId = table.Column<int>(type: "INTEGER", nullable: true),
                    AppRoleId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppRoleMenuMappings", x => x.AppRoleMenuMappingId);
                    table.ForeignKey(
                        name: "FK_tblAppRoleMenuMappings_tblAppMenuGroups_AppMenuGroupId",
                        column: x => x.AppMenuGroupId,
                        principalTable: "tblAppMenuGroups",
                        principalColumn: "AppMenuGroupId");
                    table.ForeignKey(
                        name: "FK_tblAppRoleMenuMappings_tblAppRoles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalTable: "tblAppRoles",
                        principalColumn: "AppRoleId");
                });

            migrationBuilder.CreateTable(
                name: "tblAppUserRoleMappings",
                columns: table => new
                {
                    AppUserRoleMappingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AppRoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppUserRoleMappings", x => x.AppUserRoleMappingId);
                    table.ForeignKey(
                        name: "FK_tblAppUserRoleMappings_tblAppRoles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalTable: "tblAppRoles",
                        principalColumn: "AppRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAppUserRoleMappings_tblUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblAppConfigs",
                columns: new[] { "ConfigId", "ConfigKey", "ConfigValue", "IsActive", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, "AuthType", "NTLM", true, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6355) });

            migrationBuilder.InsertData(
                table: "tblAppMenuGroups",
                columns: new[] { "AppMenuGroupId", "AppMenuGroupName", "IsActive" },
                values: new object[,]
                {
                    { 1, "Admin", true },
                    { 2, "Support", true },
                    { 3, "User", true }
                });

            migrationBuilder.InsertData(
                table: "tblAppRoles",
                columns: new[] { "AppRoleId", "IsActive", "Role", "RoleDesc" },
                values: new object[,]
                {
                    { 1, true, "Admin", "Application Administrator" },
                    { 2, true, "Support", "Application Support team" },
                    { 3, true, "Default", "Default User" }
                });

            migrationBuilder.InsertData(
                table: "tblUsers",
                columns: new[] { "UserId", "ContactNumber", "CreatedBy", "CreatedDate", "Email", "FirstName", "IsActive", "LastName", "ManagerId", "MiddleName", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { 1, null, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6116), "ps-admin@padmasekhar.com", "Admin", true, "User", null, null, null, null, "ps-admin@padmasekhar.com" },
                    { 2, null, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6121), "app-support@padmasekhar.com", "Padmasekhar", true, "Pottepalem", null, null, null, null, "app-support@padmasekhar.com" },
                    { 3, null, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6122), "padmasekhar.ps@outlook.com", "Padmasekhar", true, "Pottepalem", null, null, null, null, "padmasekhar.ps@outlook.com" },
                    { 4, null, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6124), "PADMASEKHAR-NEW\\padma", "Padmasekhar", true, "Windows", null, null, null, null, "PADMASEKHAR-NEW\\padma" },
                    { 5, null, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6126), "ayush-ss@padmasekhar.com", "Ayush", true, "Jah", null, null, null, null, "ayush-ss@padmasekhar.com" },
                    { 6, null, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6129), "sumuk@padmasekhar.com", "Sumukh", true, "S", null, null, null, null, "sumuk@padmasekhar.com" }
                });

            migrationBuilder.InsertData(
                table: "tblAppMenuItems",
                columns: new[] { "AppMenuItemId", "AppMenuGroupId", "IsActive", "MenuItem", "MenuItemDesc" },
                values: new object[,]
                {
                    { 1, 3, true, "User", "User information will be displayed" },
                    { 2, 1, true, "AppConfig", "App config information will be avaliable" },
                    { 3, 2, true, "ActivityLogs", "Activity Logs" },
                    { 4, 2, true, "ErrorLogs", "Error Logs" }
                });

            migrationBuilder.InsertData(
                table: "tblAppRoleMenuMappings",
                columns: new[] { "AppRoleMenuMappingId", "AppMenuGroupId", "AppRoleId", "IsActive", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, 1, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6291) },
                    { 2, 2, 1, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6297) },
                    { 3, 3, 1, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6298) },
                    { 4, 2, 2, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6300) },
                    { 5, 3, 2, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6301) },
                    { 6, 3, 3, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6303) }
                });

            migrationBuilder.InsertData(
                table: "tblAppUserRoleMappings",
                columns: new[] { "AppUserRoleMappingId", "AppRoleId", "IsActive", "UpdatedBy", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6325), 1 },
                    { 2, 2, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6327), 2 },
                    { 3, 3, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6329), 3 },
                    { 4, 1, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6330), 4 },
                    { 5, 1, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6331), 5 },
                    { 6, 1, false, "seeder", new DateTime(2023, 9, 6, 17, 44, 20, 492, DateTimeKind.Utc).AddTicks(6332), 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAppMenuItems_AppMenuGroupId",
                table: "tblAppMenuItems",
                column: "AppMenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAppRoleMenuMappings_AppMenuGroupId",
                table: "tblAppRoleMenuMappings",
                column: "AppMenuGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAppRoleMenuMappings_AppRoleId",
                table: "tblAppRoleMenuMappings",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAppUserRoleMappings_AppRoleId",
                table: "tblAppUserRoleMappings",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAppUserRoleMappings_UserId",
                table: "tblAppUserRoleMappings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUsers_ManagerId",
                table: "tblUsers",
                column: "ManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAppConfigs");

            migrationBuilder.DropTable(
                name: "tblAppMenuItems");

            migrationBuilder.DropTable(
                name: "tblAppRoleMenuMappings");

            migrationBuilder.DropTable(
                name: "tblAppUserRoleMappings");

            migrationBuilder.DropTable(
                name: "tblAppMenuGroups");

            migrationBuilder.DropTable(
                name: "tblAppRoles");

            migrationBuilder.DropTable(
                name: "tblUsers");
        }
    }
}
