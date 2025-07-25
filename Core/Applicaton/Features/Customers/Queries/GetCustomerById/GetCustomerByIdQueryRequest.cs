using Applicaton.DTOs;
using Applicaton.DTOs.Customer;
using MediatR;

namespace Applicaton.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryRequest : IRequest<ResponseDto<CustomerResponseDto>>
{
    public int Id { get; set; }
}
