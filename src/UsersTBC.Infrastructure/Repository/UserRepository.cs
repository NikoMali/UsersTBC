using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.HelperModel;
using UsersTBC.Domain.Interfaces;
using UsersTBC.Insfrastructure.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace UsersTBC.Insfrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _dbContext;
        public UserRepository(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUser(int id)
        {
            var result = from U in _dbContext.Users
                         join C in _dbContext.Cities on U.CityId equals C.Id
                         /*join UM in _dbContext.UserMobileNumbers on U.Id equals UM.UserId into U_UM
                         from UM in U_UM.DefaultIfEmpty()
                         join UI in _dbContext.UserImages on U.Id equals UI.UserId into U_UI
                         from UI in U_UI.DefaultIfEmpty()
                         join UR in _dbContext.UseRelateds on U.Id equals UR.UserId into U_UR
                         from UR in U_UR.DefaultIfEmpty()*/
                         where U.Id == id
                         select new User(U, C);
            return await result.FirstOrDefaultAsync();

        }

        public async Task<List<UseRelated>> GetRelatedUsersByUserId(int id)
        {
            var result = from UR in _dbContext.UseRelateds
                         join U in _dbContext.Users on UR.RelatedUserId equals U.Id
                         join C in _dbContext.Cities on U.CityId equals C.Id
                         
                         where UR.UserId == id
                         select new UseRelated(UR,U,C,id);
            return await result.ToListAsync();

        }
    }
}
