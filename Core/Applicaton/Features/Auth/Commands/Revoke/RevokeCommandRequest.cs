using Applicaton.DTOs;
using MediatR;

namespace Applicaton.Features.Auth.Commands.Revoke;

public class RevokeCommandRequest : IRequest<ResponseDto<NoContentDto>>
{
    public string PhoneNumberOrEmail { get; set; }
}
