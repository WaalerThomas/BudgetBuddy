using System.Net;
using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Core.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Api.Controller;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public BuddyResponse<string?> Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception?.InnerException is BuddyException)
        {
            exception = exception.InnerException;
        }
        
        // TODO: Change exception class to also include a status code
        // TODO: Catch UnauthorizedAccessException
        var (statusCode, message) = exception switch
        {
            BuddyException buddyException => (400, buddyException.Message),
            ValidationException validationException => (400, validationException.Message),
            NotImplementedException => (501, "Not implemented"),
            _ => (500, "An error occurred")
        };

        HttpContext.Response.StatusCode = statusCode;
        return new BuddyResponse<string?>(StatusCodeEnum.Error, message);
    }
}