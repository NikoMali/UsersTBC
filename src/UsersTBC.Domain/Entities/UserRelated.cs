using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

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
        public UseRelated(UseRelated useRelated, User user, City city,City_Translation city_Translation, int userId)
        {
            Id = useRelated.Id;
            UserId = userId;
            RelatedUserId = useRelated.RelatedUserId;
            RelatedUser = new User(user,city);
            RelatedType = useRelated.RelatedType;
            if (city_Translation != null)
            {
                RelatedUser.City.Name = city_Translation.NameTranslate;
            }
        }
        public void AssignedUserId(int userId)
        {
            UserId = userId;
        }
    }
}
