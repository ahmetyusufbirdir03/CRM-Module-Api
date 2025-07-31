using Applicaton.DTOs.Customer;
using Applicaton.Features.Customers.Commands.CreateCustomer;
using Applicaton.Features.Customers.Queries.GetAllCustomers;
using Applicaton.Features.Customers.Queries.GetCustomerById;
using AutoMapper;
using Domain.Entities;

namespace Applicaton.Features.Customers.Profiles;

public class CustomersMappingProfile : Profile
{
    public CustomersMappingProfile()
    {
        CreateMap<Customer, CustomerResponseDto>();
        CreateMap<CreateCustomerCommandRequest, Customer>();
    }
}
