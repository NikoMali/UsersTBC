using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Models
{
    public class UserImagesResponseModel
    {
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
    }
}
