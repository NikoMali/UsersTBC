using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class UserRelatedResponseModel
    {
        public int RelatedUserId { get; set; }
        public int RelatedUserId1111 { get; set; }
        public UserModel RelatedUser { get; set; }
        public RelatedTypeEnum RelatedType { get; set; }

    }
}
