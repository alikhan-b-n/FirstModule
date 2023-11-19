using FirstModule.ThirdTask.Dtos;

namespace FirstModule.ThirdTask.Services.Abstract;

public interface IAnalysisService
{
    public Task<List<TeamStandingDto>> GetAnalysis(DateTime from, DateTime until);
}