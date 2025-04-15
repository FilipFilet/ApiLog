using Microsoft.EntityFrameworkCore;
using ApiLog.Models;
namespace ApiLog.Services;


public class OrganizationService
{
    private readonly DataContext _context;

    public OrganizationService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<OrganizationGetDTO>> GetAllOrganizationsAsync()
    {
        return await _context.Organizations
            .Include(o => o.Employees) // Include the employees in the organization
            .Select(o => new OrganizationGetDTO // Selecting the properties and mapping them to OrganizationGetDTO
            {
                Id = o.Id,
                OrganizationName = o.OrganizationName,
                EmployeeNames = o.Employees.Select(e => e.EmployeeName).ToList() // Include the employee names in the organization
            })
            .ToListAsync();
    }

    public async Task CreateOrganizationAsync(Organization organization)
    {
        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrganizationAsync(int id)
    {
        var organization = await _context.Organizations.FindAsync(id);
        if (organization == null)
        {
            throw new Exception("Organization not found");
        }

        _context.Organizations.Remove(organization);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrganizationAsync(int id, OrganizationDTO organizationDTO)
    {
        var organization = await _context.Organizations.FindAsync(id);

        if (organization == null)
        {
            throw new Exception("Organization not found");
        }

        organization.OrganizationName = organizationDTO.OrganizationName;

        _context.Organizations.Update(organization);
        await _context.SaveChangesAsync();
    }

}