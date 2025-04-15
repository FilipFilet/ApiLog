using ApiLog.Models;
using Microsoft.EntityFrameworkCore;
namespace ApiLog.Services;

public class EmployeeService
{
    private readonly DataContext _context;

    public EmployeeService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeGetDTO>> GetAllEmployeesAsync()
    {
        return await _context.Employees
        .Select(e => new EmployeeGetDTO // Selecting the properties and mapping them to EmployeeGetDTO
        {
            Id = e.Id,
            EmployeeName = e.EmployeeName,
            IsManager = e.IsManager,
            ManagerName = e.Manager.EmployeeName, // Include the manager of this employee
            OrganizationName = e.Organization.OrganizationName, // Include the organization of this employee
            Employees = e.Employees.Select(e => new SubEmployeeGetDTO
            {
                Id = e.Id,
                EmployeeName = e.EmployeeName,
                IsManager = e.IsManager,
                ManagerName = e.Manager.EmployeeName, // Include the manager of this employee
                OrganizationName = e.Organization.OrganizationName // Include the organization of this employee
            }).ToList() // Include the employees managed by this employee
        })
        .ToListAsync();
    }

    public async Task CreateEmployeeAsync(Employee employee)
    {
        if (employee.ManagerId == 0)
        {
            employee.ManagerId = null; // Set to null if no manager is assigned
        }

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<EmployeeGetDTO?> GetEmployeeByIdAsync(int id)
    {
        return await _context.Employees
        .Include(e => e.Organization) // Include the organization of this employee
        .Select(e => new EmployeeGetDTO // Selecting the properties and mapping them to EmployeeGetDTO
        {
            Id = e.Id,
            EmployeeName = e.EmployeeName,
            IsManager = e.IsManager,
            ManagerName = e.Manager.EmployeeName, // Include the manager of this employee
            OrganizationName = e.Organization.OrganizationName, // Include the organization of this employee
            Employees = e.Employees.Select(e => new SubEmployeeGetDTO
            {
                Id = e.Id,
                EmployeeName = e.EmployeeName,
                IsManager = e.IsManager,
                ManagerName = e.Manager.EmployeeName, // Include the manager of this employee
                OrganizationName = e.Organization.OrganizationName // Include the organization of this employee
            }).ToList() // Include the employees managed by this employee
        })
        .FirstOrDefaultAsync(e => e.Id == id); // Find the employee by ID
    }

    public async Task UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            throw new Exception("Employee not found");
        }

        if (employeeDTO.ManagerId == 0)
        {
            employeeDTO.ManagerId = null; // Set to null if no manager is assigned
        }

        employee.EmployeeName = employeeDTO.EmployeeName;
        employee.IsManager = employeeDTO.IsManager;
        employee.ManagerId = employeeDTO.ManagerId;
        employee.OrganizationId = employeeDTO.OrganizationId;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}