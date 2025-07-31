using Applicaton.DTOs;
using Applicaton.DTOs.Auth;
using MediatR;

namespace Applicaton.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandRequest : IRequest<ResponseDto<RefreshTokenResponseDto>>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
