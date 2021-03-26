using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersTBC.Application.Filter;
using UsersTBC.Application.Models;
using UsersTBC.Application.Paging.Helpers;
using UsersTBC.Application.Paging.Services;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Enum;

namespace UsersTBC.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly IUriService _uriService;

        public UsersController(IUserService userService, IUriService uriService)
        {
            _userService = userService;
            _uriService = uriService;
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var result = await _userService.GetUser(userId);
            return Ok(result);
        }
        [HttpGet("WithRelatedPersons/{RelatedTypeId}")]
        public async Task<IActionResult> UsersWithRelatedPersons(RelatedType RelatedTypeId)
        {
            var result = await _userService.UsersWithRelatedPersons(RelatedTypeId);
            return Ok(result);
        }

        [HttpGet("SearchQuick")]
        public async Task<IActionResult> SearchQuick([FromQuery] PaginationFilterQuickSeach filter)
        {
            var route = Request.Path.Value;
            var model = await _userService.SearchQuick(filter.SearchString, filter.PageNumber, filter.PageSize);
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserModel>(model.entities, model.PaginationFilter, model.totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("SearchDetail")]
        public async Task<IActionResult> SearchDetail([FromQuery] PaginationFilterDetailSearch filter)
        {
            var route = Request.Path.Value;
            var model = await _userService.SearchDetail(filter);
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserModel>(model.entities, model.PaginationFilter, model.totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }






    }
}
