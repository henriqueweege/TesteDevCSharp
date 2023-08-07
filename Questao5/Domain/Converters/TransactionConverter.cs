using Domain.Commands.CheckingAccountCommands;
using Domain.Commands.Contracts;
using Domain.Converters.Contracts;
using Domain.Entities;
using Domain.Entities.Contracts;
using Domain.Models;
using Domain.Models.Contracts;
using Tools;

namespace Domain.Converters;

public class TransactionConverter : IConverter<TransactionEntity, TransactionModel>
{
    public TransactionEntity ConvertFromCommandCreateToEntity(ICreateCommand command)
    {
        var transactionCommand = command as ExecuteTransactionCommand;
        return new TransactionEntity(transactionCommand.Id, transactionCommand.IdCheckingAccount, transactionCommand.Type.ToString(), transactionCommand.Value, null, true);
    }

    public TransactionModel ConvertFromEntityToModel(TransactionEntity entity)
    {
        var transactionEntity = entity as TransactionEntity;

        return new TransactionModel()
        {
            idmovimento = transactionEntity.Id.ToString(),
            idcontacorrente = transactionEntity.IdCheckingAccount.ToString(),
            datamovimento = transactionEntity.Date,
            tipomovimento = transactionEntity.Type.ToDescription().ToUpper(),
            valor = transactionEntity.Value
        };
    }
    public TransactionEntity ConvertFromModelToEntity(TransactionModel model)
    {
        var transactionModel = (TransactionModel)model;
        return new TransactionEntity(Guid.Parse(transactionModel.idmovimento), Guid.Parse(transactionModel.idcontacorrente), transactionModel.tipomovimento, transactionModel.valor, transactionModel.datamovimento, false);
    }

    public IList<TransactionEntity> ConvertFromModelToEntity(IEnumerable<TransactionModel> models)
    {
        var entities = new List<TransactionEntity>();
        foreach (var m in models)
        {
            entities.Add(ConvertFromModelToEntity(m));
        }
        return entities;
    }
}
