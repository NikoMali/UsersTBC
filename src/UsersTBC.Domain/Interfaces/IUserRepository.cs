using System.Collections.Generic;
using System.Threading.Tasks;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Enums;
using UsersTBC.Domain.HelperModel;

namespace UsersTBC.Domain.Interfaces
{

    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUser(int id);
        Task<List<UseRelated>> GetRelatedUsersByUserId(int id);
        Task<List<UserWithRelatedPerson>> UsersWithRelatedPersons(RelatedTypeEnum id);
        Task<List<User>> SearchQuick(string SearchString);
        Task<IEnumerable<User>> SearchDetail(User userSearch);
    }
}
