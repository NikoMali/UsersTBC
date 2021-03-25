using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enum;

namespace UsersTBC.Application.Models
{
    public class UserImageRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IFormFile file { get; set; }
    }
}
