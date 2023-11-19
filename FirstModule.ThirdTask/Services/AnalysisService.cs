using System.Diagnostics.CodeAnalysis;
using FirstModule.ThirdTask.Dtos;
using FirstModule.ThirdTask.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FirstModule.ThirdTask.Services;

public class AnalysisService : IAnalysisService
{
    private readonly ApplicationContext _applicationContext;

    public AnalysisService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<List<TeamStandingDto>> GetAnalysis(DateTime from, DateTime until)
    {
        var gamesBetweenDates = _applicationContext
            .Games
            .Where(x => x.DateOfEvent >= from.ToUniversalTime() && x.DateOfEvent <= until.ToUniversalTime()).Select(x => new
            {
                x.HomeId,
                x.VisitorsId,
                x.ScoreId
            }).ToList();

        var teamStandings = _applicationContext.Teams.Select(x => new TeamStandingDto
        {
            Name = x.Name
        }).ToList();

        foreach (var game in gamesBetweenDates)
        {
            var homeTeam = await _applicationContext.Teams.FirstAsync(x => x.Id.Equals(game.HomeId));
            var visitorTeam = await _applicationContext.Teams.FirstAsync(x => x.Id.Equals(game.VisitorsId));
            var score = await _applicationContext.Scores.FirstAsync(x => x.Id.Equals(game.ScoreId));

            if (score.HomeScore > score.VisitorsScore)
            {
                teamStandings.First(x => x.Name.Equals(homeTeam.Name)).Wins += 1;
                teamStandings.First(x => x.Name.Equals(visitorTeam.Name)).Loses += 1;
            }
            else
            {
                teamStandings.First(x => x.Name.Equals(homeTeam.Name)).Loses += 1;
                teamStandings.First(x => x.Name.Equals(visitorTeam.Name)).Wins += 1;
            }
        }

        return teamStandings;
    }
}