using Microsoft.AspNetCore.Hosting;
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
using UsersTBC.Domain.Enums;
using UsersTBC.Domain.Help;
using UsersTBC.Infrastructure.Helpers;
using UsersTBC.Infrastructure.Reporting;
using UsersTBC.WebAPI.Helpers;

namespace UsersTBC.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly IUriService _uriService;
        private readonly IWebHostEnvironment _web;

        public UsersController(
            IUserService userService, 
            IUriService uriService, 
            IWebHostEnvironment web)
        {
            _userService = userService;
            _uriService = uriService;
            _web = web;
        }

        
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel userRequestModel)
        {
            var result = await _userService.Create(userRequestModel);
            return Ok(new GenericResponse(true));
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequestModel userUpdateRequestModel)
        {
            var result = await _userService.UpdateUser(userUpdateRequestModel);
            return Ok(new GenericResponse(true));
        }

        [HttpPost("AddOrUpdateImage")]
        public async Task<IActionResult> AddOrUpdateImage([FromForm] UserImageRequestModel userImageRequestModel)
        {
            var result = await _userService.AddOrUpdateImage(userImageRequestModel);
            return Ok(new GenericResponse(true));
        }

        [HttpPost("AddOrUpdateUserRelated")]
        public async Task<IActionResult> AddOrUpdateUserRelated([FromBody] UserRelatedRequestModel userRelatedRequestModel)
        {
            var result = await _userService.AddOrUpdateUserRelated(userRelatedRequestModel);
            return Ok(new GenericResponse(true));
        }

        [HttpDelete("UserDelete/{userId}")]
        public async Task<IActionResult> UserDelete(int userId)
        {
            var result = await _userService.RemoveUser(userId);
            return Ok(new GenericResponse(true));
        }

        [HttpGet("{Id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<User>))]
        public async Task<IActionResult> GetUser(int Id)
        {
            var result = await _userService.GetUser(Id);
            var k = Gender.Male.GetDescription();
            return Ok(new GenericResponseWithData<UserResponseModel>(result,true));
        }
        [HttpGet("WithRelatedPersons/{RelatedTypeId}")]
        public async Task<IActionResult> UsersWithRelatedPersons(RelatedType RelatedTypeId)
        {
            var route = Request.Path.Value;
            var result = await _userService.UsersWithRelatedPersons(RelatedTypeId);
            var reportPath = ReportUsersWithRelatedPerson.ReportUsers(_web,_uriService.GetBaseUrl(), result);
            return Ok(new GenericResponseWithDataList<UserResponseModel>(result,true,"ReportPdfPathView: " + reportPath));
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
