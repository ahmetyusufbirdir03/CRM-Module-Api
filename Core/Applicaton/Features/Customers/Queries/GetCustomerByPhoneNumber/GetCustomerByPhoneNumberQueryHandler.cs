using Applicaton.DTOs;
using Applicaton.DTOs.Customer;
using Applicaton.Features.Customers.Rules;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Customers.Queries.GetCustomerByPhoneNumber;

public class GetCustomerByPhoneNumberQueryHandler : IRequestHandler<GetCustomerByPhoneNumberQueryRequest, ResponseDto<CustomerResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public GetCustomerByPhoneNumberQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ErrorMessageService errorMessageService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
    }

    public async Task<ResponseDto<CustomerResponseDto>> Handle(GetCustomerByPhoneNumberQueryRequest request, CancellationToken cancellationToken)
    {
        var customers = await unitOfWork.GetGenericRepository<Customer>().GetAllAsync(c => c.PhoneNumber == request.PhoneNumber && c.DeletedBy == null);

        if (!customers.Any())
            return ResponseDto<CustomerResponseDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.CustomerNotFound);

        var customer = customers.FirstOrDefault();

        var response = mapper.Map<CustomerResponseDto>(customer);

        return ResponseDto<CustomerResponseDto>.Success(StatusCodes.Status200OK, response);
    }
}


