using Microsoft.AspNetCore.Mvc;
using ApiLog.Services;
using ApiLog.Models;
using Microsoft.Extensions.Logging;

namespace ApiLog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly OrganizationService _organizationService;
    private readonly ILogger<OrganizationController> _logger;

    public OrganizationController(OrganizationService organizationService, ILogger<OrganizationController> logger)
    {
        _organizationService = organizationService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrganizations()
    {
        _logger.LogInformation("GET/ Fetched all organizations.");
        var organizations = await _organizationService.GetAllOrganizationsAsync();
        return Ok(organizations);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization(OrganizationDTO organizationDTO)
    {
        var organization = new Organization
        {
            OrganizationName = organizationDTO.OrganizationName,
            Employees = null
        };

        await _organizationService.CreateOrganizationAsync(organization);
        _logger.LogInformation("POST/ Created organization with id: {id}.", organization.Id);
        return CreatedAtAction(nameof(GetAllOrganizations), new { id = organization.Id }, organization);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(int id)
    {
        await _organizationService.DeleteOrganizationAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganization(int id, OrganizationDTO organizationDTO)
    {
        // Add try-catch block to handle exceptions
        await _organizationService.UpdateOrganizationAsync(id, organizationDTO);
        return NoContent();
    }

}
