namespace DotnetTemplateWithDotnetIdentity.UnitTest.TestHelpers
{
    public class ConstructorHelper
    {
        public static IConfiguration GetTestConfigFile()
        {
            var inMemorySettings = new Dictionary<string, string>();
            inMemorySettings.Add("Api:Login", "user/login");
            inMemorySettings.Add("Api:Post", "user/post");
            IConfiguration configuration = new ConfigurationBuilder()
                                            .AddInMemoryCollection(inMemorySettings)
                                            .Build();

            return configuration;
        }
    }
}
