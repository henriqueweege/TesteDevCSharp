using Domain.DTO;
using DomainServices.Handlers;
using DomainServices.Utils;
using Infrastructure.Services;
using Questao2;
using System;

AppSettings.Set();

Printer.ShowInitialMessageQuestion2();

while (true)
{
    var parameters = Operations.GetParametersQuestion2();
    try
    {
        var qntOfGoals = MatchesStatisticsHandler.GetSumOfGoals(AppSettings.UrlApi, parameters.TeamName, parameters.Year);
        Printer.PrintQntOfGoals(qntOfGoals, parameters.TeamName, parameters.Year);
    }
    catch
    {
        Printer.DisplayMessage("Um erro ocorreu ao tentar buscar os dados solicitados. Por favor, tente novamente mais tarde");
    }
}
