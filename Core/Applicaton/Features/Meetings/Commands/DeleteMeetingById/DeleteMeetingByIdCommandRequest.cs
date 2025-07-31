using Applicaton.DTOs;
using MediatR;

namespace Applicaton.Features.Meetings.Commands.DeleteMeetingById;

public class DeleteMeetingByIdCommandRequest : IRequest<ResponseDto<NoContentDto>>
{
    public int Id { get; set; }
}
