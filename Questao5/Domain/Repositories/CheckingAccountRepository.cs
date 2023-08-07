using Domain.Models;
using Domain.Repositories.Base;
using Infrastructure.Data;
using Infrastructure.Data.Contracts;

namespace Domain.Repositories
{
    public class CheckingAccountRepository : BaseRepository<CheckingAccountModel>
    {
        private SQLiteDbContext Context { get; set; }
        public CheckingAccountRepository(IDbContext context) : base(context)
        {
            Context = (SQLiteDbContext)context;
        }
    }
}
