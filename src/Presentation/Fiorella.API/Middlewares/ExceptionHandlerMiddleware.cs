using Fiorella.Aplication.DTOs.ResponseDTOs;
using Fiorella.Persistence.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace Fiorella.API.Middlewares
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    int statusCode = (int)HttpStatusCode.InternalServerError;
                    string message = "Internal Server Error";

                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is IBaseException)
                        {
                            var exception = (IBaseException)contextFeature.Error;
                            statusCode = exception.StatusCode;
                            message = exception.CustomMessage;
                        }
                    }
                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(new ExceptionResponseDto(statusCode, message));
                    await context.Response.CompleteAsync();
                });
            });
            return app;
        }
    }
}
