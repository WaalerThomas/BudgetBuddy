using AutoMapper;
using BudgetBuddy.Account.Model;
using BudgetBuddy.Account.Request;
using BudgetBuddy.Account.ViewModel;
using BudgetBuddy.Contracts.Model.Account;

namespace BudgetBuddy.Account.AutoMapper;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<AccountModel, AccountVm>();
        CreateMap<AccountVm, AccountModel>()
            .ForMember(x => x.CreatedAt, p => p.Ignore())
            .ForMember(x => x.UpdatedAt, p => p.Ignore());

        CreateMap<AccountModel, AccountDao>()
            .ReverseMap();

        CreateMap<CreateAccountRequest, AccountModel>();
        CreateMap<UpdateAccountRequest, AccountModel>();

        CreateMap<AccountBalanceModel, AccountBalanceVm>();
    }
}