using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UsersTBC.Application.Filter;
using UsersTBC.Application.Helpers;
using UsersTBC.Application.Models;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Enum;

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
        Task<List<UserResponseModel>> UsersWithRelatedPersons(RelatedType relatedTypeId);
        Task<GetAllWithPaging<UserModel, PaginationFilterQuickSeach>> SearchQuick(string searchString, int PageNumber, int PageSize);
        Task<GetAllWithPaging<UserModel, PaginationFilterDetailSearch>> SearchDetail(PaginationFilterDetailSearch paginationFilterDetailSearch);
        //Task<List<UserModel>> SearchDetail(SearchDetailModel searchUser);

    }
}
