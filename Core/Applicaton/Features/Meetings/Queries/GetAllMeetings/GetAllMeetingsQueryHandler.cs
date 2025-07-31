using Applicaton.DTOs;
using Applicaton.DTOs.Meeting;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Applicaton.Features.Meetings.Queries.GetAllMeetings;

public class GetAllMeetingsQueryHandler :
    BaseHandler,
    IRequestHandler<GetAllMeetingsQueryRequest, ResponseDto<IList<MeetingResponseDto>>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public GetAllMeetingsQueryHandler(
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

    public async Task<ResponseDto<IList<MeetingResponseDto>>> Handle(GetAllMeetingsQueryRequest request, CancellationToken cancellationToken)
    {
        List<Meeting> meetings = await unitOfWork
            .GetGenericRepository<Meeting>()
            .GetAllAsync(x => x.DeletedBy == null);

        if (!meetings.Any())
            return ResponseDto<IList<MeetingResponseDto>>
                .Fail(StatusCodes.Status404NotFound, errorMessageService.MeetingsNotFound);

        IList<MeetingResponseDto> _meetings = mapper.Map<IList<MeetingResponseDto>>(meetings);

        return ResponseDto<IList<MeetingResponseDto>>.Success(StatusCodes.Status200OK, _meetings);
    }
}