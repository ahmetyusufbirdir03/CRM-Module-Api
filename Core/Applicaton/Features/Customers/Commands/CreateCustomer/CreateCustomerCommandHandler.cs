using Applicaton.DTOs;
using Applicaton.DTOs.Customer;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommandRequest, ResponseDto<CustomerResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public CreateCustomerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ErrorMessageService errorMessageService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
    }
    public async Task<ResponseDto<CustomerResponseDto>> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.GetGenericRepository<Customer>().GetAllAsync(
            c => c.PhoneNumber == request.PhoneNumber || 
            c.Email == request.Email);
        if (customer.Any())
            return ResponseDto<CustomerResponseDto>.Fail(StatusCodes.Status409Conflict, errorMessageService.CustomerAlreadyExist);

        Customer _customer = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            City = request.City,
            Country = request.Country,
            Address = request.Address,
            LastContactDate = DateTime.UtcNow,
            TypeId = request.TypeId,
            StateId = request.StateId,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "Admin"
        };

        await unitOfWork.GetGenericRepository<Customer>().CreateAsync(_customer);
        await unitOfWork.SaveChangesAsync();

        var response = mapper.Map<CustomerResponseDto>(_customer);

        return ResponseDto<CustomerResponseDto>.Success(StatusCodes.Status200OK, response);
    }
}

