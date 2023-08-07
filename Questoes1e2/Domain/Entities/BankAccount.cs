using System.Globalization;

namespace Domain.Entities;

public class BankAccount
{
    public string HolderName { get; private set; }
    public int AccountNumber { get; private set; }
    public double AccountBalance { get; private set; }
    private bool Updated { get; set; }
    public BankAccount(string holderName, int accountNumber, double? accountBalance)
    {
        HolderName = holderName;
        AccountNumber = accountNumber;
        AccountBalance = accountBalance is null ? 0 : (double)accountBalance;
    }

    public void Withdraw(double toWithdraw, double fee)
    {
        AccountBalance -= toWithdraw + fee;
        Updated = true;
    }

    public void Deposit(double toDeposit)
    {
        AccountBalance += toDeposit;
        Updated = true;
    }

    public void ChangeName(string newName)
    {
        HolderName = newName;
        Updated = true;
    }

    public override string ToString()
        => Updated is false ? $"Dados da conta: \nConta: {AccountNumber}, Titular: {HolderName}, Saldo: {AccountBalance.ToString("C", CultureInfo.CurrentCulture)}"
        : $"Dados da conta atualizados: \nConta: {AccountNumber}, Titular: {HolderName}, Saldo: {AccountBalance.ToString("C", CultureInfo.CurrentCulture)}";
}
