using Microsoft.AspNetCore.Mvc;
using ApiLog.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiLog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogController : ControllerBase
{
    private readonly DataContext _context;

    public LogController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetLogs()
    {
        if (_context.Logs == null || _context.Logs.Count() == 0)
        {
            return NotFound("Logs not found.");
        }


        var logs = await _context.Logs.Select(log => new LogDTO
        {
            Id = log.Id,
            Message = log.Message,
            Level = log.Level,
            Timestamp = log.Timestamp,
            Exception = log.Exception
        }).ToListAsync();

        return Ok(logs);
    }
}
