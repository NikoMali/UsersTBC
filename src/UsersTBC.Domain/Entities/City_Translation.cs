using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Domain.Entities
{
    public class City_Translation: BaseEntity
    {
        public int CityId { get; set; }
        public int LanguageId { get; set; }
        public string NameTranslate { get; set; }
        
    }
}
