using BirthdayApi.Models;
using CsvHelper;
using System.Globalization;
using System.IO;

namespace BirthdayApi.CsvParser
{
    public interface ICsvWriterWrapper
    {
        public void WriteToBirthdayCsvFile(BirthdayPerson birthdayPerson);
    }
       
    public class CsvWriterWrapper : ICsvWriterWrapper
    {
        ConfigurationWrapper configurationWrapper;
        CsvReaderWrapper csvReaderWrapper;
        
        public CsvWriterWrapper()
        {
            configurationWrapper = new ConfigurationWrapper();
            csvReaderWrapper = new CsvReaderWrapper();
        }
        
        public void WriteToBirthdayCsvFile(BirthdayPerson birthdayPerson)
          {
            var currentRecords = csvReaderWrapper.ReadFromBirthDayCsvFile();
            currentRecords.Add(birthdayPerson);

            using (var stream = new StreamWriter(configurationWrapper.GetBirthdayCsvFilePath()))
            using (var writer = new CsvWriter(stream, new CultureInfo("EN")))
            {
                writer.Configuration.HasHeaderRecord = true;                
                writer.WriteRecords(currentRecords);
            }
          }
    }
}
