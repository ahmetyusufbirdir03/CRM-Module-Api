using Applicaton.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Applicaton.Features.User.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<AppUser, UserResponseDto>();
    }
}
