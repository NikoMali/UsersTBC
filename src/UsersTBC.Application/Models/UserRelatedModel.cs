using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class UserRelatedModel
    {
        [Required]
        public int RelatedUserId { get; set; }

        [EnumDataType(typeof(RelatedTypeEnum), ErrorMessage = "type value doesn't exist within enum")]
        public int RelatedTypeId { get; set; }

    }
}
