using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UsersTBC.Application.Helpers;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class UserRequestModel
    {
        [Required(ErrorMessage = "Pls fill")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "This is Short letter")]
        [RegularExpression(@"^([a-zA-Z]+)$|^([ა-ჰ]+)$", ErrorMessage = "Only Latin Or Only Georgian")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Pls fill")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "This is Short letter")]
        [RegularExpression(@"^([a-zA-Z]+)$|^([ა-ჰ]+)$", ErrorMessage = "Only Latin Or Only Georgian")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "Only 11 Number")]
        public string PersonalNumber { get; set; }

        [Required]
        [BirthDateRange("BirthDate", 18)]
        public DateTime BirthDate { get; set; }


        [EnumDataType(typeof(Gender), ErrorMessage = "type value doesn't exist within enum")]
        public Gender Gender { get; set; }

        [Required]
        public int CityId { get; set; }


        public List<UserMobileNumberModel> UserMobileNumbers { get; set; }
        public List<UserImageModel> Images { get; set; }
        public List<UserRelatedModel> userRelateds { get; set; }
    }
}
