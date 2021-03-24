using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UsersTBC.Application.ApplicationDbContext;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.Domain.Entities;

namespace UsersTBC.Application.Services.Repository
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;


        public UserService(IApplicationDbContext context)
        {
            _context = context;
        }

        

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
