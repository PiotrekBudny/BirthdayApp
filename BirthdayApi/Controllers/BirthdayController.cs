using BirthdayApi.CsvParser;
using BirthdayApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


namespace BirthdayApi.Controllers
{
    [ApiController]
    [Route("api/birthdaycontroller")]
    public class BirthdayController : ControllerBase
    {
        [HttpGet("lastname/{lastname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBirthDayPersonInfoByLastName(string lastName)
        {
            var response = new GetBirthDayPeopleDetailsResponse();
            
            try
            {
                response = ResponseBuilder.BuildGetBirthdayPersonDetailsResponse(CsvReaderWrapper.ReadFromCsvFile(ConfigurationWrapper.GetBirthdayCsvFilePath())
                                                                                                 .FindAll(x => x.LastName == lastName));          
            }
            catch(InvalidOperationException exception)
            {
                return NotFound(500);
            }

            if (response.BirthdayPeopleList == null || !response.BirthdayPeopleList.Any())
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("today")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPeopleWhoHaveBirthDayToday()
        {
            var response = new GetBirthDayPeopleDetailsResponse();

            try
            {
                response = ResponseBuilder.BuildGetBirthdayPersonDetailsResponse(CsvReaderWrapper.ReadFromCsvFile(ConfigurationWrapper.GetBirthdayCsvFilePath())
                          .FindAll(x => BirthdayValidator.ValidateIfTodayIsPersonBirthday(x.DayOfBirth) == true));
            }
            catch (Exception exception)
            {
                return NotFound();
            }

            if (response.BirthdayPeopleList == null || !response.BirthdayPeopleList.Any())
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public IActionResult AddBirthDayToTheList(AddBirthdayToTheListRequest addBirthdayToTheListRequest)
        {
            if (!BirthdayValidator.ValidateAddBirthDayToListRequest(addBirthdayToTheListRequest))
            {
                return BadRequest(new AddBirthdayToTheListResponse { BirthdayAdded = false,
                Message = "Bad request"});
            }

            var birthdayPerson = new BirthdayPerson()
            {
                LastName = addBirthdayToTheListRequest.LastName,
                FirstName = addBirthdayToTheListRequest.FirstName,
                DayOfBirth = addBirthdayToTheListRequest.DayOfBirth
            };
            
            try
            {
                CsvWriterWrapper.WriteToCsvFile(ConfigurationWrapper.GetBirthdayCsvFilePath(), birthdayPerson);
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return Created("",new AddBirthdayToTheListResponse {BirthdayAdded = true});
        }
    }
}
