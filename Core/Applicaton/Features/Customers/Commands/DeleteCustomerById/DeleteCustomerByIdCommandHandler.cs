using Applicaton.DTOs;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Customers.Commands.DeleteCustomerById;

public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteCustomerByIdCommandRequest, ResponseDto<NoContentDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public DeleteCustomerByIdCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ErrorMessageService errorMessageService)
    {
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
    }
    public async Task<ResponseDto<NoContentDto>> Handle(DeleteCustomerByIdCommandRequest request, CancellationToken cancellationToken)
    {
        var customers = await unitOfWork.GetGenericRepository<Customer>().GetAllAsync(c => c.Id == request.Id && c.DeletedBy == null);
        if (!customers.Any())
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.CustomerNotFound);

        var customer = customers.FirstOrDefault();

        await unitOfWork.GetGenericRepository<Customer>().SoftDeleteAsync(customer);

        return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);

    }
}


