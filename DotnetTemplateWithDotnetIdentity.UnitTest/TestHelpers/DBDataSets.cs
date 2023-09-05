namespace DotnetTemplateWithDotnetIdentity.UnitTest.TestHelpers
{
    public static class DBDataSets
    {
        public static List<User> GetEmployeeTableTestData()
        {
            List<User> employees = new List<User>();
            employees.Add(new User { UserId = 1, UserName = "CORP\\e999999", FirstName = "TestOne", LastName = "One", Email = "pp@g.com", CreatedBy = DateTime.Now.ToString() });
            employees.Add(new User { UserId = 2, UserName = "CORP\\e777777", FirstName = "TestTwo", LastName = "Two", Email = "gg@g.com", CreatedBy = DateTime.Now.ToString() });
            employees.Add(new User { UserId = 3, UserName = "CORP\\e666666", FirstName = "TestThree", LastName = "Three", Email = "kk@g.com", CreatedBy = DateTime.Now.ToString() });

            return employees;
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(GetEmployeeTableTestData());
        }
    }
}
