using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Exceptions;

namespace Todo.Web.Infrastructure;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case DomainException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                //case KeyNotFoundException e:
                //    // not found error
                //    response.StatusCode = (int)HttpStatusCode.NotFound;
                //    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            /*
                Type – [string] – URI reference to identify the problem type
                Title – [string] – a short human-readable problem summary
                Status – [number] – the HTTP status code generated on the problem occurrence
                Detail – [string] – a human-readable explanation for what exactly happened
                Instance – [string] – URI reference of the occurrence
             */
            await response.WriteAsJsonAsync(new ProblemDetails
            {
                Detail = error.Message,
                Status = response.StatusCode,
                Title = "Error",
                Instance = context.Request.GetEncodedPathAndQuery()
            }, typeof(ProblemDetails), new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}
