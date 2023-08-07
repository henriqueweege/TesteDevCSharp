using System.ComponentModel;

namespace Domain.Enums;

public enum ETransactionType
{
    [Description("C")]
    Credit = 1,
    [Description("D")]
    Debit = 2,
}
