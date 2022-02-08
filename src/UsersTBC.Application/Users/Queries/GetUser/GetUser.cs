using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UsersTBC.Application.Helpers;
using UsersTBC.Application.Models;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Interfaces;
using UsersTBC.Domain.Localize;

namespace UsersTBC.Application.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserResponseModel>
    {
        public int Id { get; set; }
    }
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMemoryCache _memoryCache;
        private readonly IAppLogger<GetUserQueryHandler> _appLogger;

        public GetUserQueryHandler(
                            IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IStringLocalizer<Resource> localizer,
                            IMemoryCache memoryCache,
                            IAppLogger<GetUserQueryHandler> appLogger
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _memoryCache = memoryCache;
            _appLogger = appLogger;
        }

        public async Task<UserResponseModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Start GetById {UserId}", request.Id);
            var cacheKey = "user-" + request.Id;

            if (!_memoryCache.TryGetValue(cacheKey, out User user))
            {

                user = await _unitOfWork.UserRepository.GetUser(request.Id);


                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };

                _memoryCache.Set(cacheKey, user, cacheExpiryOptions);
            }

            if (user == null)
            {
                throw new AppException(_localizer["UserNotFound"]);
            }
            var userModel = _mapper.Map<UserResponseModel>(user);
            userModel.UserMobileNumbers = _mapper.Map<List<UserMobileNumberModel>>(await _unitOfWork.UserMobileRepository.FindAll(x => x.UserId == request.Id));
            userModel.Images = _mapper.Map<List<UserImagesResponseModel>>(await _unitOfWork.UserImageRepository.FindAll(x => x.UserId == request.Id));
            userModel.userRelateds = _mapper.Map<List<UserRelatedResponseModel>>(await _unitOfWork.UserRepository.GetRelatedUsersByUserId(request.Id));
            _appLogger.LogInformation("End GetById {UserId}", request.Id);
            return userModel;
        }

    }
}
