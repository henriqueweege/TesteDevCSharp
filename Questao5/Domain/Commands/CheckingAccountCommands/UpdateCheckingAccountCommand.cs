using Domain.Commands.Contracts;
using Domain.Models;
using MediatR;

namespace Domain.Commands.CheckingAccountCommands;

public class UpdateCheckingAccountCommand : IUpdateCommand, IRequest<CommandResult<CheckingAccountModel>>
{
    public Guid IdCheckingAccount { get; set; }
    public string HolderName { get; set; }
    public bool Active { get; set; }
}
