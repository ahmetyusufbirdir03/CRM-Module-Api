using Applicaton.DTOs.Meeting;
using Applicaton.Features.Meetings.Commands.CreateMeeting;
using AutoMapper;
using Domain.Entities;

namespace Applicaton.Features.Meetings.Profiles;

public class MeetingMappingProfile : Profile
{
    public MeetingMappingProfile()
    {
        CreateMap<CreateMeetingCommandRequest, Meeting>();
        CreateMap<Meeting, MeetingResponseDto>();
    }
}
