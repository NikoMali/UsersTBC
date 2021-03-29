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

namespace UsersTBC.Application.Services.Repository
{
    public class UserService : IUserService 
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<UserMobileNumber> _userMobileRepository;
        private readonly IRepository<UserImage> _userImageRepository;
        private readonly IRepository<UseRelated> _userRelatedRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository,
                            IRepository<UserMobileNumber> userMobileRepository,
                            IRepository<UserImage> userImageRepository,
                            IRepository<UseRelated> userRelatedRepository,
                            IHostingEnvironment hostEnvironment,
                            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _userMobileRepository = userMobileRepository;
            _userImageRepository = userImageRepository;
            _userRelatedRepository = userRelatedRepository;
            _hostingEnvironment = hostEnvironment;
            _mapper = mapper;
        }

        

        

        public async Task<string> Create(UserRequestModel userRequestModel)
        {
            var user = _mapper.Map<User>(userRequestModel);
            var mobileNumber = _mapper.Map<List<UserMobileNumber>>(userRequestModel.UserMobileNumbers);
            var images = _mapper.Map<List<UserImage>>(userRequestModel.Images);
            var userRelateds = _mapper.Map<List<UseRelated>>(userRequestModel.userRelateds);

            await _userRepository.AddAsync(user);

            if (mobileNumber.Count > 0)
            {
                mobileNumber.ForEach(x => x.AssignedUserId(user.Id));
                mobileNumber.ForEach(x => _userMobileRepository.AddAsync(x).Wait());
            }

           
            await UserSaveImages(userRequestModel.Images, user.Id);

            if (userRelateds.Count > 0)
            {
                userRelateds.ForEach(x => x.AssignedUserId(user.Id));
                userRelateds.ForEach(x => _userRelatedRepository.AddAsync(x).Wait());
            }

            
            return ResultStatus.SUCCESS;
        }

        public async Task<string> UpdateUser(UserUpdateRequestModel userUpdateRequestModel)
        {
            //try
            //{
                var user = _mapper.Map<User>(userUpdateRequestModel);
                var userMobile = _mapper.Map<List<UserMobileNumber>>(userUpdateRequestModel.UserMobileNumbers);
                await _userRepository.UpdateAsync(user);
                await _userMobileRepository.UpdateRangeAsync(userMobile);
                return ResultStatus.SUCCESS;
            //}
            //catch (Exception)
            //{

            //return "FAILED";
            //}
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
                await _userImageRepository.AddAsync(userImage);
                return ResultStatus.SUCCESS;
            }
            else
            {
                userImage.Id = userImageRequestModel.Id;
                UploadedFile(null, userImageRequestModel.file, out fileName, out filePath);
                userImage.DocumentName = fileName;
                userImage.DocumentPath = filePath;
                await _userImageRepository.UpdateAsync(userImage);
                return ResultStatus.SUCCESS;
            }
           
            
        }
        public async Task<string> AddOrUpdateUserRelated(UserRelatedRequestModel userRelatedRequestModel)
        {
            
            var userRelated = _mapper.Map<UseRelated>(userRelatedRequestModel);
            if (userRelated.Id == 0)
            {
                await _userRelatedRepository.AddAsync(userRelated);
                return ResultStatus.SUCCESS;
            }
            else
            {
                await _userRelatedRepository.UpdateAsync(userRelated);
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
                    await _userImageRepository.AddAsync(userImage);
                }
            }
            
        }

        public async Task<string> RemoveUser(int userId)
        {
           
            await _userImageRepository.RemoveRangeAsync(await _userImageRepository.FindAll(x => x.UserId == userId));
            await _userMobileRepository.RemoveRangeAsync(await _userMobileRepository.FindAll(x => x.UserId == userId));
            await _userRelatedRepository.RemoveRangeAsync(await _userRelatedRepository.FindAll(x => x.UserId == userId));
            await _userRepository.DeleteAsync(await _userRepository.First(x => x.Id == userId));
            return ResultStatus.SUCCESS;


        }
        public async Task<UserResponseModel> GetUser(int userId)
        {
            var user = await _userRepository.GetUser(userId);
            var userModel = _mapper.Map<UserResponseModel>(user);
            userModel.UserMobileNumbers = _mapper.Map<List<UserMobileNumberModel>>(await _userMobileRepository.FindAll(x => x.UserId == userId));
            userModel.Images = _mapper.Map<List<UserImagesResponseModel>>(await _userImageRepository.FindAll(x => x.UserId == userId));
            userModel.userRelateds = _mapper.Map<List<UserRelatedResponseModel>>(await _userRepository.GetRelatedUsersByUserId(userId));

            return userModel;
        }


        public async Task<List<UserResponseModel>> UsersWithRelatedPersons(RelatedType relatedTypeId)
        {
            var usersModel = new List<UserResponseModel>();
            var users = await _userRepository.UsersWithRelatedPersons(relatedTypeId);

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
            var users =await _userRepository.SearchQuick(searchString);
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
            var users = await _userRepository.SearchDetail(userSearch);
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
