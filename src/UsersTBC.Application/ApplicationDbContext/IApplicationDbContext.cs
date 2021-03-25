
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UsersTBC.Domain.Entities;

namespace UsersTBC.Application.ApplicationDbContext
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<UserMobileNumber> UserMobileNumbers { get; set; }
        public DbSet<UseRelated> UseRelateds { get; set; }

        Task<int> SaveChangesAsync();
    }
}
