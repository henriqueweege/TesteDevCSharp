using Domain.Models;
using Domain.Queries.Contracts;
using Domain.Queries.ViewModel;
using MediatR;

namespace Domain.Queries.CheckingAccountQueries;

public class GetCheckingAccountBalanceQuery : IGetByIdQuery, IRequest<QueryResult<GetCheckingAccountBalanceVM>>
{
    public Guid Id { get; set; }
}
