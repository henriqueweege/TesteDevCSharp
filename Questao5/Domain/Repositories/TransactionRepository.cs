using Domain.Models;
using Domain.Repositories.Base;
using Infrastructure.Data;
using Infrastructure.Data.Contracts;
using System.Data.Common;
using System.Data;
using Dapper;
using System.Data.Entity.Core.Mapping;

namespace Domain.Repositories
{
    public class TransactionRepository : BaseRepository<TransactionModel>
    {
        private SQLiteDbContext Context { get; set; }
        public TransactionRepository(IDbContext context) : base(context)
            => Context = (SQLiteDbContext)context;
        

        public IList<TransactionModel> GetAllByIdCheckingAccount(string idCheckingAccount)
        {
            var query = @"
                SELECT
                    *
                FROM
                    [movimento]
                WHERE
                    [movimento].[idcontacorrente] = @idCheckingAccount";


            var items = Context.DbConnection.Query<TransactionModel>(query, new { idCheckingAccount });
            return items.ToList();
        }

    }
}
