using Domain.Commands.CheckingAccountCommands;
using Domain.Commands.Contracts;
using Domain.Converters.Contracts;
using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Models;
using Domain.Models.Contracts;

namespace Domain.Converters;

public class CheckingAccountConverter : IConverter<CheckingAccountEntity, CheckingAccountModel>
{
    public CheckingAccountEntity ConvertFromCommandCreateToEntity(ICreateCommand command)
    {
        var createCheckingAccountCommand = command as CreateCheckingAccountCommand;
        return new CheckingAccountEntity(true, createCheckingAccountCommand.HolderName);
    }

    public CheckingAccountModel ConvertFromEntityToModel(CheckingAccountEntity entity)
    {
        var checkingAccountEntity = entity as CheckingAccountEntity;

        return new CheckingAccountModel()
        {
            idcontacorrente = checkingAccountEntity.IdCheckingAccount.ToString(),
            nome = checkingAccountEntity.HolderName,
            numero = checkingAccountEntity.Number,
            ativo = checkingAccountEntity.Active
        };
    }

    public CheckingAccountEntity ConvertFromModelToEntity(CheckingAccountModel model)
    {
        var checkingAccountModel = model;
        return new CheckingAccountEntity(false, checkingAccountModel.nome, Guid.Parse(checkingAccountModel.idcontacorrente), checkingAccountModel.numero, checkingAccountModel.ativo);
    }

    public IList<CheckingAccountEntity> ConvertFromModelToEntity(IEnumerable<CheckingAccountModel> models)
    {
        var entities = new List<CheckingAccountEntity>();
        foreach (var m in models)
        {
            entities.Add(ConvertFromModelToEntity(m));
        }
        return entities;
    }
}
