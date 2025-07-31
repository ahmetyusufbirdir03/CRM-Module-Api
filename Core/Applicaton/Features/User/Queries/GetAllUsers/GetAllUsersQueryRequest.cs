using Applicaton.DTOs;
using Applicaton.DTOs.User;
using Applicaton.Interfaces.Repositories;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.User.Queries.GetAllUsers;

public class GetAllUsersQueryRequest : IRequest<ResponseDto<IList<UserResponseDto>>>
{
}

public class GetAllUsersQueryHandler : BaseHandler, IRequestHandler<GetAllUsersQueryRequest, ResponseDto<IList<UserResponseDto>>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public GetAllUsersQueryHandler(
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IUnitOfWork unitOfWork,
        ErrorMessageService errorMessageService) : 
        base(httpContextAccessor, mapper, unitOfWork, errorMessageService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
    }

    public async Task<ResponseDto<IList<UserResponseDto>>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.GetGenericRepository<AppUser>().GetAllAsync();
        if (!users.Any())
            return ResponseDto<IList<UserResponseDto>>.Fail(StatusCodes.Status404NotFound, errorMessageService.UserNotFound);

        IList<UserResponseDto> _users = mapper.Map<IList<UserResponseDto>>(users);

        return ResponseDto<IList<UserResponseDto>>.Success(StatusCodes.Status200OK, _users);
    }
}