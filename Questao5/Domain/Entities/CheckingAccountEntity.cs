using Domain.Entities.Contracts;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Tools;

namespace Domain.Entities;

[Table("[contacorrente]")]
public class CheckingAccountEntity : IEntity
{
    public Guid IdCheckingAccount { get; private set; }
    public int Number { get; private set; }
    public string HolderName { get; private set; }
    public bool Active { get; private set; }

    public CheckingAccountEntity(bool isNewEntity, string holderName, Guid? idCheckingAccount = null, int? number = null, bool? active = null)
    {
        if ((!isNewEntity && (idCheckingAccount == null || number == null || active == null)) || holderName == null)
        {
            throw new ArgumentException(EErrorMessages.INVALID_PARAMETER.ToDescription());
        }
        else if (!isNewEntity)
        {
            IdCheckingAccount = (Guid)idCheckingAccount;
            Number = (int)number;
            HolderName = holderName;
            Active = (bool)active;
        }
        else
        {
            IdCheckingAccount = Guid.NewGuid();
            Number = GenerateAccountNumber();
            HolderName = holderName;
            Active = true;
        }
    }

    public void UpdateName(string newName)
    {
        HolderName = newName;
    }
    public void Activate()
    {
        Active = true;
    }
    public void Inactivate()
    {
        Active = false;
    }

    private int GenerateAccountNumber()
    {
        var random = new Random();
        return random.Next(1000, 999999999);
    }
}