using Domain.Commands.Contracts;
using Domain.Entities.Contracts;
using Domain.Models.Contracts;

namespace Domain.Converters.Contracts;

public interface IConverter<E,M> where E : IEntity where M : IModel
{
    public M ConvertFromEntityToModel(E entity);
    public E ConvertFromModelToEntity(M model);
    public IList<E> ConvertFromModelToEntity(IEnumerable<M> models);
    public E ConvertFromCommandCreateToEntity(ICreateCommand command);
}
