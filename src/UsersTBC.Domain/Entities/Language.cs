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
        public List<City_Translation> City_Translations { get; set; }


    }
}
