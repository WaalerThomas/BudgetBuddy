using AutoMapper;
using BudgetBuddy.Client.Repositories;
using BudgetBuddy.Contracts.Model.Client;
using BudgetBuddy.Core.Exceptions;
using BudgetBuddy.Core.Operation;

namespace BudgetBuddy.Client.Operations;

public class IncrementFailedLoginAttemptsOperation : Operation<Guid, ClientModel>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public IncrementFailedLoginAttemptsOperation(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    protected override ClientModel OnOperate(Guid clientId)
    {
        var clientDao = _clientRepository.GetById(clientId);
        if (clientDao is null)
        {
            throw new BuddyException("Client not found");
        }
        
        clientDao.FailedLoginAttempts++;
        
        if (clientDao.FailedLoginAttempts >= 3)
        {
            clientDao.LockoutEnd = DateTime.UtcNow.AddMinutes(10);
        }
        
        clientDao = _clientRepository.Update(clientDao);
        
        return _mapper.Map<ClientModel>(clientDao);
    }
}