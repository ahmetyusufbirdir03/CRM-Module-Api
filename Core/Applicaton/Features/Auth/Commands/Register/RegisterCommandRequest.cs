using Applicaton.DTOs;
using MediatR;

namespace Applicaton.Features.Auth.Commands.Register;

public class RegisterCommandRequest : IRequest<ResponseDto<NoContentDto>>
{        
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }        
}
