using Applicaton.DTOs;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Meetings.Commands.DeleteMeetingById;

public class DeleteMeetingByIdCommandHandler :
    BaseHandler,
    IRequestHandler<DeleteMeetingByIdCommandRequest, ResponseDto<NoContentDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public DeleteMeetingByIdCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        ErrorMessageService errorMessageService) : 
        base(httpContextAccessor, mapper, unitOfWork, errorMessageService)
    {
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
    }

    public async Task<ResponseDto<NoContentDto>> Handle(DeleteMeetingByIdCommandRequest request, CancellationToken cancellationToken)
    {
        var meetings = await unitOfWork.GetGenericRepository<Meeting>().GetAllAsync(x => x.Id == request.Id && x.DeletedBy == null);

        if (!meetings.Any())
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.MeetingNotFound);

        var meeting = meetings.FirstOrDefault();

        await unitOfWork.GetGenericRepository<Meeting>().SoftDeleteAsync(meeting);

        return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);
    }
}