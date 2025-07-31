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
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;

namespace Applicaton.Features.Auth.Commands.Login;

public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, ResponseDto<LoginResponseDto>>
{
    private readonly UserManager<AppUser> userManager;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;
    private readonly ITokenService tokenService;
    private readonly IConfiguration configuration;

    public LoginCommandHandler(UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IUnitOfWork unitOfWork,
        ErrorMessageService errorMessageService,
        ITokenService tokenService,
        IConfiguration configuration) : base(httpContextAccessor, mapper, unitOfWork, errorMessageService)
    {
        this.userManager = userManager;
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
        this.tokenService = tokenService;
        this.configuration = configuration;
    }

    private bool IsEmail(string input)
    {
        return Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public async Task<ResponseDto<LoginResponseDto>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser user;
        if(IsEmail(request.PhoneNumberOrEmail))
            user = await userManager.FindByEmailAsync(request.PhoneNumberOrEmail);
        else
            user = await unitOfWork.UserRepository.GetUserByPhoneNumberAsync(request.PhoneNumberOrEmail);

        bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);

        if(user == null || !checkPassword) 
            return ResponseDto<LoginResponseDto>.Fail(StatusCodes.Status401Unauthorized, errorMessageService.InvalidAuthenticationInformations);

        IList<string> roles = await userManager.GetRolesAsync(user);

        JwtSecurityToken token = await tokenService.CreateToken(user, roles);
        string refreshToken = tokenService.GenerateRefreshToken();

        _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays); 

        await userManager.UpdateAsync(user);
        await userManager.UpdateSecurityStampAsync(user);

        string _token = new JwtSecurityTokenHandler().WriteToken(token);

        await userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

        return ResponseDto<LoginResponseDto>.Success(
            StatusCodes.Status200OK, 
            new LoginResponseDto
            {
                Token = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
    }
}
