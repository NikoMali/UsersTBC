using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class UserMobileNumberModel
    {
        [EnumDataType(typeof(MobileNumberTypeEnum), ErrorMessage = "type value doesn't exist within enum")]
        public int MobileNumberTypeId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "This not valid Number 4-50 range")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only Number")]
        public string Number { get; set; }
    }
}
