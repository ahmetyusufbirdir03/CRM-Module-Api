using Applicaton.DTOs;
using Applicaton.DTOs.Auth;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace Applicaton.Features.Auth.Commands.Revoke;

public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommandRequest, ResponseDto<NoContentDto>>
{
    private readonly UserManager<AppUser> userManager;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public RevokeCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        ErrorMessageService errorMessageService,
        UserManager<AppUser> userManager) : base(httpContextAccessor, mapper, unitOfWork, errorMessageService)
    {
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
        this.userManager = userManager;
    }

    private bool IsEmail(string input)
    {
        return Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public async Task<ResponseDto<NoContentDto>> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser user;
        if (IsEmail(request.PhoneNumberOrEmail))
            user = await userManager.FindByEmailAsync(request.PhoneNumberOrEmail);
        else
            user = await unitOfWork.UserRepository.GetUserByPhoneNumberAsync(request.PhoneNumberOrEmail);

        if (user == null )
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status401Unauthorized, errorMessageService.InvalidCredentials);

        user.RefreshToken = null;
        await userManager.UpdateAsync(user);

        return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
        
    }
}
