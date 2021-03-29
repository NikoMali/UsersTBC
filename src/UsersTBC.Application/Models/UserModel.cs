using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public int CityId { get; set; }
        public CityModel City { get; set; }
    }
}
