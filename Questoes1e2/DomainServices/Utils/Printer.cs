using Domain.Entities;
using System.Text;

namespace DomainServices.Utils;

public static class Printer
{
    private static StringBuilder DisplayedUntilNow { get; set; }

    static Printer()
    {
        DisplayedUntilNow = new StringBuilder();
    }

    public static void AddToDisplay(string toAdd)
    {
        DisplayedUntilNow.AppendLine(toAdd);
    }

    public static void DisplayUntilNow()
    {
        Console.Write(DisplayedUntilNow.ToString());
    }

    private static void Display(string toDisplay)
    {
        DisplayedUntilNow.Append(toDisplay);
        Console.Write(toDisplay);
    }

    private static void DisplayErrorMessage(string message)
    {
        Console.Write(message);
        Thread.Sleep(4000);
        Console.Clear();
    }

    #region Question 2

    public static void ShowInitialMessageQuestion2()
        => Display("Olá! Para consultar estatísticas de um time, por favor, forneça os dados abaixo. \n");

    public static void ShowInputTeamNameMessage()
        => Display("Time: ");

    public static void ShowInputYearMessage()
        => Display("Ano: ");

    public static void ShowInvalidTeamNameMessage()
        => DisplayErrorMessage("Nome do time deve conter apenas letras e caracteres especiais. Por favor, tente novamente. \n");

    public static void ShowInvalidYearMessage()
        => DisplayErrorMessage("Ano deve conter apenas números. Por favor, tente novamente. \n");

    #endregion

    #region Question 1

    public static void ShowInitialMessageQuestion1()
        => Display("Olá! Para cadastrar uma conta, por favor, forneça os dados abaixo. \n");

    public static void ShowInputAccountNumberMessage()
        => Display("Entre o número da conta: ");

    public static void ShowInputWithdrawnMessage()
        => Display("Entre um valor para saque: ");

    public static void ShowInputOwnerNameMessage()
        => Display("Entre o titular da conta: ");

    public static void ShowInvalidOwnerNameMessage()
        => DisplayErrorMessage("Nome do titular deve conter apenas letras. Por favor, tente novamente. \n");

    public static void ShowInvalidAccountNumberMessage()
        => DisplayErrorMessage("Número da conta deve conter apenas números. Por favor, tente novamente. \n");

    public static void ShowInputInitialDepositOptionMessage()
        => Display("Haverá depósito inicial (s/n)? ");

    public static void ShowInvalidInitialDepositOptionMessage()
        => DisplayErrorMessage("Por favor, digite 'S' para realizar um depósito, ou 'N' para não realizar um depósito. \n");

    public static void ShowInputInitialDepositMessage()
        => Display("Entre o valor de depósito inicial: ");

    public static void ShowInputDepositMessage()
        => Display("Entre o valor de depósito: ");

    public static void ShowInvalidDepositWithdrawnMessage()
        => DisplayErrorMessage("Deposito deve conter apenas apenas números e ser positivo. Por favor, tente novamente. \n");

    public static void ShowAccount(BankAccount account, bool breakLineBefore = false)
    {
        if (breakLineBefore)
        {
            Display($"\n{account} \n\n");
        }
        else
        {
            Display($"{account} \n\n");
        }
    }

    public static void PrintQntOfGoals(int qntOfGoals, string teamName, int year)
        => Display($"Team {teamName} scored {qntOfGoals} goals in {year} \n\n");

    public static void DisplayMessage(string message)
        => DisplayErrorMessage(message);


    #endregion
}
