using System.ComponentModel.DataAnnotations;
using FirstModule.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstModule.Controllers;

public class EmployeeController : ControllerBase
{
    private readonly ApplicationContext _context;
    private readonly EmployeeService _employeeService;

    public EmployeeController(ApplicationContext context, EmployeeService employeeService)
    {
        _context = context;
        _employeeService = employeeService;
    }

    [HttpPost("/api/employees")]
    public async Task<IActionResult> GetInfo([FromBody] string url)
    {
        try
        {
            await _employeeService.SaveInfo(url);
            return Ok("success");
        }
        catch (ArgumentException e) // TODO: Certain exception
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/api/employees/all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _employeeService.GetAll();

        return response.Count == 0 ? NoContent() : Ok(response);
    }

    [HttpGet("/api/employees/name")]
    public async Task<IActionResult> GetEmployee([FromQuery] string name)
    {
        var response = await _employeeService.GetByName(name);

        return response.Count == 0 ? NoContent() : Ok(response);
    }

    [HttpGet("/api/employees/skills")]
    public async Task<IActionResult> GetBySkills([FromQuery] string skill)
    {
        var response = await _employeeService.GetBySkills(skill);

        return response.Count == 0 ? NoContent() : Ok(response);
    }
}