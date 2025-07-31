using Applicaton.DTOs;
using Applicaton.DTOs.Meeting;
using MediatR;

namespace Applicaton.Features.Meetings.Queries.GetAllMeetings;

public class GetAllMeetingsQueryRequest : IRequest<ResponseDto<IList<MeetingResponseDto>>>
{
}
