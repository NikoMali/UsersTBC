using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Domain.Entities
{
    public class UserImage: BaseEntity
    {
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public void AssignedUserId(int userId)
        {
            UserId = userId;
        }
    }
}
