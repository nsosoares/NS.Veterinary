using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using NS.Veterinary.Api.Exceptions;
using NS.Veterinary.Api.Notifications;

namespace NS.Veterinary.Api.Extensions
{
    public static class ProblemDetailsOptionsExtensions
    {
        public static IServiceCollection AddProblemDetailsWithConfigurations(this IServiceCollection services)
        {
            services.AddProblemDetails((options) =>
            {
                // Only include exception details in a development environment. There's really no need
                // to set this as it's the default behavior. It's just included here for completeness :)
                //options.IncludeExceptionDetails = (ctx, ex) => Environment.IsDevelopment();

                // You can configure the middleware to re-throw certain types of exceptions, all exceptions or based on a predicate.
                // This is useful if you have upstream middleware that needs to do additional handling of exceptions.
                options.Rethrow<NotSupportedException>();

                // This will map NotImplementedException to the 501 Not Implemented status code.
                options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

                // This will map HttpRequestException to the 503 Service Unavailable status code.
                options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

                // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
                // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });
            return services;
        }
    }
}
