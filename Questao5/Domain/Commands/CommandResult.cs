using Domain.Models.Contracts;
using MediatR;

namespace Domain.Commands;

public class CommandResult<M> : IRequest where M : IModel
{
    public M? Result { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public CommandResult()
    {

    }

    public CommandResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public CommandResult(bool success, string message, M entity)
    {
        Result = entity;
        Success = success;
        Message = message;
    }
}
