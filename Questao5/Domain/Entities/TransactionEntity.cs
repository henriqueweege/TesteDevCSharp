using Domain.Entities.Contracts;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Tools;

namespace Domain.Entities;

[Table("[movimento]")]
public class TransactionEntity : IEntity
{
    public Guid Id { get; private set; }
    public Guid IdCheckingAccount { get; private set; }
    public DateTime Date { get; private set; }
    public ETransactionType Type { get; private set; }
    public double Value { get; private set; }
    public TransactionEntity(Guid id, Guid idCheckingAccount, string type, double value, DateTime? date = null, bool? newEntity = true)
    {
        HandleType(type);
        HandleDate(newEntity, date);

        Id = id;
        IdCheckingAccount = idCheckingAccount;
        Value = value;
    }

    private void HandleDate(bool? newEntity, DateTime? date)
    {
        if (newEntity is not null && newEntity is true)
        {
            Date = DateTime.Now.Date;
        }
        else if (date is null)
        {
            throw new ArgumentException(EErrorMessages.INVALID_PARAMETER.ToDescription());
        }
        else
        {
            Date = (DateTime)date;
        }
    }

    private void HandleType(string type)
    {
        var toCheck = type.ToUpper();
        if (toCheck is not "C" && toCheck is not "D")
            throw new ArgumentException(EErrorMessages.INVALID_TYPE.ToDescription());

        if (toCheck == ETransactionType.Credit.ToDescription().ToUpper())
        {
            Type = ETransactionType.Credit;
        }
        else if (toCheck == ETransactionType.Debit.ToDescription().ToUpper())
        {
            Type = ETransactionType.Debit;
        }
    }
}