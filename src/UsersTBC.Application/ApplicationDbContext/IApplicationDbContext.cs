
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

        Task<int> SaveChangesAsync();
    }
}
