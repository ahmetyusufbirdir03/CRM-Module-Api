using Applicaton.DTOs;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerByIdCommandHandler : IRequestHandler<UpdateCustomerByIdCommandRequest, ResponseDto<NoContentDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;
    private readonly IMapper mapper;

    public UpdateCustomerByIdCommandHandler(IUnitOfWork unitOfWork, ErrorMessageService errorMessageService, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
        this.mapper = mapper;
    }
    public async Task<ResponseDto<NoContentDto>> Handle(UpdateCustomerByIdCommandRequest request, CancellationToken cancellationToken)
    {
        var customers = await unitOfWork.GetGenericRepository<Customer>().GetAllAsync(x => x.Id == request.Id && x.DeletedBy == null);
        if (!customers.Any())
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.CustomerNotFound);

        bool isPhoneExists = await unitOfWork.GetGenericRepository<Customer>().AnyAsync(x =>
            x.PhoneNumber == request.PhoneNumber && x.Id != request.Id && x.DeletedBy == null);
        if (isPhoneExists)
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status409Conflict, errorMessageService.PhoneNumberAlreadyExist);

        bool isEmailExists = await unitOfWork.GetGenericRepository<Customer>().AnyAsync(x =>
            x.Email == request.Email && x.Id != request.Id && x.DeletedBy == null);
        if (isEmailExists)
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status409Conflict, errorMessageService.EmailAlreadyExist);

        var customer = customers.First();

        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;   
        customer.Email = request.Email; 
        customer.PhoneNumber = request.PhoneNumber;
        customer.Address = request.Address;
        customer.City = request.City;
        customer.Country = request.Country;
        customer.TypeId = request.TypeId;
        customer.StateId = request.StateId;
        customer.UpdatedDate = DateTime.UtcNow;
        customer.UpdatedBy = "Admin";
        
        await unitOfWork.GetGenericRepository<Customer>().UpdateAsync(customer);

        return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
    }
}
