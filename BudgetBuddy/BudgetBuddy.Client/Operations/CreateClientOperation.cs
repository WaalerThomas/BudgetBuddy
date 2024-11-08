using AutoMapper;
using BudgetBuddy.Client.Model;
using BudgetBuddy.Client.Repositories;
using BudgetBuddy.Client.Service;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Core.Operation;
using FluentValidation;

namespace BudgetBuddy.Client.Operations;

public class CreateClientOperation : Operation<ClientModel, ClientModel>
{
    private readonly IMapper _mapper;
    private readonly IClientValidator _clientValidator;
    private readonly IClientService _clientService;
    private readonly IClientRepository _clientRepository;
    private readonly IPasswordService _passwordService;

    public CreateClientOperation(
        IClientValidator clientValidator,
        IClientService clientService,
        IPasswordService passwordService,
        IClientRepository clientRepository,
        IMapper mapper)
    {
        _clientValidator = clientValidator;
        _clientService = clientService;
        _passwordService = passwordService;
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    protected override ClientModel OnOperate(ClientModel clientModel)
    {
        _clientValidator.ValidateAndThrow(clientModel);
        
        var client = _clientService.GetByUsername(clientModel.Username);
        if (client is not null)
        {
            throw new BuddyException("Client already exists");
        }
        
        clientModel.Password = _passwordService.Hash(clientModel.Password, out var salt);
        clientModel.Salt = salt;
        
        var clientDao = _mapper.Map<ClientDao>(clientModel);
        clientDao = _clientRepository.Create(clientDao);

        clientModel = _mapper.Map<ClientModel>(clientDao);
        return clientModel;
    }
}