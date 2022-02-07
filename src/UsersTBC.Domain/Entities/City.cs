using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Domain.Entities
{
    public class City: BaseEntity
    {
        public string Name { get; set; }
        public string IsActive { get; set; }

        private readonly HashSet<User> _users = new HashSet<User>();
        public IReadOnlyCollection<User> Users => _users;

        private readonly HashSet<City_Translation> _city_Translations = new HashSet<City_Translation>();
        public IReadOnlyCollection<City_Translation> City_Translations => _city_Translations;
    }
}
