using Applicaton.DTOs;
using Applicaton.DTOs.Customer;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQueryRequest, ResponseDto<IList<CustomerResponseDto>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ErrorMessageService errorMessageService;

    public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ErrorMessageService errorMessageService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.errorMessageService = errorMessageService;
    }

    public async Task<ResponseDto<IList<CustomerResponseDto>>> Handle(GetAllCustomersQueryRequest request, CancellationToken cancellationToken)
    {
        List<Customer> customers = await unitOfWork.GetGenericRepository<Customer>().GetAllAsync(x => x.DeletedBy == null);

        if (customers is null)
            return ResponseDto<IList<CustomerResponseDto>>.Fail(StatusCodes.Status404NotFound, errorMessageService.CustomersNotFound);

        IList<CustomerResponseDto> response = mapper.Map<IList<CustomerResponseDto>>(customers);

        return ResponseDto<IList<CustomerResponseDto>>.Success(StatusCodes.Status200OK, response);
    }
}
