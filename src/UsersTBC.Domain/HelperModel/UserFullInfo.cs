using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Domain.HelperModel
{
    public class UserFullInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }


        //refference
        public int CityId { get; set; }
        public City City { get; set; }

        public List<UserMobileNumber> userMobileNumbers { get; set; }
        public List<UserImage> userImages { get; set; }
        public List<UseRelated> useRelateds { get; set; }

        public UserFullInfo() { }
        
    }
}
