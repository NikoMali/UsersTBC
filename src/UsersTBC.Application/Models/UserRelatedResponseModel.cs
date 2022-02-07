using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Application.Models
{
    public class UserRelatedResponseModel
    {
        public int RelatedUserId { get; set; }
        public UserModel RelatedUser { get; set; }
        [JsonIgnore]
        public int RelatedTypeId { get; set; }
        public RelatedTypeEnum RelatedType { get { return _relatedTypeId; } 
            set { _relatedTypeId = (RelatedTypeEnum)RelatedTypeId; } }


        private RelatedTypeEnum _relatedTypeId;
    }
}
