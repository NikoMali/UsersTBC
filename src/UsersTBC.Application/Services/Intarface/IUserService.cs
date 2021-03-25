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
        Task<UserResponseModel> Create(UserRequestModel userRequestModel);


    }
}
