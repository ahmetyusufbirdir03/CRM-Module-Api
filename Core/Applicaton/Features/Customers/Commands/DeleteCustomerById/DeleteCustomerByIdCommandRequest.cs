using Applicaton.DTOs;
using MediatR;

namespace Applicaton.Features.Customers.Commands.DeleteCustomerById;

public class DeleteCustomerByIdCommandRequest : IRequest<ResponseDto<NoContentDto>>
{
    public int Id { get; set; }
}


