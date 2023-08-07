using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Handlers
{
    public static class MatchesStatisticsHandler
    {

        public static int GetSumOfGoals(string baseUrl, string teamName, int year)
        {

            var goalsAsTeam1 = GetGoals(baseUrl, teamName, year, getAsTeam1 : true);
            var goalsAsTeam2 = GetGoals(baseUrl, teamName, year, getAsTeam1 : false);
            return goalsAsTeam1 + goalsAsTeam2;

        }

        private static int GetGoals(string baseUrl, string teamName, int year, bool getAsTeam1)
        {
            var asTeam = getAsTeam1 == true ? 1 : 2;
            var url = $"{baseUrl}?year={year}&team{asTeam}={teamName}";
            var numOfGoals = 0;
            var numOfPages = int.MaxValue;
            var pageToGet = 1;
            while (pageToGet <= numOfPages)
            {
                try
                {
                    var info = HttpServices.FetchFootBallMatchesData($"{url}&page={pageToGet}");

                    if (getAsTeam1)
                    {
                        numOfGoals += info.data.Select(x => int.Parse(x.team1goals)).ToList().Sum();
                    }
                    else
                    {
                        numOfGoals += info.data.Select(x => int.Parse(x.team2goals)).ToList().Sum();
                    }
                    numOfPages = info.total_pages;
                }
                catch
                {
                    //LOG
                }

                pageToGet++;
            }

            return numOfGoals;
        }
    }
}
