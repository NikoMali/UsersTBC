using System.Collections.Generic;
using System.Threading.Tasks;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.HelperModel;

namespace UsersTBC.Domain.Interfaces
{

    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUser(int id);
        Task<List<UseRelated>> GetRelatedUsersByUserId(int id);
    }
}
