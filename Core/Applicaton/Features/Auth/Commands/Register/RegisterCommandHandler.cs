using Applicaton.DTOs;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Applicaton.Features.Auth.Commands.Register;

public class RegisterCommandHandler : BaseHandler,IRequestHandler<RegisterCommandRequest, ResponseDto<NoContentDto>>
{

    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<AppRole> roleManager;
    private readonly ErrorMessageService errorMessageService;

    public RegisterCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        ErrorMessageService errorMessageService) : base(httpContextAccessor, mapper, unitOfWork, errorMessageService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.errorMessageService = errorMessageService;
    }

    public async Task<ResponseDto<NoContentDto>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var emailExist = await userManager.FindByEmailAsync(request.Email);
        if (emailExist is not null)
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status409Conflict, errorMessageService.EmailAlreadyExist);

        var phoneNumberExist = await unitOfWork.UserRepository.GetUserByPhoneNumberAsync(request.PhoneNumber);
        if (phoneNumberExist is not null)
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status409Conflict, errorMessageService.PhoneNumberAlreadyExist);

        AppUser user = mapper.Map<AppUser>(request);

        user.UserName = request.Email.Trim().ToLowerInvariant();
        user.SecurityStamp = Guid.NewGuid().ToString();

        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            if (!await roleManager.RoleExistsAsync("user"))
                await roleManager.CreateAsync(new AppRole
                {
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                });

            await userManager.AddToRoleAsync(user, "user");
        }

        return ResponseDto<NoContentDto>.Success(StatusCodes.Status201Created);
    }
}
