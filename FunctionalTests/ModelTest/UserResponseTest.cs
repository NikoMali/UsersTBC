using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Application.Models;

namespace FunctionalTests.ModelTest
{
    public class UserResponseTest
    {
        public DataTest data { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class DataTest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public CityModel City { get; set; }
        public List<UserMobileNumberModelTest> UserMobileNumbers { get; set; }
        public List<UserImagesResponseModel> Images { get; set; }
        public List<UserRelatedResponseModelTest> userRelateds { get; set; }
    }

    public class UserMobileNumberModelTest
    {
        
        public string Type { get; set; }

        public string Number { get; set; }
    }

    public class UserRelatedResponseModelTest
    {

        public int RelatedUserId { get; set; }
        public UserModelTest RelatedUser { get; set; }
        public string RelatedType { get; set; }
    }

    public class UserModelTest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public CityModel City { get; set; }
    }
}
