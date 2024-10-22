using System.Net;
using BudgetBuddy.Contracts.Enums;
using BudgetBuddy.Contracts.Model.Common;
using BudgetBuddy.Core.Exceptions;
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
        
        var (statusCode, message) = exception switch
        {
            BuddyException buddyException => (400, buddyException.Message),
            NotImplementedException => (501, "Not implemented"),
            _ => (500, "An error occurred")
        };

        HttpContext.Response.StatusCode = statusCode;
        return new BuddyResponse<string?>(StatusCodeEnum.Error, message);
    }
}