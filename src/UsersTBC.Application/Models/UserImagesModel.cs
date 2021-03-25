using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Models
{
    public class UserImageModel
    {
        public string DocumentName { get; set; }
        public string Image { get; set; }
    }
}
