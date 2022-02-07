using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Domain.Entities
{
    public class User: BaseEntity,IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public int CityId { get; set; }


        public Gender Gender { get; set; }
        public City City { get; set; }

        public User() {
        
            
        }
        public User(User user, City city)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PersonalNumber = user.PersonalNumber;
            BirthDate = user.BirthDate;
            GenderId = user.GenderId;
            CityId = city.Id;
            City = city;
        }
        public User(User user, City city, City_Translation city_Translation)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PersonalNumber = user.PersonalNumber;
            BirthDate = user.BirthDate;
            GenderId = user.GenderId;
            CityId = city.Id;
            City = city;
            if (city_Translation != null)
            {
                City.Name = city_Translation.NameTranslate;
            }
        }

        private readonly HashSet<UserMobileNumber> _userMobileNumbers = new HashSet<UserMobileNumber>();
        public IReadOnlyCollection<UserMobileNumber> UserMobileNumbers => _userMobileNumbers;

        private readonly HashSet<UserImage> _userImages = new HashSet<UserImage>();
        public IReadOnlyCollection<UserImage> userImages => _userImages;

        private readonly HashSet<UseRelated> _userRelateds = new HashSet<UseRelated>();
        public IReadOnlyCollection<UseRelated> useRelateds => _userRelateds;
        
    }
}
