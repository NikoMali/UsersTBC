using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class SearchDetailModel
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; } 
        
        public GenderEnum Genders { get; set; } 
        public CityModel City { get; set; }
        [NotMapped]
        public int GenderId { get { return _genderId; } set { _genderId = (int)Genders; } }


        private int _genderId;
    }
}
