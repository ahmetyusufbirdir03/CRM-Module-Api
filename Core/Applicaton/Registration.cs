﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Applicaton;

public static class Registration
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
