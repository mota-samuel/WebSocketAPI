using Microsoft.AspNetCore.Mvc;
using WebSocket_Domain.ServicesConnection;

namespace WebSocket_API.Controllers;
[Route("[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly IObsService _obs;

    public StatusController(IObsService obs)
    {
        _obs = obs;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            connected = _obs.IsConnected(),
            recording = _obs.IsRecording()
        });
    }
}
