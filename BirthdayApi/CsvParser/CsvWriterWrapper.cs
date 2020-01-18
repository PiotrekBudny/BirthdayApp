using BirthdayApi.Models;
using CsvHelper;
using System.Globalization;
using System.IO;

namespace BirthdayApi.CsvParser
{
    public static class CsvWriterWrapper
    {
          public static void WriteToCsvFile(string filePath, BirthdayPerson birthdayPerson)
          {
            var currentRecords = CsvReaderWrapper.ReadFromCsvFile(filePath);
            currentRecords.Add(birthdayPerson);

            using (var stream = new StreamWriter(filePath))
            using (var writer = new CsvWriter(stream, new CultureInfo("EN")))
            {
                writer.Configuration.HasHeaderRecord = true;                
                writer.WriteRecords(currentRecords);
            }
          }
    }
}
