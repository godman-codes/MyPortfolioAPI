using AutoMapper;
using Entities.Models;
using Shared.DTOs;

namespace MyPortfolioAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, UserModel>();
        }

    }
}
