using FirstModule.Dtos;

namespace FirstModel.FirstTask.Abstract;

public interface IApiService
{
    public Task<EmployeeDto> GetInfo(string url);

}