using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Models
{
    public class UserRelatedModel
    {
        [Required]
        public int RelatedUserId { get; set; }

        [EnumDataType(typeof(Gender), ErrorMessage = "type value doesn't exist within enum")]
        public RelatedType RelatedType { get; set; }

    }
}
