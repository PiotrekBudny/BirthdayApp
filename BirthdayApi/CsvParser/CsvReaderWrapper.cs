using BirthdayApi.Models;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BirthdayApi.CsvParser
{
    public static class CsvReaderWrapper
    {
        public static List<BirthdayPerson> ReadFromCsvFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csvReader = new CsvReader(reader, new CultureInfo("EN")))
            {
                csvReader.Configuration.HasHeaderRecord = true;

                return csvReader.GetRecords<BirthdayPerson>().ToList();
            }
        }
    }
}
