using AutoMapper;
using UsersTBC.Application.Models;
using UsersTBC.Domain.Entities;

namespace ProductTermsControl.WebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRequestModel>();
            CreateMap<UserRequestModel, User>();
            CreateMap<UserResponseModel, User>();
            CreateMap<User, UserResponseModel>();
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
            CreateMap<UserUpdateRequestModel, User>();
            CreateMap<User, UserUpdateRequestModel>();


            CreateMap<City, CityModel>();
            CreateMap<CityModel, City>();

            CreateMap<UserImage, UserImageModel>();
            CreateMap<UserImageModel, UserImage>();
            CreateMap<UserImage, UserImageRequestModel>();
            CreateMap<UserImageRequestModel, UserImage>();
            CreateMap<UserImage, UserImagesResponseModel>();
            CreateMap<UserImagesResponseModel, UserImage>();

            CreateMap<UserMobileNumber, UserMobileNumberModel>();
            CreateMap<UserMobileNumberModel, UserMobileNumber>();
            CreateMap<UserMobileNumber, UserMobileNumberRequestModel>();
            CreateMap<UserMobileNumberRequestModel, UserMobileNumber>();


            CreateMap<UseRelated, UserRelatedModel>();
            CreateMap<UserRelatedModel, UseRelated>();
            CreateMap<UseRelated, UserRelatedRequestModel>();
            CreateMap<UserRelatedRequestModel, UseRelated>();
            CreateMap<UseRelated, UserRelatedResponseModel>();
            CreateMap<UserRelatedResponseModel, UseRelated>();

        }
    }
}