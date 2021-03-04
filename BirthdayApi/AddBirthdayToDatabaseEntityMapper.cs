using System;
using AutoMapper;
using BirthdayTracker.Database.Models;
using BirthdayTracker.Web.Models;

namespace BirthdayTracker.Web
{
    public class AddBirthdayToDatabaseEntityMapper
    { 
        public void MapRequestToDatabaseEntities(AddBirthdayToTheListRequest addBirthdayToTheListRequest)
        {
            
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AddBirthdayToTheListRequest, User>();
                cfg.CreateMap<AddBirthdayToTheListRequest, BirthdayInfo>();
                cfg.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            });

            var mapper = config.CreateMapper();

            GetMappedBirthdayInfoEntity = mapper.Map<BirthdayInfo>(addBirthdayToTheListRequest);
            GetMappedUserEntity = mapper.Map<User>(addBirthdayToTheListRequest);
        }

        public BirthdayInfo GetMappedBirthdayInfoEntity { get; private set; }
        public User GetMappedUserEntity { get; private set; }
    }

    public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            return System.Convert.ToDateTime(source);
        }
    }
}
