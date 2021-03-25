using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Domain.Entities
{
    public class UseRelated: BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int RelatedUserId { get; set; }
        public User RelatedUser { get; set; }
        public RelatedType RelatedType { get; set; }


        public void AssignedUserId(int userId)
        {
            UserId = userId;
        }
    }
}
