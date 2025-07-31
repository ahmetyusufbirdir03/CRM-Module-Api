using Applicaton.Features.User.Commands.DeleteUser;
using Applicaton.Features.User.Commands.UpdateUserById;
using Applicaton.Features.User.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstraction;

namespace WebApi.Controllers;

[Authorize]
public class UserController : ApiController
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await mediator.Send(new GetAllUsersQueryRequest());

        return StatusCode(response.StatusCode, response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        var response = await mediator.Send(new DeleteUserByIdCommandRequest { Id = id });

        return StatusCode(response.StatusCode, response);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUser(UpdateUserCommandRequest request)
    {
        var response = await mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }
}
