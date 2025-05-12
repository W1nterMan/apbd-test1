using Microsoft.AspNetCore.Mvc;
using Test1.Model;
using Test1.Services;

namespace Test1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentsService _appointmentsService;

    public AppointmentsController(IAppointmentsService appointmentsService)
    {
        _appointmentsService = appointmentsService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(int id)
    {
        try
        {
            var res = await _appointmentsService.GetAppointmentInfo(id);
            return Ok(res);

        }
        catch(Exception e)
        {
            return NotFound(e.Message);
        }
    }
    //dont work, had no time
    
    [HttpPost]
    public async Task<IActionResult> PostAppointment([FromBody] PostAppointmentDto postAppointmentDto)
    {
        
    }
}