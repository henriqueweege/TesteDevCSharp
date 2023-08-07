using Domain.Models;
using Domain.Repositories.Base;
using Infrastructure.Data;
using Infrastructure.Data.Contracts;

namespace Domain.Repositories;

public class IdempotencyRepository : BaseRepository<IdempotencyModel>
{
    private SQLiteDbContext Context { get; set; }
    public IdempotencyRepository(IDbContext context) : base(context)
    {
        Context = (SQLiteDbContext)context;
    }
}
