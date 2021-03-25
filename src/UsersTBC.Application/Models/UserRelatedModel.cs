using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Models
{
    public class UserRelatedModel
    {
        
        public int RelatedUserId { get; set; }
        public RelatedType RelatedType { get; set; }

    }
}
