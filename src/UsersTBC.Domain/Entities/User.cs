using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Domain.Entities
{
    public class User: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}
