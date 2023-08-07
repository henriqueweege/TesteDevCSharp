using DomainServices.Utils;
using Questao1;

AppSettings.Set();

Printer.ShowInitialMessageQuestion1();

var account = Operations.SetupAccount();
Printer.ShowAccount(account, true);

account = Operations.Deposit(account);
Printer.ShowAccount(account);

account = Operations.Withdrawn(account, AppSettings.Fee);
Printer.ShowAccount(account);