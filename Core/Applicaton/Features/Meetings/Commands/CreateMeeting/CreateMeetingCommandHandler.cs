using Applicaton.DTOs;
using Applicaton.DTOs.Meeting;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Meetings.Commands.CreateMeeting;

public class CreateMeetingCommandHandler : BaseHandler, IRequestHandler<CreateMeetingCommandRequest, ResponseDto<MeetingResponseDto>>
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;
    private readonly MeetingRules meetingRules;

    public CreateMeetingCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        ErrorMessageService errorMessageService,
        MeetingRules meetingRules) : 
        base(httpContextAccessor, mapper, unitOfWork, errorMessageService)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.errorMessageService = errorMessageService;
        this.meetingRules = meetingRules;
    }

    public async Task<ResponseDto<MeetingResponseDto>> Handle(CreateMeetingCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.GetGenericRepository<AppUser>().GetByIdAsync(request.UserId);
        if (user == null)
            return ResponseDto<MeetingResponseDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.UserNotFound);

        var customer = await unitOfWork
            .GetGenericRepository<Customer>()
            .GetByIdAsync(request.CustomerId);

        if (customer == null || customer.DeletedBy != null)
            return ResponseDto<MeetingResponseDto>.Fail(StatusCodes.Status404NotFound, errorMessageService.CustomerNotFound);

        var meeting = mapper.Map<Meeting>(request);

        var isConflict = await meetingRules.IsMeetingConflictsAsync(meeting);

        if (isConflict)
            return ResponseDto<MeetingResponseDto>.Fail(StatusCodes.Status409Conflict, errorMessageService.MeetingTimeConflict);

        await unitOfWork.GetGenericRepository<Meeting>().CreateAsync(meeting);

        var response = mapper.Map<MeetingResponseDto>(meeting);

        return ResponseDto<MeetingResponseDto>.Success(StatusCodes.Status201Created, response);
    }
}
