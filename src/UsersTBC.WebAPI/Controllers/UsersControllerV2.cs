using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UsersTBC.Application.Models;
using UsersTBC.Application.Paging.Services;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.WebAPI.Helpers;
using Microsoft.Extensions.DependencyInjection;
using UsersTBC.Application.Users.Commond.CreateUser;
using UsersTBC.Application.Users.Queries.GetUser;

namespace UsersTBC.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}/Users", Name = "Users")]
    public class UsersControllerV2 : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersControllerV2(
            IMediator mediator)
        {
            _mediator = mediator;

        }

        /// <summary>
        /// Create User with Mediatr
        /// </summary>
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel userRequestModel)
        {
            await _mediator.Send(new CreateUserCommond { UserRequestModel = userRequestModel });
            return Ok(
                new GenericResponse(true));
        }

        /// <summary>
        /// Get User Full Info with Mediatr
        /// Validate is method response entity exists attribute as Action Filter
        /// </summary>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUser(int Id)
        {
            return Ok(
                new GenericResponseWithData<UserResponseModel>(
                await _mediator.Send(new GetUserQuery { Id = Id }),true));
            
        }





    }
}
