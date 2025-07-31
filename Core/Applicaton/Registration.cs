using Applicaton.Behaviors;
using Applicaton.Exceptions;
using Applicaton.Rules;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Applicaton;

public static class Registration
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules<>));
        services.AddTransient<ExceptionMiddleware>();

        services.AddScoped<MeetingRules>();

        services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(FluentValidationBehevior<,>));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services,
    Assembly assembly,
    Type openGenericBaseType)
    {
        var types = assembly.GetTypes()
            .Where(t =>
                t.BaseType != null &&
                t.BaseType.IsGenericType &&
                t.BaseType.GetGenericTypeDefinition() == openGenericBaseType
            )
            .ToList();

        foreach (var t in types)
            services.AddTransient(t);

        return services;
    }

}
