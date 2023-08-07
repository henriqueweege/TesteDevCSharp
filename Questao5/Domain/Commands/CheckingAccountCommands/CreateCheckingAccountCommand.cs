using Domain.Commands.Contracts;
using Domain.Models;
using MediatR;

namespace Domain.Commands.CheckingAccountCommands;

public class CreateCheckingAccountCommand : ICreateCommand, IRequest<CommandResult<CheckingAccountModel>>
{
    public string HolderName { get; set; }

}
