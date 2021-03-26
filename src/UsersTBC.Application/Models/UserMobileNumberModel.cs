using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Models
{
    public class UserMobileNumberModel
    {
        [EnumDataType(typeof(Gender), ErrorMessage = "type value doesn't exist within enum")]
        public MobileNumberType Type { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "This not valid Number 4-50 range")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only Number")]
        public string Number { get; set; }
    }
}
