using Applicaton.DTOs;
using Applicaton.DTOs.Meeting;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Applicaton.Features.Meetings.Queries.GetMeetingsByCustomerPhoneNumber;

public class GetMeetingsByCustomerPhoneNumberOrEmailQueryRequest : IRequest<ResponseDto<IList<MeetingResponseDto>>>
{
    public string PhoneNumberOrEmail { get; set; }
}

