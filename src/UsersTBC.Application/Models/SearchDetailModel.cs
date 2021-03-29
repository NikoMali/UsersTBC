using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class SearchDetailModel
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }   
        public Gender Gender { get; set; } 
        public CityModel City { get; set; }
    }
}
