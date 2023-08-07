using Dapper.Contrib.Extensions;
using Domain.Models.Contracts;
using Domain.Repositories.Contracts;
using Infrastructure.Data;
using Infrastructure.Data.Contracts;

namespace Domain.Repositories.Base;

public class BaseRepository<T> : IDisposable, IRepository<T> where T : class, IModel
{
    private SQLiteDbContext Context { get; set; }
    public BaseRepository(IDbContext context)
    {
        Context = (SQLiteDbContext)context;
    }
    public bool Delete(T model) => Context.DbConnection.Delete(model);

    public IEnumerable<T> GetAll() => Context.DbConnection.GetAll<T>().ToList();

    public T GetById(string id) => Context.DbConnection.Get<T>(id);

    public long Save(T model) => Context.DbConnection.Insert(model);

    public bool Update(T model) => Context.DbConnection.UpdateAsync(model).Result;

    public void Dispose()
    {
        Context.Dispose();
    }
}
