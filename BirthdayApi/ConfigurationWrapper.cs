﻿using Microsoft.Extensions.Configuration;

namespace BirthdayTracker.Web
{
    public interface IConfigurationWrapper
    {
        public string GetBirthdayCsvFilePath();
    }
     
    public class ConfigurationWrapper : IConfigurationWrapper
    {
        public string GetBirthdayCsvFilePath()
        {
            return InitializeConfigurationBuilder().Build()["Filepath:Birthdaycsv"];            
        }
        
        private  IConfigurationBuilder InitializeConfigurationBuilder()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json");
        }   
    }
}
