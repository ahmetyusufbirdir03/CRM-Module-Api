using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.ErrorMessages;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CrmModuleApi.Shared.Bases;

public class BaseHandler
{
    public readonly int userId;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public BaseHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper, IUnitOfWork unitOfWork, ErrorMessageService errorMessageService)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
        //userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
