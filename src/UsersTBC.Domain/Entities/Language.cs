using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Domain.Entities
{
    public class Language: BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string IsActive { get; set; }


        private readonly List<City_Translation> city_Translations = new List<City_Translation>();
        public IReadOnlyCollection<City_Translation> City_Translations => city_Translations.AsReadOnly();


    }
}
