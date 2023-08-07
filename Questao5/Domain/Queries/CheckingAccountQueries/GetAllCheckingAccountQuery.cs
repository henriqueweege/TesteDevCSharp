using Domain.Models;
using Domain.Queries.Contracts;
using MediatR;

namespace Domain.Queries.CheckingAccountQueries;

public class GetAllCheckingAccountQuery : IGetAllQuery, IRequest<QueryResult<CheckingAccountModel>>
{
}
