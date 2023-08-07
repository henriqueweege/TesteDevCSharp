using Domain.Entities;
using System.Globalization;

namespace Domain.UnitTests;

public class BankAccount_UnitTests
{
    private string OwnerName { get; set; }
    private int AccountNumber { get; set; }
    private double AccountBalance { get; set; }
    private double Fee { get; set; }
    public BankAccount_UnitTests()
    {
        OwnerName = "some name";
        AccountNumber = 1234;
        AccountBalance = 10;
        Fee = 1;
    }

    [Fact]
    public void GivenCorrectInput_Withdraw_ShouldUpdateAccountBalance()
    {
        //arrange
        var toWithdraw = 2;
        var account = new BankAccount(OwnerName, AccountNumber, AccountBalance);

        //act
        account.Withdraw(toWithdraw, Fee);

        //assert
        Assert.Equal(AccountBalance - (toWithdraw + Fee), account.AccountBalance);
    }

    [Fact]
    public void GivenCorrectInput_Deposit_ShouldUpdateAccountBalance()
    {
        //arrange
        var toDeposit = 2;
        var account = new BankAccount(OwnerName, AccountNumber, AccountBalance);

        //act
        account.Deposit(toDeposit);

        //assert
        Assert.Equal(AccountBalance + toDeposit, account.AccountBalance);
    }

    [Fact]
    public void GivenNewBankAccount_ToString_ShouldContainCorrectMessage()
    {
        //arrange
        var expected = $"Dados da conta: \nConta: {AccountNumber}, Titular: {OwnerName}, Saldo: {AccountBalance.ToString("C", CultureInfo.CurrentCulture)}";

        //act
        var account = new BankAccount(OwnerName, AccountNumber, AccountBalance);

        //assert
        Assert.Equal(expected, account.ToString());
    }

    [Fact]
    public void AfterWithdraw_ToString_ShouldUpdateAccountBalance()
    {
        //arrange
        var toWithdraw = 2;
        var expected = $"Dados da conta atualizados: \nConta: {AccountNumber}, Titular: {OwnerName}, Saldo: {(AccountBalance - (toWithdraw + Fee)).ToString("C", CultureInfo.CurrentCulture)}";
        var account = new BankAccount(OwnerName, AccountNumber, AccountBalance);

        //act
        account.Withdraw(toWithdraw, Fee);

        //assert
        Assert.Equal(expected, account.ToString());
    }

    [Fact]
    public void AfterDeposit_ToString_ShouldUpdateAccountBalance()
    {
        //arrange
        var toDeposit = 2;
        var expected = $"Dados da conta atualizados: \nConta: {AccountNumber}, Titular: {OwnerName}, Saldo: {(AccountBalance + toDeposit).ToString("C", CultureInfo.CurrentCulture)}";
        var account = new BankAccount(OwnerName, AccountNumber, AccountBalance);

        //act
        account.Deposit(toDeposit);

        //assert
        Assert.Equal(expected, account.ToString());
    }

}