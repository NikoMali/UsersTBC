using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class UserImageModel
    {
        public string DocumentName { get; set; }
        public string ImageBinaryData { get; set; }
    }
}
