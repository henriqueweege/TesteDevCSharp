using Domain.Commands.Contracts;
using Domain.Commands.ViewModels;
using Domain.Enums;
using Domain.Models;
using MediatR;

namespace Domain.Commands.CheckingAccountCommands;

public class ExecuteTransactionCommand : ICreateCommand, IRequest<CommandResult<ExecuteTransactionCommandVM>>
{
    public Guid Id { get; set; }
    public Guid IdCheckingAccount { get; set; }
    public char Type { get; set; }
    public double Value { get; set; }
}