using Applicaton.Features.Auth.Commands.Register;
using AutoMapper;
using Domain.Entities;

namespace Applicaton.Features.Auth.Profiles;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<RegisterCommandRequest, AppUser>();
    }
}
