using Applicaton.DTOs;
using Applicaton.DTOs.Customer;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.ErrorMessages;
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
        var customer = await unitOfWork.CustomerRepository.GetByPhoneNumberAsync(request.PhoneNumber);
        if (customer is null)
            return ResponseDto<CustomerResponseDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.CustomerNotFound);

        var response = mapper.Map<CustomerResponseDto>(customer);

        return ResponseDto<CustomerResponseDto>.Success(StatusCodes.Status200OK, response);
    }
}


