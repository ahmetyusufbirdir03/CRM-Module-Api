﻿using Applicaton.DTOs;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Customers.Commands.DeleteCustomerById;

public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteCustomerByIdCommandRequest, ResponseDto<NoContentDto>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public DeleteCustomerByIdCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ErrorMessageService errorMessageService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
    }
    public async Task<ResponseDto<NoContentDto>> Handle(DeleteCustomerByIdCommandRequest request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.GetGenericRepository<Customer>().GetByIdAsync(request.Id);
        if (customer is null)
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.CustomerNotFound);

        await unitOfWork.GetGenericRepository<Customer>().DeleteAsync(customer);
        await unitOfWork.SaveChangesAsync();

        return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);

    }
}


