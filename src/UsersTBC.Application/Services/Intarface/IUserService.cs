using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UsersTBC.Application.Models;
using UsersTBC.Domain.Entities;

namespace UsersTBC.Application.Services.Intarface
{
    public interface IUserService
    {

        Task<IEnumerable<UserResponseModel>> GetAll();
        Task<string> Create(UserRequestModel userRequestModel);
        Task<string> UpdateUser(UserUpdateRequestModel userUpdateRequestModel);
        Task<string> AddOrUpdateImage(UserImageRequestModel userImageRequestModel);
        Task<string> AddOrUpdateUserRelated(UserRelatedRequestModel userRelatedRequestModel);
        Task<string> RemoveUser(int userId);
        Task<UserResponseModel> GetUser(int userId);

    }
}
