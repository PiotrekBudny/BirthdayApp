using ApiEndpointSimulator.CsvParser;
using ApiEndpointSimulator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiEndpointSimulator.Controllers
{
    [ApiController]
    [Route("api/birthdaycontroller")]
    public class BirthdayController : ControllerBase
    {
        [HttpGet("lastname/{lastname}")]
        public BirthdayPerson GetBirthDayPersonInfoByLastName(string lastName)
        {
            return CsvReaderWrapper.ReadFromCsvFile(ConfigurationWrapper.GetBirthdayCsvFilePath())
                                   .First(x => x.LastName == lastName);
        }
    }
}
