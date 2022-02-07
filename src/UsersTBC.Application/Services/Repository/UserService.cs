using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using UsersTBC.Application.Models;
using UsersTBC.Application.Services.Intarface;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Interfaces;
using UsersTBC.Domain.Enums;
using UsersTBC.Application.Filter;
using UsersTBC.Application.Helpers;
using Microsoft.Extensions.Localization;
using UsersTBC.Domain.Localize;
using Microsoft.Extensions.Caching.Memory;

namespace UsersTBC.Application.Services.Repository
{
    public class UserService : IUserService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IMapper _mapper;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMemoryCache _memoryCache;


        public UserService(
            IUnitOfWork unitOfWork,
                            IHostingEnvironment hostEnvironment,
                            IMapper mapper,
                            IStringLocalizer<Resource> localizer,
                            IMemoryCache memoryCache
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _hostingEnvironment = hostEnvironment;
            _mapper = mapper;
            _localizer = localizer;
            _memoryCache = memoryCache;

        }





        public async Task<string> Create(UserRequestModel userRequestModel)
        {
            var user = _mapper.Map<User>(userRequestModel);
            var mobileNumber = _mapper.Map<List<UserMobileNumber>>(userRequestModel.UserMobileNumbers);
            var images = _mapper.Map<List<UserImage>>(userRequestModel.Images);
            var userRelateds = _mapper.Map<List<UseRelated>>(userRequestModel.userRelateds);

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            /*if (mobileNumber.Count > 0)
            {
                mobileNumber.ForEach(x => x.AssignedUserId(user.Id));
                mobileNumber.ForEach(x => _userMobileRepository.AddAsync(x));
            }

           

            if (userRelateds.Count > 0)
            {
                userRelateds.ForEach(x => x.AssignedUserId(user.Id));
                userRelateds.ForEach(x => _userRelatedRepository.AddAsync(x));
            }

            await UserSaveImages(userRequestModel.Images, user.Id);*/
            
            return ResultStatus.SUCCESS;
        }

        public async Task<string> UpdateUser(UserUpdateRequestModel userUpdateRequestModel)
        {
            var user = _mapper.Map<User>(userUpdateRequestModel);
            var userMobile = _mapper.Map<List<UserMobileNumber>>(userUpdateRequestModel.UserMobileNumbers);
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.UserMobileRepository.UpdateRange(userMobile);
            await _unitOfWork.SaveChangesAsync();
            return ResultStatus.SUCCESS;
        }

        public async Task<string> AddOrUpdateImage(UserImageRequestModel userImageRequestModel)
        {
            
            string fileName, filePath;
            var userImage = new UserImage();
            userImage.UserId = userImageRequestModel.UserId;
            if (userImageRequestModel.Id == 0)
            {
                UploadedFile(null,userImageRequestModel.file, out fileName, out filePath);
                userImage.DocumentName = fileName;
                userImage.DocumentPath = filePath;
                await _unitOfWork.UserImageRepository.AddAsync(userImage);
                await _unitOfWork.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
            else
            {
                userImage.Id = userImageRequestModel.Id;
                UploadedFile(null, userImageRequestModel.file, out fileName, out filePath);
                userImage.DocumentName = fileName;
                userImage.DocumentPath = filePath;
                _unitOfWork.UserImageRepository.Update(userImage);
                await _unitOfWork.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
           
            
        }
        public async Task<string> AddOrUpdateUserRelated(UserRelatedRequestModel userRelatedRequestModel)
        {
            
            var userRelated = _mapper.Map<UseRelated>(userRelatedRequestModel);
            if (userRelated.Id == 0)
            {
                await _unitOfWork.UserRelatedRepository.AddAsync(userRelated);
                await _unitOfWork.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
            else
            {
                _unitOfWork.UserRelatedRepository.Update(userRelated);
                await _unitOfWork.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
           
           
        }

        private async Task UserSaveImages(List<UserImageModel> userImages, int userId)
        {
            var userImage = new UserImage(); 
            string fileName, filePath;
            if (userImages.Count>0)
            {
                for (int i = 0; i < userImages.Count; i++)
                {
                    Byte[] bytes = Convert.FromBase64String(userImages[i].ImageBinaryData);
                    //IFormFile formFile = File.WriteAllBytes(path, bytes);
                    UploadedFile(bytes, null, out fileName, out filePath);
                    userImage.DocumentPath = filePath;
                    userImage.DocumentName = fileName;
                    userImage.UserId = userId;
                    await _unitOfWork.UserImageRepository.AddAsync(userImage);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            
        }

        public async Task<string> RemoveUser(int userId)
        {
           
             _unitOfWork.UserImageRepository.RemoveRange(await _unitOfWork.UserImageRepository.FindAll(x => x.UserId == userId));
             _unitOfWork.UserMobileRepository.RemoveRange(await _unitOfWork.UserMobileRepository.FindAll(x => x.UserId == userId));
             _unitOfWork.UserRelatedRepository.RemoveRange(await _unitOfWork.UserRelatedRepository.FindAll(x => x.UserId == userId));
             _unitOfWork.UserRepository.Delete(await _unitOfWork.UserRepository.First(x => x.Id == userId));
            await _unitOfWork.SaveChangesAsync();
            return ResultStatus.SUCCESS;


        }
        public async Task<UserResponseModel> GetUser(int userId)
        {
            var cacheKey = "user-"+userId;
            
            if (!_memoryCache.TryGetValue(cacheKey, out User user))
            {
                
                user = await _unitOfWork.UserRepository.GetUser(userId);

                
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
            userModel.UserMobileNumbers = _mapper.Map<List<UserMobileNumberModel>>(await _unitOfWork.UserMobileRepository.FindAll(x => x.UserId == userId));
            userModel.Images = _mapper.Map<List<UserImagesResponseModel>>(await _unitOfWork.UserImageRepository.FindAll(x => x.UserId == userId));
            userModel.userRelateds = _mapper.Map<List<UserRelatedResponseModel>>(await _unitOfWork.UserRepository.GetRelatedUsersByUserId(userId));

            return userModel;
        }


        public async Task<List<UserResponseModel>> UsersWithRelatedPersons(RelatedTypeEnum relatedTypeId)
        {
            var usersModel = new List<UserResponseModel>();
            var users = await _unitOfWork.UserRepository.UsersWithRelatedPersons(relatedTypeId);
            if(users.Count == 0)
            {
                throw new AppException("Not Exist Relate Type Users");
            } 

            users.ForEach(x =>
            {
                var user = _mapper.Map<UserResponseModel>(x);
                user.userRelateds = _mapper.Map<List<UserRelatedResponseModel>>(x.useRelateds);
                usersModel.Add(user);
            });
            return usersModel;

        }

        public async Task<GetAllWithPaging<UserModel, PaginationFilterQuickSeach>> SearchQuick(string searchString, int PageNumber, int PageSize)
        {
            var validFilter = new PaginationFilterQuickSeach(searchString,PageNumber, PageSize);
            var users =await _unitOfWork.UserRepository.SearchQuick(searchString);
            var usersModel = _mapper.Map<List<UserModel>>(users);
            var totalRecords = usersModel.Count();
            var pagedData = usersModel
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            var result = new GetAllWithPaging<UserModel, PaginationFilterQuickSeach>(validFilter, pagedData, totalRecords);
            return result;
        }
        public async Task<GetAllWithPaging<UserModel, PaginationFilterDetailSearch>> SearchDetail(PaginationFilterDetailSearch paginationFilterDetailSearch)
        {
            var validFilter = new PaginationFilterDetailSearch(paginationFilterDetailSearch);
            var userSearch = _mapper.Map<User>(paginationFilterDetailSearch.Search);
            var users = await _unitOfWork.UserRepository.SearchDetail(userSearch);
            var usersModel = _mapper.Map<IEnumerable<UserModel>>(users);
            var totalRecords = usersModel.Count();
            var pagedData = usersModel
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            var result = new GetAllWithPaging<UserModel, PaginationFilterDetailSearch>(validFilter, pagedData, totalRecords);
            return result;
        }

        private void UploadedFile(Byte[] file, IFormFile formFile, out string fileName, out string filePath)
        {
            string uniqueFileName = null;
            var folderName = "images";
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, folderName);
            if (file != null)
            {
                
                uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                string filePathes = Path.Combine(uploadsFolder, uniqueFileName);
                File.WriteAllBytes(filePathes, file);

                fileName = uniqueFileName;
                filePath = folderName + "/"+ uniqueFileName;
            }
            else
            {
                
                uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePathes = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePathes, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }

                fileName = uniqueFileName;
                filePath = folderName + "/" + uniqueFileName;
            }
        }
        
    }
}
