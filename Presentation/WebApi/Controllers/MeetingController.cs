using Applicaton.Features.Customers.Queries.GetCustomerByPhoneNumber;
using Applicaton.Features.Meetings.Commands.CreateMeeting;
using Applicaton.Features.Meetings.Commands.DeleteMeetingById;
using Applicaton.Features.Meetings.Commands.UpdateMeeting;
using Applicaton.Features.Meetings.Queries.GetAllMeetings;
using Applicaton.Features.Meetings.Queries.GetMeetingsByCustomerPhoneNumber;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstraction;

namespace WebApi.Controllers;

[Authorize]
public class MeetingController : ApiController
{
    private readonly IMediator mediator;

    public MeetingController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMeeting(CreateMeetingCommandRequest request)
    {
        var response = await mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMeetings()
    {
        var response = await mediator.Send(new GetAllMeetingsQueryRequest());

        return StatusCode(response.StatusCode, response);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateMeeting(UpdateMeetingCommandRequest request)
    {
        var response = await mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeeting(int id)
    {
        var response = await mediator.Send(new DeleteMeetingByIdCommandRequest { Id = id });

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetMeetingsByCustomerPhoneNumberOrEmail(GetMeetingsByCustomerPhoneNumberOrEmailQueryRequest request)
    {
        var response = await mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }
}
