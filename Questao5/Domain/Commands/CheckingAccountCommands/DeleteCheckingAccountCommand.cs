using Domain.Commands.Contracts;
using Domain.Models;
using MediatR;

namespace Domain.Commands.CheckingAccountCommands;

public class DeleteCheckingAccountCommand : IDeleteCommand, IRequest<CommandResult<CheckingAccountModel>>
{
    public Guid Id { get; set; }
}
