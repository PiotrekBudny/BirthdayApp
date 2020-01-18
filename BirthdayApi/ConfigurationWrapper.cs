using Microsoft.Extensions.Configuration;

namespace BirthdayApi
{
    public static class ConfigurationWrapper
    {
            public static string GetBirthdayCsvFilePath()
            {
            return InitializeConfigurationBuilder().Build()["Filepath:Birthdaycsv"];            
            }

            private static IConfigurationBuilder InitializeConfigurationBuilder()
            {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json");
            }   
    }
}
