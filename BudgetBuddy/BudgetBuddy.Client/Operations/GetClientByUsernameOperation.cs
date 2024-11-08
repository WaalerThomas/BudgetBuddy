using AutoMapper;
using BudgetBuddy.Client.Repositories;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Client.Operations;

public class GetClientByUsernameOperation : Operation<string, ClientModel?>
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;

    public GetClientByUsernameOperation(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    protected override ClientModel? OnOperate(string username)
    {
        var clientDao = _clientRepository.GetByUsername(username);
        return _mapper.Map<ClientModel>(clientDao);
    }
}