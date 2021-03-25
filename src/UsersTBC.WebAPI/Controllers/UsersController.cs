using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersTBC.Application.Models;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.Domain.Entities;

namespace UsersTBC.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok(await _userService.GetAll());
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel userRequestModel)
        {
            var result = await _userService.Create(userRequestModel);
            return Ok(new { status = result });
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequestModel userUpdateRequestModel)
        {
            var result = await _userService.UpdateUser(userUpdateRequestModel);
            return Ok(new { status = result });
        }

        [HttpPost("AddOrUpdateImage")]
        public async Task<IActionResult> AddOrUpdateImage([FromForm] UserImageRequestModel userImageRequestModel)
        {
            var result = await _userService.AddOrUpdateImage(userImageRequestModel);
            return Ok(new { status = result });
        }

        [HttpPost("AddOrUpdateUserRelated")]
        public async Task<IActionResult> AddOrUpdateUserRelated([FromBody] UserRelatedRequestModel userRelatedRequestModel)
        {
            var result = await _userService.AddOrUpdateUserRelated(userRelatedRequestModel);
            return Ok(new { status = result });
        }

        [HttpDelete("UserDelete/{userId}")]
        public async Task<IActionResult> UserDelete(int userId)
        {
            var result = await _userService.RemoveUser(userId);
            return Ok(new { status = result });
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var result = await _userService.GetUser(userId);
            return Ok(result);
        }







    }
}
