using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Interfaces;

namespace UsersTBC.Application.Services.Intarface
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRepository<UserMobileNumber> UserMobileRepository { get; }
        IRepository<UserImage> UserImageRepository { get; }
        IRepository<UseRelated> UserRelatedRepository { get; }
        Task SaveChangesAsync();
        void SaveChanges();
        void Dispose();
    }
}
