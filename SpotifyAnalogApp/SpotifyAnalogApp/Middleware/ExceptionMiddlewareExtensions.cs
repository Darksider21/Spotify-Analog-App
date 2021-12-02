using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpotifyAnalogApp.Business.DTO.ResponceDTOs;
using SpotifyAnalogApp.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Web.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    Exception error = context.Features.Get<IExceptionHandlerFeature>().Error;
                    //logger.LogError($"Exception was thrown: {error}");

                    if (error is AccessForbidenException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return;
                    }
                    if (error is NotFoundException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return;
                    }

                    await (error switch
                    {
                        BaseCustomException e => FillContextResponseAsync((int)e.ErrorCode, e.Message),
                        JsonReaderException e => FillContextResponseAsync(400,
                            "Json validation failed. Please review inserted data."),
                        _ => FillContextResponseAsync(500, "Oops! Something went wrong.", null,
                            HttpStatusCode.InternalServerError)
                    });

                    Task FillContextResponseAsync(int errorCode, string message, object additionalInfo = null,
                        HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
                    {
                        context.Response.StatusCode = (int)httpStatusCode;
                        context.Response.ContentType = "application/json";

                        return context.Response.WriteAsync(new ErrorDetails
                        {
                            ErrorCode = errorCode,
                            Message = message,
                            Description = env.IsDevelopment() ? error.ToString() : null
                        }.ToString());
                    }
                });
            });
        }
    }
}
