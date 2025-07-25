using Applicaton.DTOs;
using Applicaton.DTOs.Customer;
using MediatR;

namespace Applicaton.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryRequest : IRequest<ResponseDto<IList<CustomerResponseDto>>>
{
}
