using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Enum;
using UsersTBC.Domain.HelperModel;

namespace UsersTBC.Application.Models
{
    public class UserResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public int CityId { get; set; }
        public CityModel City { get; set; }
        public List<UserMobileNumberModel> UserMobileNumbers { get; set; }
        public List<UserImagesResponseModel> Images { get; set; }
        public List<UserRelatedResponseModel> userRelateds { get; set; }

        public UserResponseModel() { }
        /*public UserResponseModel(UserWithRelatedPerson userWithRelatedPerson)
        {
            FirstName = userWithRelatedPerson.FirstName;
            LastName = userWithRelatedPerson.LastName;
            PersonalNumber = userWithRelatedPerson.PersonalNumber;
            BirthDate = userWithRelatedPerson.BirthDate;
            Gender = userWithRelatedPerson.Gender;
            CityId = userWithRelatedPerson.CityId;
            userRelateds = userWithRelatedPerson.useRelateds;
        }*/

    }
}
