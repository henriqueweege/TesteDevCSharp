using Domain.Models.Contracts;

namespace Domain.Repositories.Contracts;

public interface IRepository<T> where T : class, IModel
{
    public long Save(T model);
    public T GetById(string id);
    public IEnumerable<T> GetAll();
    public bool Update(T model);
    public bool Delete(T id);
}
