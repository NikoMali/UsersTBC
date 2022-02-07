using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Domain.Entities
{
    public class UserMobileNumber: BaseEntity
    {
        public int MobileNumberTypeId { get; set; }
        public string Number { get; set; }
        public int UserId { get; set; }


        public MobileNumberType MobileNumberType { get; set; }
        public User User { get; set; }

        public void AssignedUserId(int userId)
        {
            UserId = userId;
        }
    }
}
