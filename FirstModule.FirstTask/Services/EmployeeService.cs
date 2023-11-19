using FirstModel.FirstTask.Abstract;
using FirstModule.Dtos;
using FirstModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstModule;

public class EmployeeService
{
    private readonly ApplicationContext _context;
    private readonly IApiService _apiService;

    public EmployeeService(ApplicationContext context, IApiService apiService)
    {
        _context = context;
        _apiService = apiService;
    }

    public async Task SaveInfo(string url)
    {
        var employeeDto = await _apiService.GetInfo(url);

        EmployeeEntity employeeEntity = new EmployeeEntity
        {
            FullName = employeeDto.FullName
        };

        SkillEntity skillEntity = new SkillEntity
        {
            Skills = employeeDto.Skills,
            EmployeeId = employeeEntity.Id
        };

        await _context.Skills.AddAsync(skillEntity);
        await _context.Employies.AddAsync(employeeEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<EmployeeResponseDto>> GetAll()
    {
        return await _context
            .Employies
            .Join(_context.Skills,
                x => x.Id,
                y => y.EmployeeId,
                (x, y)
                    => new EmployeeResponseDto
                    {
                        FullName = x.FullName,
                        Skills = y.Skills,
                        Id = x.Id
                    })
            .ToListAsync();
    }

    public async Task<List<EmployeeResponseDto>> GetByName(string name)
    {
        return await _context
            .Employies
            .Join(_context.Skills,
                x => x.Id,
                y => y.EmployeeId,
                (x, y)
                    => new EmployeeResponseDto
                    {
                        FullName = x.FullName,
                        Skills = y.Skills,
                        Id = x.Id
                    })
            .Where(x => x.FullName.ToLower()
                .Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<List<EmployeeResponseDto>> GetBySkills(string skill)
    {
        var joint = await _context
            .Employies
            .Join(_context.Skills,
                x => x.Id,
                y => y.EmployeeId,
                (x, y)
                    => new EmployeeResponseDto
                    {
                        FullName = x.FullName,
                        Skills = y.Skills,
                        Id = x.Id
                    })
            .ToListAsync();

        return joint.Where(x => x.Skills.ToLower().Contains(skill.ToLower())).ToList();
    }
}