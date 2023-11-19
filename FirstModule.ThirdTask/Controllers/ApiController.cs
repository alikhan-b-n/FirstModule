using FirstModule.ThirdTask.Services;
using FirstModule.ThirdTask.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FirstModule.ThirdTask.Controllers;

public class ApiController : ControllerBase
{
    private readonly IApiService _apiService;
    private readonly IAnalysisService _analysisService;

    public ApiController(IApiService apiService, IAnalysisService analysisService)
    {
        _apiService = apiService;
        _analysisService = analysisService;
    }

    [HttpGet("/api/service/")]
    public async Task<IActionResult> GetInfoByDate([FromQuery] DateTime date)
    {
        await _apiService.GetInfoByDateInDb(date);
        return Ok("done");
    }
    
    [HttpGet("/api/service/{season}")]
    public async Task<IActionResult> GetInfoBySeason([FromRoute] string season)
    {
        await _apiService.GetInfoBySeasonInDb(season);
        return Ok("done");
    }

    [HttpGet("/api/service/analysis")]
    public async Task<IActionResult> GetAnalysis([FromQuery] DateTime from, DateTime until)
    {
        return Ok(await _analysisService.GetAnalysis(from, until));
    }
}
