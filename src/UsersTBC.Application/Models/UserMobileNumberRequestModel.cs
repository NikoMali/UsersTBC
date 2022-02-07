using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class UserMobileNumberRequestModel
    {
        public int Id { get; set; }
        public MobileNumberTypeEnum Type { get; set; }
        public string Number { get; set; }
        public int UserId { get; set; }
    }
}
