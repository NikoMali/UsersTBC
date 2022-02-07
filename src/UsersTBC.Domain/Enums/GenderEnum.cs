using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UsersTBC.Domain.Help;

namespace UsersTBC.Domain.Enums
{
    /// <summary>
    /// In general, instead of enum, I use Entity, or StaticEntity, as a static value. But in this case I decided to use Enum
    /// </summary>
    public enum GenderEnum: int
    {
        Male = 1,
        Female = 2   
    }
}
