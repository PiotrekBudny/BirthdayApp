using System;
using System.Collections.Generic;
using BirthdayTracker.Web.Models;
using BirthdayTracker.Web.Providers;

namespace BirthdayTracker.UnitTests.Builders
{
    public class MockBirthdayPersonListBuilder
    {
        List<BirthdayPerson> _birthdayPersonList;

        public MockBirthdayPersonListBuilder()
        {
            _birthdayPersonList = new List<BirthdayPerson>();
        }

        public MockBirthdayPersonListBuilder WithLastName(string lastName)
        {
            var birthdayPerson = new BirthdayPerson()
            {
                LastName = lastName,
                FirstName = "Name",
                DayOfBirth = new DateTimeProvider().UtcNow.ToString("dd/MM/yyyy")
            };

            _birthdayPersonList.Add(birthdayPerson);

            return this;
        }

        public MockBirthdayPersonListBuilder WithBirthdayOnDate(DateTime dateTime)
        {
            var birthdayPerson = new BirthdayPerson()
            {
                LastName = "LastName",
                FirstName = "Name",
                DayOfBirth = dateTime.ToString("dd/MM/yyyy")
            };

            _birthdayPersonList.Add(birthdayPerson);

            return this;
        }

        public List<BirthdayPerson> Build()
        {
            return _birthdayPersonList;
        }
    }
}
