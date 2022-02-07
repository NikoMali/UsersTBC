using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;
using UsersTBC.Domain.Help;

namespace UsersTBC.Domain.Entities
{
    public class MobileNumberType : BaseEntity, IEnumModel<MobileNumberType, int, MobileNumberTypeEnum>
    {
        public MobileNumberTypeEnum Name { get; set; }
        public bool IsActive { get; set; }
    }
}
