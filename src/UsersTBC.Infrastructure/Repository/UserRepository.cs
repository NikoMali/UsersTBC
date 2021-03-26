using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.HelperModel;
using UsersTBC.Domain.Interfaces;
using UsersTBC.Insfrastructure.Helpers;
using System.Linq;
using System.Collections.Generic;
using UsersTBC.Domain.Enum;
using System;

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
        public Task<List<UserWithRelatedPerson>> UsersWithRelatedPersons(RelatedType id)
        {
            var Users = from U in _dbContext.Users.AsEnumerable()
                        join UR in _dbContext.UseRelateds.AsEnumerable() on U.Id equals UR.UserId into U_UR
                        from UR in U_UR.DefaultIfEmpty()
                        where UR?.RelatedType == id
                        group new { UR, U } by new { UR.UserId } into URg
                        select new UserWithRelatedPerson(URg.FirstOrDefault().U, URg.Select(urg => urg.UR));

            return Task.FromResult(Users.Take(10).ToList());

        }

        public async Task<List<User>> SearchQuick(string SearchString)
        {
            var users = from U in _dbContext.Users
                        join C in _dbContext.Cities on U.CityId equals C.Id
                        where U.FirstName.Contains(SearchString)
                              ||
                              U.LastName.Contains(SearchString)
                              ||
                              U.PersonalNumber.Contains(SearchString)
                        select new User(U,C);

            return await users.ToListAsync();
        }

        public Task<IEnumerable<User>> SearchDetail(User userSearch)
        {
            var result = from U in _dbContext.Users.AsEnumerable()
                         join C in _dbContext.Cities.AsEnumerable() on U.CityId equals C.Id
                         select new User(U, C);

            if (!string.IsNullOrEmpty(userSearch?.FirstName))
            {
                result = result.Where(x => x.FirstName == userSearch.FirstName);
            }
            if (!string.IsNullOrEmpty(userSearch?.LastName))
            {
                result = result.Where(x => x.LastName == userSearch.LastName);
            }
            if (!string.IsNullOrEmpty(userSearch?.PersonalNumber))
            {
                result = result.Where(x => x.PersonalNumber == userSearch.PersonalNumber);
            }
            if (!string.IsNullOrEmpty(userSearch?.City?.Name))
            {
                result = result.Where(x => x.City.Name == userSearch.City.Name);
            }
            if (!string.IsNullOrEmpty(userSearch?.BirthDate.ToString()) && userSearch?.BirthDate != DateTime.MinValue && userSearch?.BirthDate != default(DateTime))
            {
                result = result.Where(x => x.BirthDate == userSearch.BirthDate);
            }
            if (userSearch?.Gender != null)
            {

                if (Enum.IsDefined(typeof(Gender), userSearch.Gender))
                {
                    result = result.Where(x => x.Gender == userSearch.Gender);
                }
            }
            return Task.FromResult(result.AsEnumerable());
        }
        /*from UR in _dbContext.UseRelateds.AsEnumerable()
                        group UR by UR.UserId into URg
                        join U in _dbContext.Users.AsEnumerable() on URg.FirstOrDefault().UserId equals U.Id
                        where(from urg in URg where urg.RelatedType == id select new { userRelated = urg
    }).FirstOrDefault()?.userRelated?.RelatedType == id
select new UserWithRelatedPerson(U, URg);*/


        /*var Users = from UR in _dbContext.UseRelateds
                    join U in _dbContext.Users on UR.UserId equals U.Id
                    where UR.RelatedType == id
                    group new { UR, U } by new { UR.UserId } into URg
                    let urgroup = URg.FirstOrDefault()
                    let ur = urgroup.UR
                    let u = urgroup.U
                    select new UserWithRelatedPerson(u, URg.Select(urg => urg.UR));

            return Task.FromResult(Users.AsEnumerable().ToList());*/
    }
}
