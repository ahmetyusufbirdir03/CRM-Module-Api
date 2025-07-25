using Applicaton.DTOs;
using Applicaton.DTOs.Customer;
using MediatR;

namespace Applicaton.Features.Customers.Queries.GetCustomerByPhoneNumber;

public class GetCustomerByPhoneNumberQueryRequest : IRequest<ResponseDto<CustomerResponseDto>>
{
    public string PhoneNumber { get; set; }
}


