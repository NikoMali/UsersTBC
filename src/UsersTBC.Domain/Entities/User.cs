using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Domain.Entities
{
    public class User: BaseEntity,IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }


        //refference
        public int CityId { get; set; }
        public City City { get; set; }

        public User() { }
        public User(User user, City city)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PersonalNumber = user.PersonalNumber;
            BirthDate = user.BirthDate;
            Gender = user.Gender;
            CityId = city.Id;
            City = city;
        }
        //public List<UserMobileNumber> userMobileNumbers { get; set; }
        //public List<UserImage> userImages { get; set; }
        //public List<UseRelated> useRelateds { get; set; }
    }
}
