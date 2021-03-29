using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UsersTBC.Domain.Help;
using UsersTBC.Domain.Resources;

namespace UsersTBC.Domain.Enums
{
    /// <summary>
    /// In general, instead of enum, I use Entity, or StaticEntity, as a static value. But in this case I decided to use Enum
    /// </summary>
    public enum Gender
    {
        [EnumMember(Value = "Maleee")]
        //[LocalizedDescription("Male", typeof(ResourceEn))]
        [LocalizedDescription("Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2   
    }
}
