using System;
using System.Linq;
using BirthdayTracker.Database;
using BirthdayTracker.Web.Models;
using BirthdayTracker.Web.Providers;
using BirthdayTracker.Web.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayTracker.Web.Controllers
{
    [ApiController]
    [Route("api/birthdaycontroller")]
    public class BirthdayController : ControllerBase
    {
        IGetBirthdayPeopleDetailsResponseProvider getBirthdayPeopleDetailsResponseProvider;
        IAddBirthdayToListResponseProvider addBirthdayToListResponseProvider;
        IAddBirthdayHelper addBirthdayHelper;
        BirthdayDbContext _dbContext;

        public BirthdayController(IGetBirthdayPeopleDetailsResponseProvider getBirthdayPeopleDetailsResponseProvider,
                                  IAddBirthdayToListResponseProvider addBirthdayToListResponseProvider,
                                  IAddBirthdayHelper addBirthdayHelper,
                                  BirthdayDbContext dbContext)
        {
            this.addBirthdayToListResponseProvider = addBirthdayToListResponseProvider;
            this.getBirthdayPeopleDetailsResponseProvider = getBirthdayPeopleDetailsResponseProvider;
            this.addBirthdayHelper = addBirthdayHelper;
            this._dbContext = dbContext;
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
                var mapper = new AddBirthdayToDatabaseEntityMapper();
                mapper.MapRequestToDatabaseEntities(addBirthdayToTheListRequest);
                
                _dbContext.AddUserWithBirthdayInfo(mapper.GetMappedUserEntity, mapper.GetMappedBirthdayInfoEntity);
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
