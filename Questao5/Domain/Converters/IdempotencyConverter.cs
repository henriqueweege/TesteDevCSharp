using Domain.Commands.Contracts;
using Domain.Converters.Contracts;
using Domain.Entities;
using Domain.Models;

namespace Domain.Converters;

public class IdempotencyConverter : IConverter<IdempotencyEntity, IdempotencyModel>
{
    public IdempotencyEntity ConvertFromCommandCreateToEntity(ICreateCommand command)
    {
        throw new NotImplementedException();
    }

    public IdempotencyModel ConvertFromEntityToModel(IdempotencyEntity entity)
        =>new IdempotencyModel()
        {
            chave_idempotencia = entity.Id.ToString(),
            requisicao = entity.Requisition,
            resultado = entity.Result
        };


    public IdempotencyEntity ConvertFromModelToEntity(IdempotencyModel model)
        => new IdempotencyEntity(false, Guid.Parse(model.chave_idempotencia), model.requisicao, model.resultado);

    public IList<IdempotencyEntity> ConvertFromModelToEntity(IEnumerable<IdempotencyModel> models)
    {
        throw new NotImplementedException();
    }
}
