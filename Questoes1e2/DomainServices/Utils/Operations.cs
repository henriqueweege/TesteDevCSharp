using Domain.Entities;
using Domain.DTO;
using System.Text;

namespace DomainServices.Utils;

public static class Operations
{
    #region Question 2

    public static InputParametersQuestion2DTO GetParametersQuestion2()
    {
        var teamName = GetTeamName();
        var year = GetYear();
        return new InputParametersQuestion2DTO()
        {
            TeamName = teamName,
            Year = year
        };
    }

    private static int GetYear()
    {
        Printer.ShowInputYearMessage();
        var year = Console.ReadLine();

        while (InputValidator.ValidateNumberInput(year) is false)
        {
            Printer.ShowInvalidYearMessage();

            Printer.DisplayUntilNow();
            year = Console.ReadLine();
        }

        Printer.AddToDisplay(year);
        return int.Parse(year);
    }

    private static string GetTeamName()
    {
        Printer.ShowInputTeamNameMessage();
        var teamName = Console.ReadLine();

        while (InputValidator.ValidateTeamNameInput(teamName) is false)
        {
            Printer.ShowInvalidTeamNameMessage();

            Printer.DisplayUntilNow();
            teamName = Console.ReadLine();
        }

        Printer.AddToDisplay(teamName);
        return teamName;
    }

    #endregion

    #region Question 1
    public static BankAccount Withdrawn(BankAccount account, double fee)
    {
        var amount = GetWithdrawnAmount();
        account.Withdraw((double)TreatDepositWithdrawData(amount), fee);
        return account;
    }

    public static BankAccount Deposit(BankAccount account)
    {
        var deposit = GetDepositAmount();
        account.Deposit((double)TreatDepositWithdrawData(deposit));
        return account;
    }

    public static BankAccount SetupAccount()
    {

        string? accountNumber = GetAccountNumber();
        string? ownerName = GetOwnerName(accountNumber);
        string? initialDepositInput = GetInitialDepositOption(accountNumber, ownerName);

        var isInitialDeposit = initialDepositInput.ToLower() == "s" ? true : false;

        string? deposit = "";
        if (isInitialDeposit)
        {
            deposit = GetInitialDeposit(accountNumber, ownerName, initialDepositInput);
        }

        double? accountBalance = TreatDepositWithdrawData(deposit);

        return new BankAccount(ownerName, int.Parse(accountNumber), accountBalance);
    }

    private static double? TreatDepositWithdrawData(string deposit)
        => string.IsNullOrWhiteSpace(deposit) == true ? null : double.Parse(deposit.Replace('.', ','));


    private static string GetWithdrawnAmount()
    {
        Printer.ShowInputWithdrawnMessage();
        var amount = Console.ReadLine();

        while (InputValidator.ValidateDepositWithdrawnInput(amount) is false)
        {
            Printer.ShowInvalidDepositWithdrawnMessage();

            Printer.DisplayUntilNow();
            amount = Console.ReadLine();
        }

        Printer.AddToDisplay(amount);
        return amount;
    }

    private static string GetDepositAmount()
    {
        Printer.ShowInputDepositMessage();
        var deposit = Console.ReadLine();

        while (InputValidator.ValidateDepositWithdrawnInput(deposit) is false)
        {
            Printer.ShowInvalidDepositWithdrawnMessage();


            Printer.DisplayUntilNow();
            deposit = Console.ReadLine();
        }

        Printer.AddToDisplay(deposit);
        return deposit;
    }

    private static string GetInitialDeposit(string accountNumber, string ownerName, string initialDepositInput)
    {



        Printer.ShowInputInitialDepositMessage();
        var deposit = Console.ReadLine();

        while (InputValidator.ValidateNumberInput(deposit) is false)
        {
            Printer.ShowInvalidDepositWithdrawnMessage();


            Printer.DisplayUntilNow();
            deposit = Console.ReadLine();
        }

        Printer.AddToDisplay(deposit);
        return deposit;
    }

    private static string GetInitialDepositOption(string accountNumber, string ownerName)
    {
        Printer.ShowInputInitialDepositOptionMessage();
        var initialDepositInput = Console.ReadLine();

        while (InputValidator.ValidateInitialDepositOptionInput(initialDepositInput) is false)
        {
            Printer.ShowInvalidInitialDepositOptionMessage();

            Printer.DisplayUntilNow();
            initialDepositInput = Console.ReadLine();
        }

        Printer.AddToDisplay(initialDepositInput);
        return initialDepositInput;
    }

    private static string GetOwnerName(string accountNumber)
    {
        Printer.ShowInputOwnerNameMessage();
        var ownerName = Console.ReadLine();
        while (InputValidator.ValidateNameInput(ownerName) is false)
        {
            Printer.ShowInvalidOwnerNameMessage();

            Printer.DisplayUntilNow();
            ownerName = Console.ReadLine();
        }

        Printer.AddToDisplay(ownerName);
        return ownerName;
    }

    private static string GetAccountNumber()
    {
        Printer.ShowInputAccountNumberMessage();
        var accountNumber = Console.ReadLine();

        while (InputValidator.ValidateNumberInput(accountNumber) is false)
        {
            Printer.ShowInvalidAccountNumberMessage();

            Printer.DisplayUntilNow();
            accountNumber = Console.ReadLine();
        }

        Printer.AddToDisplay(accountNumber);
        return accountNumber;
    }

    #endregion
}

