using System;
using System.Collections.Generic;
using System.Text;

namespace UsersTBC.Domain.Enums
{
    /// <summary>
    /// In general, instead of enum, I use Entity, or StaticEntity, as a static value. But in this case I decided to use Enum
    /// </summary>
    public enum RelatedType
    {
        Colleague = 1,
        Acquaintance = 2,
        Relative = 3,
        Other = 4
    }
}
