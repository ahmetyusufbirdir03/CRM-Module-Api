using Applicaton.DTOs;
using Applicaton.DTOs.Meeting;
using MediatR;

namespace Applicaton.Features.Meetings.Commands.UpdateMeeting;

public class UpdateMeetingCommandRequest : IRequest<ResponseDto<NoContentDto>>
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TypeId { get; set; }
    public int FormatId { get; set; }
    public int StateId { get; set; }
    public string Description { get; set; }
}
