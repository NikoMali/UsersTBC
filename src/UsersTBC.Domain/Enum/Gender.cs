using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UsersTBC.Domain.Enum
{
    
    public enum Gender
    {
        [EnumMember(Value = "Maleee")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2   
    }
}
