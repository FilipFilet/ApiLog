using Microsoft.AspNetCore.Mvc;
using ApiLog.Services;
using ApiLog.Models;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ApiLog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(EmployeeService employeeService, ILogger<EmployeeController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        _logger.LogInformation("GET/ Fetched all employees.");
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(EmployeeDTO employeeDTO)
    {
        Employee employee = new Employee()
        {
            EmployeeName = employeeDTO.EmployeeName,
            IsManager = employeeDTO.IsManager,
            ManagerId = employeeDTO.ManagerId,
            OrganizationId = employeeDTO.OrganizationId,
        };

        await _employeeService.CreateEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetAllEmployees), new { id = employee.Id }, employee);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {

        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            _logger.LogWarning("GET/ Employee with id {id} not found.", id);
            return NotFound();

        }
        _logger.LogInformation("GET/ Fetched employee with id {id}.", id);
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO employeeDTO)
    {
        await _employeeService.UpdateEmployeeAsync(id, employeeDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        await _employeeService.DeleteEmployeeAsync(id);
        return NoContent();
    }

}
