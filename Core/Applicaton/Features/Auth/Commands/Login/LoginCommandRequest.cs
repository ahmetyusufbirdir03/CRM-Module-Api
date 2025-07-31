using Applicaton.DTOs;
using Applicaton.DTOs.Auth;
using MediatR;
using System.ComponentModel;

namespace Applicaton.Features.Auth.Commands.Login;

public class LoginCommandRequest : IRequest<ResponseDto<LoginResponseDto>>
{
    [DefaultValue("admin@mail.com")]
    public string PhoneNumberOrEmail { get; set; }

    [DefaultValue("123456")]
    public string Password { get; set; }
}
