using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Models
{
    public class UserMobileNumberRequestModel
    {
        public int Id { get; set; }
        public MobileNumberType Type { get; set; }
        public string Number { get; set; }
        public int UserId { get; set; }
    }
}
