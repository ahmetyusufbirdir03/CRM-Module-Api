using Microsoft.AspNetCore.Builder;

namespace Applicaton.Exceptions;

public static class ConfigureExceptionMiddleware
{
    public static void ConfigureExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
