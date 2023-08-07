using Domain.Models;
using Domain.Queries.Contracts;
using MediatR;

namespace Domain.Queries.CheckingAccountQueries;

public class GetByIdCheckingAccountQuery : IGetByIdQuery, IRequest<QueryResult<CheckingAccountModel>>
{
    public Guid Id { get; set; }
}
