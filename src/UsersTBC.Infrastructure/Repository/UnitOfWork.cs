using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Interfaces;
using UsersTBC.Infrastructure.Helpers;
using UsersTBC.Infrastructure.Repository;

namespace UsersTBC.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;

        public IUserRepository UserRepository { get; private set; }
        public IRepository<UserMobileNumber> UserMobileRepository { get; private set; }
        public IRepository<UserImage> UserImageRepository { get; private set; }
        public IRepository<UseRelated> UserRelatedRepository { get; private set; }


        public UnitOfWork(
            DataContext context
            )
        {
            _context = context;
            UserRepository = new UserRepository(context);
            UserMobileRepository = new Repository<UserMobileNumber>(context);
            UserImageRepository = new Repository<UserImage>(context);
            UserRelatedRepository = new Repository<UseRelated>(context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
