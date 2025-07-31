using Applicaton.DTOs;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Applicaton.Features.User.Commands.DeleteUser;

public class DeleteUserByIdCommandRequest : IRequest<ResponseDto<NoContentDto>>
{
    public int Id { get; set; }
}

public class DeleteUserByIdCommandHandler :
    BaseHandler, IRequestHandler<DeleteUserByIdCommandRequest, ResponseDto<NoContentDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public DeleteUserByIdCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        ErrorMessageService errorMessageService) : 
        base(httpContextAccessor, mapper, unitOfWork, errorMessageService)
    {
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
    }

    public async Task<ResponseDto<NoContentDto>> Handle(DeleteUserByIdCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.GetGenericRepository<AppUser>().GetByIdAsync(request.Id);
        if (user == null)
            return ResponseDto<NoContentDto>
                .Fail(StatusCodes.Status404NotFound, errorMessageService.UserNotFound);

        await unitOfWork.GetGenericRepository<AppUser>().DeleteAsync(user);

        return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
    }
}
