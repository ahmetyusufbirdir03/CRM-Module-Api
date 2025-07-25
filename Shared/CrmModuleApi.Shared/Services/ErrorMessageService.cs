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

    public string CustomersNotFound => _localizer["CustomersNotFound"];
    public string CustomerNotFound => _localizer["CustomerNotFound"];
    public string CustomerAlreadyExist => _localizer["CustomerAlreadyExist"];
}
