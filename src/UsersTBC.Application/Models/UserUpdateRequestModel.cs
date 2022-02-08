using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class UserUpdateRequestModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        [EnumDataType(typeof(GenderEnum), ErrorMessage = "type value doesn't exist within enum")]
        public int GenderId { get; set; }
        public int CityId { get; set; }
        public List<UserMobileNumberRequestModel> UserMobileNumbers { get; set; }

    }
}
