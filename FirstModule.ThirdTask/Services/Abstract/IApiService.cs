namespace FirstModule.ThirdTask.Services.Abstract;

public interface IApiService
{
    public Task GetInfoByDateInDb(DateTime date);
    public Task GetInfoBySeasonInDb(string year);
}