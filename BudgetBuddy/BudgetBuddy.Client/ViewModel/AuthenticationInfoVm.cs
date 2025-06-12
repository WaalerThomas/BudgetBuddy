namespace BudgetBuddy.Client.ViewModel;

public class AuthenticationInfoVm
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime ExpiresIn { get; set; }
    public DateTime RefreshExpiresIn { get; set; }
    public ClientVm Client { get; set; }
}
