using Applicaton.DTOs;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.User.Commands.UpdateUserById;

public class UpdateUserCommandHandler :
    BaseHandler,
    IRequestHandler<UpdateUserCommandRequest, ResponseDto<NoContentDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public UpdateUserCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        ErrorMessageService errorMessageService) : 
        base(httpContextAccessor, mapper, unitOfWork, errorMessageService)
    {
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
    }

    public async Task<ResponseDto<NoContentDto>> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.GetGenericRepository<AppUser>().GetByIdAsync(request.Id);

        if (user == null)
            return ResponseDto<NoContentDto>
                .Fail(StatusCodes.Status404NotFound, errorMessageService.UserNotFound);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Address = request.Address;
        user.Title = request.Title;
        user.Department = request.Department;

        await unitOfWork.GetGenericRepository<AppUser>().UpdateAsync(user);

        return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
    }
}
