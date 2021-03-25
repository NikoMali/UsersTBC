using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Models
{
    public class UserRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        [EnumDataType(typeof(Gender), ErrorMessage = "type value doesn't exist within enum")]
        public Gender Gender { get; set; }
        public int CityId { get; set; }
        public List<UserMobileNumberModel> UserMobileNumbers { get; set; }
        public List<UserImageModel> Images { get; set; }
        public List<UserRelatedModel> userRelateds { get; set; }
    }
}
