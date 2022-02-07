using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;
using UsersTBC.Domain.Help;

namespace UsersTBC.Domain.Entities
{
    public class Gender : BaseEntity, IEnumModel<Gender, int, GenderEnum>
    {
        public GenderEnum Name { get; set; }
        public bool IsActive { get; set; }
        
    }
}
