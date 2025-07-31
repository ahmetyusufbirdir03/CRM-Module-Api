using Applicaton.DTOs;
using Applicaton.DTOs.Meeting;
using Applicaton.Interfaces.UnitOfWorks;
using AutoMapper;
using CrmModuleApi.Shared.Bases;
using CrmModuleApi.Shared.ErrorMessages;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace Applicaton.Features.Meetings.Queries.GetMeetingsByCustomerPhoneNumber;

public class GetMeetingsByCustomerPhoneNumberOrEmailQueryHandler :
    BaseHandler,
    IRequestHandler<GetMeetingsByCustomerPhoneNumberOrEmailQueryRequest, ResponseDto<IList<MeetingResponseDto>>>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly ErrorMessageService errorMessageService;

    public GetMeetingsByCustomerPhoneNumberOrEmailQueryHandler(
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

    private bool IsEmail(string input)
    {
        return Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public async Task<ResponseDto<IList<MeetingResponseDto>>> Handle(GetMeetingsByCustomerPhoneNumberOrEmailQueryRequest request, CancellationToken cancellationToken)
    {
        var customer = IsEmail(request.PhoneNumberOrEmail)
            ? await unitOfWork.CustomerRepository.GetByEmailAsync(request.PhoneNumberOrEmail)
            : await unitOfWork.CustomerRepository.GetByPhoneNumberAsync(request.PhoneNumberOrEmail);

        if (customer == null || customer.DeletedBy != null)
            return ResponseDto<IList<MeetingResponseDto>>
                .Fail(StatusCodes.Status404NotFound, errorMessageService.CustomerNotFound);
        
        var meetings = await unitOfWork.GetGenericRepository<Meeting>().GetAllAsync(x => x.CustomerId == customer.Id && x.DeletedBy == null);

        if(!meetings.Any())
            return ResponseDto<IList<MeetingResponseDto>>
                .Fail(StatusCodes.Status404NotFound, errorMessageService.MeetingsNotFound);


        IList<MeetingResponseDto> _meetings = mapper.Map<IList<MeetingResponseDto>>(meetings);

        return ResponseDto<IList<MeetingResponseDto>>.Success(StatusCodes.Status200OK, _meetings);
    }
}

