using Applicaton.DTOs;
using Applicaton.DTOs.Customer;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQueryRequest, ResponseDto<CustomerResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public GetCustomerByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ErrorMessageService errorMessageService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
    }
    public async Task<ResponseDto<CustomerResponseDto>> Handle(GetCustomerByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.GetGenericRepository<Customer>().GetByIdAsync(request.Id);
        if (customer is null)
            return ResponseDto<CustomerResponseDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.CustomerNotFound);
            
        var response = mapper.Map<CustomerResponseDto   >(customer);

        return ResponseDto<CustomerResponseDto>.Success(StatusCodes.Status200OK, response);
    }
}
