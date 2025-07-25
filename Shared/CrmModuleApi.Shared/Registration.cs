using CrmModuleApi.Shared.ErrorMessages;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CrmModuleApi.Shared;

public static class Registration
{
    public static void AddShared(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddScoped<ErrorMessageService>();

    }
}
