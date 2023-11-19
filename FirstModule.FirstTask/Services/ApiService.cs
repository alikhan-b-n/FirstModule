using System.Text.Json.Serialization;
using FirstModel.FirstTask.Abstract;
using FirstModule.Dtos;

namespace FirstModule;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    public async Task<EmployeeDto> GetInfo(string url)
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri =
                new Uri(
                    $"https://fresh-linkedin-profile-data.p.rapidapi.com/get-linkedin-profile?linkedin_url={url}"),
            Headers =
            {
                { "X-RapidAPI-Key", "a5fedf3644mshc69b93f47b8bfe0p134310jsn642508808f4f" },
                { "X-RapidAPI-Host", "fresh-linkedin-profile-data.p.rapidapi.com" },
            },
        };
        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        InfoDto? employeeDto = await response.Content.ReadFromJsonAsync<InfoDto>();
        return new EmployeeDto
        {
            FullName = employeeDto.Data.FullName,
            Skills = employeeDto.Data.Skills
        };
    }
}





