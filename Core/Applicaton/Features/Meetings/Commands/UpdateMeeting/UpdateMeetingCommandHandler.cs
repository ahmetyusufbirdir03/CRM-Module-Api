using Applicaton.DTOs;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Meetings.Commands.UpdateMeeting;

public class UpdateMeetingCommandHandler : BaseHandler, IRequestHandler<UpdateMeetingCommandRequest, ResponseDto<NoContentDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;
    private readonly MeetingRules meetingRules;

    public UpdateMeetingCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        ErrorMessageService errorMessageService,
        MeetingRules meetingRules) : 
        base(httpContextAccessor, mapper, unitOfWork, errorMessageService)
    {
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
        this.meetingRules = meetingRules;
    }

    public async Task<ResponseDto<NoContentDto>> Handle(UpdateMeetingCommandRequest request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork
            .GetGenericRepository<Customer>()
            .GetByIdAsync(request.CustomerId);

        if (customer == null || customer.DeletedBy != null)
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.CustomerNotFound);

        var user = await unitOfWork.GetGenericRepository<AppUser>().GetByIdAsync(request.UserId);
        if (user == null)
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.UserNotFound);

        var meeting = await unitOfWork
            .GetGenericRepository<Meeting>()
            .GetByIdAsync(request.Id);

        if (meeting == null)
            return ResponseDto<NoContentDto>
                .Fail(StatusCodes.Status404NotFound, errorMessageService.MeetingNotFound);

        meeting.StartDate = request.StartDate;
        meeting.EndDate = request.EndDate;  
        meeting.Description = request.Description;
        meeting.CustomerId = request.CustomerId;
        meeting.UserId = request.UserId;
        meeting.TypeId = request.TypeId;
        meeting.FormatId = request.FormatId;
        meeting.StateId = request.StateId;

        var isConflict = await meetingRules.IsMeetingConflictsAsync(meeting);
        if (isConflict)
            return ResponseDto<NoContentDto>.Fail(StatusCodes.Status409Conflict, errorMessageService.MeetingTimeConflict);


        await unitOfWork.GetGenericRepository<Meeting>().UpdateAsync(meeting);

        return ResponseDto<NoContentDto>.Success(StatusCodes.Status200OK);

    }
}
