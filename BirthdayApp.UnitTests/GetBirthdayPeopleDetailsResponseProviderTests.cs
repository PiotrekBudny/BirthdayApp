using BirthdayTracker.UnitTests.Builders;
using BirthdayTracker.Web.CsvParser;
using BirthdayTracker.Web.Providers;
using Moq;
using NUnit.Framework;

namespace BirthdayTracker.UnitTests
{
    [TestFixture]
    public class GetBirthdayPeopleDetailsResponseProviderTests
    {
        Mock <ICsvReaderWrapper> csvReaderWrapperInterface;
                
        [SetUp]      
        public void SetUp()
        {
            csvReaderWrapperInterface = new Mock<ICsvReaderWrapper>();
        }
                     
        [Test]
        public void Can_Get_Birthdays_Filtering_By_Last_Name_When_List_Contains_Three_Same_Last_Names()
        {
            //ARRANGE
            var filteringValue = "lastname";
            var mockedCsvListResponse = new MockBirthdayPersonListBuilder()
                .WithLastName("lastname")
                .WithLastName("lastname")
                .WithLastName("lastname")
                .WithLastName("somelastname")
                .Build();

            csvReaderWrapperInterface.Setup(m => m.ReadFromBirthDayCsvFile()).Returns(mockedCsvListResponse); 
            
            var getVirthdayPeopleDetailsProvider = new GetBirthdayPeopleDetailsResponseProvider(csvReaderWrapperInterface.Object);
            //ACT
            var result = getVirthdayPeopleDetailsProvider.GetBirthdaysFilteringByLastName(filteringValue);
            //ASSERT
            Assert.AreEqual(3,result.BirthdayPeopleList.Count);
        }
        
        [Test]
        public void Can_Not_Get_Birthdays_Filtering_By_LastName_When_List_IsEmpty()
        {
            //ARRANGE
            var filteringValue = "lastname";            
            var mockedEmptyCsvListResponse = new MockBirthdayPersonListBuilder().Build();
            
            csvReaderWrapperInterface.Setup(m => m.ReadFromBirthDayCsvFile()).Returns(mockedEmptyCsvListResponse);
            var getVirthdayPeopleDetailsProvider = new GetBirthdayPeopleDetailsResponseProvider(csvReaderWrapperInterface.Object);

            //ACT
            var result = getVirthdayPeopleDetailsProvider.GetBirthdaysFilteringByLastName(filteringValue);
            //ASSERT
            Assert.Zero(result.BirthdayPeopleList.Count);
        }

        [Test]
        public void Can_Get_Single_Birthday_For_Today()
        {
            //ARRANGE
            var mockedCsvListResponse = new MockBirthdayPersonListBuilder()
                .WithBirthdayOnDate(new DateTimeProvider().UtcNow)
                .Build();
            csvReaderWrapperInterface.Setup(m => m.ReadFromBirthDayCsvFile()).Returns(mockedCsvListResponse);
            var getVirthdayPeopleDetailsProvider = new GetBirthdayPeopleDetailsResponseProvider(csvReaderWrapperInterface.Object);
            //ACT
            var result = getVirthdayPeopleDetailsProvider.GetBirthdaysForToday();
            //ASSERT
            Assert.AreEqual(1,result.BirthdayPeopleList.Count);
        }

        [Test]
        public void Can_Get_Multiple_Birthdays_For_Today()
        {
            //ARRANGE
            var mockedCsvListResponse = new MockBirthdayPersonListBuilder()
                .WithBirthdayOnDate(new DateTimeProvider().UtcNow)
                .WithBirthdayOnDate(new DateTimeProvider().UtcNow)
                .WithBirthdayOnDate(new DateTimeProvider().UtcNow)
                .WithBirthdayOnDate(new DateTimeProvider().UtcNow.AddDays(9))
                .Build();

            csvReaderWrapperInterface.Setup(m => m.ReadFromBirthDayCsvFile()).Returns(mockedCsvListResponse);
            
            var getVirthdayPeopleDetailsProvider = new GetBirthdayPeopleDetailsResponseProvider(csvReaderWrapperInterface.Object);
            //ACT
            var result = getVirthdayPeopleDetailsProvider.GetBirthdaysForToday();
            //ASSERT
            Assert.AreEqual(3, result.BirthdayPeopleList.Count);
        }

        [Test]
        public void Can_Not_Get_Any_Birthdays_For_Today_When_List_Contains_Other_Dates()
        {
            //ARRANGE
            var mockedCsvListResponse = new MockBirthdayPersonListBuilder()
                .WithBirthdayOnDate(new DateTimeProvider().UtcNow.AddDays(2))
                .WithBirthdayOnDate(new DateTimeProvider().UtcNow.AddDays(1))
                .WithBirthdayOnDate(new DateTimeProvider().UtcNow.AddDays(4))
                .WithBirthdayOnDate(new DateTimeProvider().UtcNow.AddDays(9))
                .Build();

            csvReaderWrapperInterface.Setup(m => m.ReadFromBirthDayCsvFile()).Returns(mockedCsvListResponse);

            var getVirthdayPeopleDetailsProvider = new GetBirthdayPeopleDetailsResponseProvider(csvReaderWrapperInterface.Object);
            //ACT
            var result = getVirthdayPeopleDetailsProvider.GetBirthdaysForToday();
            //ASSERT
            Assert.Zero(result.BirthdayPeopleList.Count);
        }

        [Test]
        public void Can_Not_Get_Any_Birthdays_For_Today_When_List_Empty()
        {
            //ARRANGE
            var mockedCsvListResponse = new MockBirthdayPersonListBuilder()
                .Build();

            csvReaderWrapperInterface.Setup(m => m.ReadFromBirthDayCsvFile()).Returns(mockedCsvListResponse);

            var getVirthdayPeopleDetailsProvider = new GetBirthdayPeopleDetailsResponseProvider(csvReaderWrapperInterface.Object);
            //ACT
            var result = getVirthdayPeopleDetailsProvider.GetBirthdaysForToday();
            //ASSERT
            Assert.Zero(result.BirthdayPeopleList.Count);
        }
    }
}
