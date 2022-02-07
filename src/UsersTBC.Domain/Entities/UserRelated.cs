using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Domain.Entities
{
    public class UseRelated: BaseEntity
    {
        //TO DO სასურველია სხვა გზა მოვნახო ? მითითების გარეშე
        public int? UserId { get; set; }
        public int RelatedUserId { get; set; }
        public int RelatedTypeId { get; set; }


        [NotMapped]
        public User User { get; set; }
        [NotMapped]
        public User RelatedUser { get; set; }
        public RelatedType RelatedType { get; set; }
        



        public UseRelated() { }
        public UseRelated(UseRelated useRelated, User user, City city,City_Translation city_Translation, int userId)
        {
            Id = useRelated.Id;
            UserId = userId;
            RelatedUserId = useRelated.RelatedUserId;
            RelatedUser = new User(user,city);
            RelatedTypeId = useRelated.RelatedTypeId;
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
