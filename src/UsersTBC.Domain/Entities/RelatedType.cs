using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;
using UsersTBC.Domain.Help;

namespace UsersTBC.Domain.Entities
{
    public class RelatedType : BaseEntity, IEnumModel<RelatedType, int, RelatedTypeEnum>
    {
        public RelatedTypeEnum Name { get; set; }
        public bool IsActive { get; set; }

    }
}
