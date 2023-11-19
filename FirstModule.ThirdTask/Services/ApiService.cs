using System.Diagnostics.CodeAnalysis;
using FirstModule.ThirdTask.Dtos;
using FirstModule.ThirdTask.Entities;
using FirstModule.ThirdTask.Services.Abstract;
using Uri = System.Uri;


namespace FirstModule.ThirdTask.Services;

public class ApiService : IApiService
{
    private readonly ApplicationContext _applicationContext;
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiService(ApplicationContext applicationContext, IHttpClientFactory httpClientFactory)
    {
        _applicationContext = applicationContext;
        _httpClientFactory = httpClientFactory;
    }


    public async Task GetInfoByDateInDb(DateTime date)
    {
        var httpClient = _httpClientFactory.CreateClient();
        string formattedDate = date.ToString("yyyy-MM-dd");
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api-nba-v1.p.rapidapi.com/games?date={formattedDate}"),
            Headers =
            {
                { "X-RapidAPI-Key", "384d62cd25mshe08e0874f7810d8p18766ajsn8a22f40bc584" },
                { "X-RapidAPI-Host", "api-nba-v1.p.rapidapi.com" },
            },
        };
        using var apiResponse = await httpClient.SendAsync(request);
        apiResponse.EnsureSuccessStatusCode();
        Root? root = await apiResponse.Content.ReadFromJsonAsync<Root>();

        var responseList = root.response;

        await SaveInfo(responseList);
    }
    
    public async Task GetInfoBySeasonInDb(string year)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api-nba-v1.p.rapidapi.com/games?season={year}"),
            Headers =
            {
                { "X-RapidAPI-Key", "384d62cd25mshe08e0874f7810d8p18766ajsn8a22f40bc584" },
                { "X-RapidAPI-Host", "api-nba-v1.p.rapidapi.com" },
            },
        };
        using var apiResponse = await httpClient.SendAsync(request);
        apiResponse.EnsureSuccessStatusCode();
        Root? root = await apiResponse.Content.ReadFromJsonAsync<Root>();

        var responseList = root.response;

        await SaveInfo(responseList);
    }


    private async Task SaveInfo(List<ResponseDto> responseList)
    {
        foreach (var response in responseList)
        {
            Guid teamHomeId;
            Guid teamVisitorsId;

            var teamHomeExist = _applicationContext.Teams
                .Any(x => x.Name.Equals(response.teams.home.name));

            var teamVisitorsExist = _applicationContext.Teams
                .Any(x => x.Name.Equals(response.teams.visitors.name));

            if (teamHomeExist)
            {
                teamHomeId = _applicationContext.Teams
                    .First(x => x.Name.Equals(response.teams.home.name)).Id;
            }
            else
            {
                var teamsEntity = new TeamsEntity()
                {
                    Name = response.teams.home.name
                };
                teamHomeId = teamsEntity.Id;
                _applicationContext.Teams.Add(teamsEntity);
                await _applicationContext.SaveChangesAsync();
            }

            if (teamVisitorsExist)
            {
                teamVisitorsId = _applicationContext.Teams
                    .First(x => x.Name.Equals(response.teams.visitors.name)).Id;
            }
            else
            {
                var teamsEntity = new TeamsEntity()
                {
                    Name = response.teams.visitors.name
                };
                teamVisitorsId = teamsEntity.Id;
                _applicationContext.Teams.Add(teamsEntity);
                await _applicationContext.SaveChangesAsync();
            }

            Guid scoreId;

            var scoreExist = _applicationContext.Scores
                .Any(x => x.HomeScore
                              .Equals(response.scores.home.points) &&
                          x.VisitorsScore.Equals(response.scores.visitors.points));

            if (scoreExist)
            {
                scoreId = _applicationContext.Scores.First(x =>
                        x.HomeScore.Equals(response.scores.home.points) &&
                        x.VisitorsScore.Equals(response.scores.visitors.points))
                    .Id;
            }
            else
            {
                var scoreEntity = new ScoreEntity()
                {
                    HomeScore = response.scores.home.points,
                    VisitorsScore = response.scores.visitors.points
                };

                _applicationContext.Scores.Add(scoreEntity);

                await _applicationContext.SaveChangesAsync();

                scoreId = scoreEntity.Id;
            }


            var gameExist = _applicationContext.Games
                .Any(x => x.HomeId.Equals(teamHomeId) &&
                          x.VisitorsId.Equals(teamVisitorsId) &&
                          x.DateOfEvent.Date.ToUniversalTime() == response.date.start.Date.ToUniversalTime() &&
                          x.ScoreId.Equals(scoreId));

            if (!gameExist)
            {
                var gameEntity = new GameEntity()
                {
                    DateOfEvent = response.date.start.Date.ToUniversalTime(),
                    HomeId = teamHomeId,
                    ScoreId = scoreId,
                    VisitorsId = teamVisitorsId
                };

                await _applicationContext.Games.AddAsync(gameEntity);
                await _applicationContext.SaveChangesAsync();
            }
        }
    }
}