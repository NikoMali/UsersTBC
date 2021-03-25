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

namespace UsersTBC.Application.Services.Repository
{
    public class UserService : IUserService 
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserMobileNumber> _userMobileRepository;
        private readonly IRepository<UserImage> _userImageRepository;
        private readonly IRepository<UseRelated> _userRelatedRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IMapper _mapper;

        public UserService(IRepository<User> userRepository,
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

        

        public async Task<IEnumerable<UserResponseModel>> GetAll()
        {
            var model = await _userRepository.GetAll();
            return _mapper.Map<List<UserResponseModel>>(model);
        }

        public async Task<UserResponseModel> Create(UserRequestModel userRequestModel)
        {
            var user = _mapper.Map<User>(userRequestModel);
            var mobileNumber = _mapper.Map<List<UserMobileNumber>>(userRequestModel.UserMobileNumbers);
            var images = _mapper.Map<List<UserImage>>(userRequestModel.Images);
            var userRelateds = _mapper.Map<List<UseRelated>>(userRequestModel.userRelateds);

            await _userRepository.Add(user);

            if (mobileNumber.Count > 0)
            {
                mobileNumber.ForEach(x => x.AssignedUserId(user.Id));
                mobileNumber.ForEach(x => _userMobileRepository.Add(x).Wait());
            }

            /*if (images.Count > 0)
            {
                images.ForEach(x => x.AssignedUserId(user.Id));
                images.ForEach(x => _userImageRepository.Add(x).Wait());
            }*/
            await UserSaveImages(userRequestModel.Images, user.Id);

            if (userRelateds.Count > 0)
            {
                userRelateds.ForEach(x => x.AssignedUserId(user.Id));
                userRelateds.ForEach(x => _userRelatedRepository.Add(x).Wait());
            }

            var model = await _userRepository.GetAll();
            return _mapper.Map<UserResponseModel>(model[0]);
        }
        private async Task UserSaveImages(List<UserImageModel> userImages, int userId)
        {
            var userImage = new UserImage(); 
            if (userImages.Count>0)
            {
                for (int i = 0; i < userImages.Count; i++)
                {
                    Byte[] bytes = Convert.FromBase64String(userImages[i].Image);
                    //IFormFile formFile = File.WriteAllBytes(path, bytes);
                    userImage.DocumentPath = await UploadedFile(bytes);
                    userImage.DocumentName = userImages[i].DocumentName;
                    userImage.UserId = userId;
                    await _userImageRepository.Add(userImage);
                }
            }
            
        }

        private Task<string> UploadedFile(Byte[] file)
        {

            string uniqueFileName = null;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                File.WriteAllBytes(filePath, file);
                /*using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }*/
            }
            return Task.FromResult(uniqueFileName);
        }
        /* private  void Add<T>(List<T> list, IRepository<T> rep) where T : class
         {
             if (list.Count > 0)
             {
                 //list.ForEach(x => x.AssignedUserId(user.Id));
                 for (int i = 0; i < list.Count; i++)
                 {
                     list[i].
                 }
                 list.ForEach(x => rep.Add(x));
             }
         }*/
    }
}
