using Applicaton.DTOs;
using Applicaton.DTOs.Meeting;
using MediatR;

namespace Applicaton.Features.Meetings.Commands.CreateMeeting;

public class CreateMeetingCommandRequest : IRequest<ResponseDto<MeetingResponseDto>>
{
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    public int TypeId { get; set; }
    public int StateId { get; set; }
    public int FormatId { get; set; }
}
