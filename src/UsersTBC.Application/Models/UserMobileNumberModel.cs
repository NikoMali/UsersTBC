using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Models
{
    public class UserMobileNumberModel
    {
        public MobileNumberType Type { get; set; }
        public string Number { get; set; }
    }
}
