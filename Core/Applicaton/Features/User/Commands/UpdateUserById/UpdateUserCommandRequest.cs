using Applicaton.DTOs;
using MediatR;

namespace Applicaton.Features.User.Commands.UpdateUserById;

public class UpdateUserCommandRequest : IRequest<ResponseDto<NoContentDto>>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Department { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }
}
