using Applicaton.DTOs;
using Applicaton.DTOs.Auth;
using Applicaton.Interfaces.Tokens;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Applicaton.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, ResponseDto<RefreshTokenResponseDto>>
{
    private readonly UserManager<AppUser> userManager;
    private readonly ITokenService tokenService;
    private readonly ErrorMessageService errorMessageService;

    public RefreshTokenCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        ErrorMessageService errorMessageService,
        ITokenService tokenService,
        UserManager<AppUser> userManager) : base(httpContextAccessor, mapper, unitOfWork, errorMessageService)
    {
        this.errorMessageService = errorMessageService;
        this.tokenService = tokenService;
        this.userManager = userManager;
    }

    public async Task<ResponseDto<RefreshTokenResponseDto>> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal? principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        string email = principal.FindFirstValue(ClaimTypes.Email);

        AppUser? user = await userManager.FindByEmailAsync(email);
        IList<string> roles = await userManager.GetRolesAsync(user);

        if(user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return ResponseDto<RefreshTokenResponseDto>.Fail(StatusCodes.Status401Unauthorized, errorMessageService.SessionExpired);

        JwtSecurityToken newAccessToken = await tokenService.CreateToken(user, roles);
        string newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await userManager.UpdateAsync(user);


        return ResponseDto<RefreshTokenResponseDto>.Success(StatusCodes.Status201Created,
            new RefreshTokenResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            });
    }
}
