using System;
using System.Collections.Generic;
using System.Text;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Enums;

namespace UsersTBC.Domain.HelperModel
{
    public class UserWithRelatedPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }


        //refference
        public int CityId { get; set; }
        public City City { get; set; }

        
        public IEnumerable<UseRelated> useRelateds { get; set; }

        public UserWithRelatedPerson() { }
        public UserWithRelatedPerson(User user, IEnumerable<UseRelated> useRelated) 
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            PersonalNumber = user.PersonalNumber;
            BirthDate = user.BirthDate;
            Gender = user.Gender;
            CityId = user.CityId;
            useRelateds = useRelated;
        }

    }
}
