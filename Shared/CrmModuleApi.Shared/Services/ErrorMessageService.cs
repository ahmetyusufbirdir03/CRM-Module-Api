using CrmModuleApi.Shared.Bases;
using Microsoft.Extensions.Localization;

namespace CrmModuleApi.Shared.ErrorMessages;

public class ErrorMessageService
{
    private readonly IStringLocalizer<ErrorMessagesBase> _localizer;

    public ErrorMessageService(IStringLocalizer<ErrorMessagesBase> localizer)
    {
        _localizer = localizer;
    }

    // CUSTOMER MESSAGES
    public string CustomersNotFound => _localizer["CustomersNotFound"];
    public string CustomerNotFound => _localizer["CustomerNotFound"];
    public string CustomerAlreadyExist => _localizer["CustomerAlreadyExist"];
    public string CustomerIsDeleted => _localizer["CustomerIsDeleted"];

    // MEETING MESSAGES
    public string MeetingTimeConflict => _localizer["MeetingTimeConflict"];
    public string MeetingNotFound => _localizer["MeetingNotFound"];
    public string MeetingsNotFound => _localizer["MeetingsNotFound"];

    //TOKEN MESSAGES
    public string TokenNotFound => _localizer["TokenNotFound"];

    //EMAIL MESSAGE
    public string EmailAlreadyExist => _localizer["EmailAlreadyExist"];

    //PHONE NUMBER MESSAGE
    public string PhoneNumberAlreadyExist => _localizer["PhoneNumberAlreadyExist"];

    // USER MESSAGES
    public string UserNotFound => _localizer["UserNotFound"];
    public string InvalidAuthenticationInformations => _localizer["InvalidAuthenticationInformations"];
    public string InvalidCredentials => _localizer["InvalidCredentials"];
    public string SessionExpired => _localizer["SessionExpired"];
    public string UserAlreadyExist => _localizer["UserAlreadyExist"];
    public string UserIsDeleted => _localizer["UserIsDeleted"];

    //DELETED MESSAGE
    public string AlreadyDeleted => _localizer["AlreadyDeleted"];

}
