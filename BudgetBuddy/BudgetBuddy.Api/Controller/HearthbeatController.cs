using BudgetBuddy.Contracts.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Api.Controller;

[ApiController]
[Route("api/hearthbeat")]
public class HearthbeatController
{
    [HttpGet]
    [EndpointSummary("Check if server is reachable")]
    [EndpointDescription("Hearthbeat the server to see if you get a response")]
    public BuddyResponse<int> Get()
    {
        return new BuddyResponse<int>(1);
    }
}