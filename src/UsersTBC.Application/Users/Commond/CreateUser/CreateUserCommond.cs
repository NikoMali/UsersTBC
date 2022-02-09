using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using UsersTBC.Application.Helpers;
using UsersTBC.Application.Models;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Interfaces;
using UsersTBC.Domain.Localize;

namespace UsersTBC.Application.Users.Commond.CreateUser
{
    public class CreateUserCommond: IRequest<string>
    {
        public UserRequestModel UserRequestModel { get; set; }
    }
    public class CreateUserCommondHandler : IRequestHandler<CreateUserCommond, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IStringLocalizer<Resource> _localizer;

        public CreateUserCommondHandler(
                            IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IStringLocalizer<Resource> localizer
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<string> Handle(CreateUserCommond request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.UserRequestModel);
            var mobileNumber = _mapper.Map<List<UserMobileNumber>>(request.UserRequestModel.UserMobileNumbers);
            var images = _mapper.Map<List<UserImage>>(request.UserRequestModel.Images);
            var userRelateds = _mapper.Map<List<UseRelated>>(request.UserRequestModel.userRelateds);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            /*if (mobileNumber.Count > 0)
            {
                mobileNumber.ForEach(x => x.AssignedUserId(user.Id));
                mobileNumber.ForEach(x => _userMobileRepository.AddAsync(x));
            }

           */

            if (userRelateds.Count > 0)
            {
                userRelateds.ForEach(x => x.AssignedUserId(user.Id));
                userRelateds.ForEach(x => _unitOfWork.UserRelatedRepository.AddAsync(x));
            }

            //await UserSaveImages(request.UserRequestModel.Images, user.Id);

            return ResultStatus.SUCCESS;

        }


    }
}