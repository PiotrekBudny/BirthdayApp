using BirthdayApi.Models;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BirthdayApi.CsvParser
{  
    public interface ICsvReaderWrapper
    {
        List<BirthdayPerson> ReadFromBirthDayCsvFile();
    }
        
    public class CsvReaderWrapper : ICsvReaderWrapper
    {
        ConfigurationWrapper configurationWrapper;
        
        public CsvReaderWrapper()
        {
            configurationWrapper = new ConfigurationWrapper();
        }
        
        public List<BirthdayPerson> ReadFromBirthDayCsvFile()
        {
            using (var reader = new StreamReader(configurationWrapper.GetBirthdayCsvFilePath()))
            using (var csvReader = new CsvReader(reader, new CultureInfo("EN")))
            {
                csvReader.Configuration.HasHeaderRecord = true;

                return csvReader.GetRecords<BirthdayPerson>().ToList();
            }
        }
    }
}
