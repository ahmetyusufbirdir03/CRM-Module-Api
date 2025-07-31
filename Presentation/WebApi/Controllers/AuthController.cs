using Applicaton.Features.Auth.Commands.Login;
using Applicaton.Features.Auth.Commands.RefreshToken;
using Applicaton.Features.Auth.Commands.Register;
using Applicaton.Features.Auth.Commands.Revoke;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstraction;

namespace WebApi.Controllers;

public class AuthController : ApiController
{
    private readonly IMediator mediator;

    public AuthController(IMediator mediator) : base(mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommandRequest request)
    {
        var response = await mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommandRequest request)
    {
        var response = await mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
    {
        var response = await mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Revoke(RevokeCommandRequest request)
    {
        var response = await mediator.Send(request);

        return StatusCode(response.StatusCode, response);
    }

}
