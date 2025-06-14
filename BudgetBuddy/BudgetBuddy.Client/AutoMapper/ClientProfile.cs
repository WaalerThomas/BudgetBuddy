﻿using AutoMapper;
using BudgetBuddy.Client.Model;
using BudgetBuddy.Client.Request;
using BudgetBuddy.Client.ViewModel;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Contracts.Response.Client;
using Microsoft.AspNetCore.Authentication;

namespace BudgetBuddy.Client.AutoMapper;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientModel, ClientDao>()
            .ReverseMap();
        
        CreateMap<ClientModel, ClientVm>()
            .ReverseMap();
        
        CreateMap<CreateClientRequest, ClientModel>()
            .ForMember(x => x.Id, p => p.Ignore())
            .ForMember(x => x.CreatedAt, p => p.Ignore())
            .ForMember(x => x.UpdatedAt, p => p.Ignore());
        
        CreateMap<LoginClientRequest, ClientModel>()
            .ForMember(x => x.Id, p => p.Ignore())
            .ForMember(x => x.CreatedAt, p => p.Ignore())
            .ForMember(x => x.UpdatedAt, p => p.Ignore());

        CreateMap<AuthenticationTokensResponse, AuthenticationInfoVm>();
    }
}