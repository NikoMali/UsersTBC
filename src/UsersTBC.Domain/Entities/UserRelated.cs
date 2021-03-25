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

        public UseRelated() { }
        public UseRelated(UseRelated useRelated, User user, City city, int userId)
        {
            Id = useRelated.Id;
            UserId = userId;
            RelatedUserId = useRelated.RelatedUserId;
            RelatedUser = new User(user,city);
            RelatedType = useRelated.RelatedType;
        }
        public void AssignedUserId(int userId)
        {
            UserId = userId;
        }
    }
}
