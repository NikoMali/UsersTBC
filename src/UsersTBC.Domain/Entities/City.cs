using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Domain.Entities
{
    public class City: BaseEntity
    {
        public string Name { get; set; }
        public string IsActive { get; set; }

        public List<User> Users { get; set; }
    }
}
