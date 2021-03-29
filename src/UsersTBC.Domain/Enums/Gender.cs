using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UsersTBC.Domain.Help;
using UsersTBC.Domain.Resources;

namespace UsersTBC.Domain.Enums
{
    
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
