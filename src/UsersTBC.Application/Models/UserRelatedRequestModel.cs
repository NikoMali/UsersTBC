using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Models
{
    public class UserRelatedRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RelatedUserId { get; set; }
        public RelatedType RelatedType { get; set; }

    }
}
