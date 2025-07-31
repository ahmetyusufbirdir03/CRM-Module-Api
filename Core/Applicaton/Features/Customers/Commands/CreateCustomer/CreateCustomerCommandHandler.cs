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
        var customers = await unitOfWork
            .GetGenericRepository<Customer>()
            .GetAllAsync(c => (c.PhoneNumber == request.PhoneNumber || 
            c.Email == request.Email) && c.DeletedBy == null);


        if (customers.Any())
            return ResponseDto<CustomerResponseDto>.Fail(StatusCodes.Status409Conflict, errorMessageService.CustomerAlreadyExist);
        
        var customer = mapper.Map<Customer>(request);

        await unitOfWork.GetGenericRepository<Customer>().CreateAsync(customer);

        var response = mapper.Map<CustomerResponseDto>(customer);

        return ResponseDto<CustomerResponseDto>.Success(StatusCodes.Status200OK, response);
    }
}

