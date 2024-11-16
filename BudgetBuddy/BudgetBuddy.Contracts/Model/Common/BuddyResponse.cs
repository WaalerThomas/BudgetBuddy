using BudgetBuddy.Contracts.Enums;

namespace BudgetBuddy.Contracts.Model.Common;

public class BuddyResponse<T>
{
    // TODO: Double check whether they should be nullable
    
    public Status Status { get; set; }
    public T? Data { get; set; }
    public string? CorrelationId { get; set; }
    
    public BuddyResponse()
    {
        Data = default;
        Status = new Status();
    }
    
    public BuddyResponse(T data) : this()
    {
        Data = data;
        Status.Code = StatusCodeEnum.Ok;
    }
    
    public BuddyResponse(StatusCodeEnum code, string message) : this()
    {
        Status.Code = code;
        Status.Message = message;
    }
}

public class Status
{
    public StatusCodeEnum Code { get; set; }
    public string Message { get; set; } = string.Empty;
} 