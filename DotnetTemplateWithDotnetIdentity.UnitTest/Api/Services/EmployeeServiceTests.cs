namespace DotnetTemplateWithDotnetIdentity.UnitTest.Api.Services
{
    public class EmployeeService_GET_Tests
    {
        [Fact]
        public async void Get_WhenCalled_ReturnsAllEmployees()
        {
            Mock<IRepository> repositoryMock = new Mock<IRepository>();
            Mock<ILogger<UserService>> loggerMock = new Mock<ILogger<UserService>>();
            Mock<IConfiguration> iConfigMock = new Mock<IConfiguration>();
            Mock<IMapper> iMapperMock = new Mock<IMapper>();
            Mock<IHttpContextAccessor> iHttpContextAccessorMock = new Mock<IHttpContextAccessor>();

            Mock<DbSet<User>> mockSet = new Mock<DbSet<User>>();
            mockSet.ConfigureMock(DBDataSets.GetEmployeeTableTestData());

            repositoryMock.Setup(r => r.GetList<User>()).Returns(() => mockSet.Object);

            iMapperMock.Setup(m => m.Map<List<UserReadDto>>(It.IsAny<List<User>>())).Returns(() =>
            {
                List<UserReadDto> returnVal = new List<UserReadDto>();
                returnVal.Add(new UserReadDto { UserId = 1, UserName = "CORP\\e999999", FirstName = "TestOne", LastName = "One", Email = "pp@g.com" });
                returnVal.Add(new UserReadDto { UserId = 2, UserName = "CORP\\e777777", FirstName = "TestTwo", LastName = "Two", Email = "gg@g.com" });
                returnVal.Add(new UserReadDto { UserId = 3, UserName = "CORP\\e666666", FirstName = "TestThree", LastName = "Three", Email = "kk@g.com" });

                return returnVal;
            });

            UserService empService = new UserService(repositoryMock.Object
                                                            , loggerMock.Object
                                                            , iConfigMock.Object
                                                            , iMapperMock.Object
                                                            , iHttpContextAccessorMock.Object);

            var result = await empService.GetAsync();

            Assert.NotNull(result);
            Assert.True(result.Count == 3);

        }
    }
}
