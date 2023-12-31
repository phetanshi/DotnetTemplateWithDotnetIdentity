﻿using Application.Dtos;
using AutoMapper;
using DotnetTemplateWithDotnetIdentity.Data.Models;

namespace DotnetTemplateWithDotnetIdentity.Api.AutoMapperProfiles
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            //Below Comment is for example only

            //CreateMap<EmployeeCreateDto, Employee>()
            //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
