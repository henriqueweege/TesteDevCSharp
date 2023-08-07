using Domain.Models.Contracts;
using MediatR;

namespace Domain.Queries;

public class QueryResult<M> : IRequest where M : IModel
{
    public IEnumerable<M>? Result { get; private set; }
    public bool Success { get; private set; }
    public string Message { get; private set; }



    public QueryResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }
    public QueryResult(bool success, string message, M entity)
    {
        Success = success;
        Message = message;
        Result = new List<M>() { entity };
    }
    public QueryResult(bool success, string message, IEnumerable<M> entity)
    {
        Success = success;
        Message = message;
        Result = entity;
    }
}
