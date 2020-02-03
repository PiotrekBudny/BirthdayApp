using BirthdayApi.Models;
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
        private AddBirthdayToListRequestValidator _addBirthdayRequestValidator;
        private GetBirthdayPeopleDetailsResponseProvider _getBirthdayPeopleDetailsResponseProvider;
        private AddBirthdayToListResponseProvider _addBirthdayToListResponseProvider;
        private AddBirthdayToTheListHelper _addBirthdayHelper;

        public BirthdayController()
        {
            _addBirthdayRequestValidator = new AddBirthdayToListRequestValidator();
            _addBirthdayToListResponseProvider = new AddBirthdayToListResponseProvider();
            _getBirthdayPeopleDetailsResponseProvider = new GetBirthdayPeopleDetailsResponseProvider();
            _addBirthdayHelper = new AddBirthdayToTheListHelper();
        }
                
        [HttpGet("lastname/{lastname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBirthDayPersonInfoByLastName(string lastName)
        {
            var response = new GetBirthDayPeopleDetailsResponse();
            try
            {
                response = _getBirthdayPeopleDetailsResponseProvider.GetBirthdaysFilteringByLastName(lastName);
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
                response = _getBirthdayPeopleDetailsResponseProvider.GetBirthdaysForToday();
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
                return BadRequest(_addBirthdayToListResponseProvider.GetBadRequstResponse());
            }
            
            try
            {
                _addBirthdayHelper.AddNewBirthdayPersonToCsvfile(addBirthdayToTheListRequest);
            }
            
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
            
            return Created(string.Empty,_addBirthdayToListResponseProvider.GetCreatedResponse());
        }

        private bool ValidateIfAddBirthDayRequestIsValid(AddBirthdayToTheListRequest addBirthdayToTheListRequest)
        {
            return _addBirthdayRequestValidator.Validate(addBirthdayToTheListRequest).IsValid;
        }
    }
}
