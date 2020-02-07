using BirthdayApi.Models;
using BirthdayApi.Providers;
using BirthdayApi.Validators;
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
        IGetBirthdayPeopleDetailsResponseProvider getBirthdayPeopleDetailsResponseProvider;
        IAddBirthdayToListResponseProvider addBirthdayToListResponseProvider;
        IAddBirthdayToTheListHelper addBirthdayHelper;

        public BirthdayController(IGetBirthdayPeopleDetailsResponseProvider getBirthdayPeopleDetailsResponseProvider,
                                  IAddBirthdayToListResponseProvider addBirthdayToListResponseProvider,
                                  IAddBirthdayToTheListHelper addBirthdayHelper)
        {
            this.addBirthdayToListResponseProvider = addBirthdayToListResponseProvider;
            this.getBirthdayPeopleDetailsResponseProvider = getBirthdayPeopleDetailsResponseProvider;
            this.addBirthdayHelper = addBirthdayHelper;
        }
                
        [HttpGet("lastname/{lastname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBirthDayPersonInfoByLastName(string lastName)
        {
            var response = new GetBirthDayPeopleDetailsResponse();
            try
            {
                response = getBirthdayPeopleDetailsResponseProvider.GetBirthdaysFilteringByLastName(lastName);
            }
            catch(InvalidOperationException exception)
            {
                return StatusCode(500);
            }
            catch(Exception exception)
            {
                return NotFound(response);
            }

            if (response.BirthdayPeopleList?.Any() ?? false)
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
                response = getBirthdayPeopleDetailsResponseProvider.GetBirthdaysForToday();
            }
            catch (Exception exception)
            {
                return NotFound();
            }

            if (response.BirthdayPeopleList?.Any() ?? false)
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
            if (!ValidateIfAddBirthDayRequestIsValid(addBirthdayToTheListRequest))
            {
                return BadRequest(addBirthdayToListResponseProvider.GetBadRequstResponse());
            }
            
            try
            {
                addBirthdayHelper.AddNewBirthdayPersonToCsvfile(addBirthdayToTheListRequest);
            }
            
            catch(Exception exception)
            {
                return StatusCode(500);
            }
            
            return Created(string.Empty,addBirthdayToListResponseProvider.GetCreatedResponse());
        }

        private bool ValidateIfAddBirthDayRequestIsValid(AddBirthdayToTheListRequest addBirthdayToTheListRequest)
        {
            var addBirthdayRequestValidator = new AddBirthdayToListRequestValidator();

            return addBirthdayRequestValidator.Validate(addBirthdayToTheListRequest).IsValid;
        }
    }
}
