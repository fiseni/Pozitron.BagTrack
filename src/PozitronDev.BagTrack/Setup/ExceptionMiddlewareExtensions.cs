using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PozitronDev.SharedKernel.Exceptions;
using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace PozitronDev.BagTrack.Setup;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            return;
        }

        app.UseExceptionHandler(new ExceptionHandlerOptions()
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = async context =>
            {
                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionObject is not null)
                {
                    var errorDetails = exceptionObject.Error switch
                    {
                        NotFoundException ex => GetProblemDetails(context, HttpStatusCode.NotFound, ex.Message),
                        HttpRequestException => GetProblemDetails(context, HttpStatusCode.GatewayTimeout, "The dependent service is down"),
                        AppException ex => GetProblemDetails(context, HttpStatusCode.BadRequest, ex.Message),
                        _ => GetProblemDetails(context, HttpStatusCode.BadRequest, "Internal error"),
                    };

                    context.Response.ContentType = "application/problem+json; charset=utf-8";
                    context.Response.StatusCode = errorDetails.Status ?? 400;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
                }
            }
        });
    }

    private static ProblemDetails GetProblemDetails(HttpContext context, HttpStatusCode status, string message)
    {
        var (title, rfcSection) = status switch
        {
            HttpStatusCode.NotFound => ("Not Found", "6.5.4"),
            HttpStatusCode.GatewayTimeout => ("Gateway Timeout", "6.6.5"),
            _ => ("Bad Request", "6.5.1")
        };

        var result = new ProblemDetails
        {
            Type = $"https://tools.ietf.org/html/rfc7231#section-{rfcSection}",
            Title = title,
            Status = (int)status,
        };

        result.Extensions.Add("traceId", Activity.Current?.Id ?? context?.TraceIdentifier);
        result.Extensions.Add("errors", new Dictionary<string, List<string>>()
        {
            [""] = new() { message }
        });

        return result;
    }
}
