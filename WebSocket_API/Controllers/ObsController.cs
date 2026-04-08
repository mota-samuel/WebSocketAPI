using Microsoft.AspNetCore.Mvc;
using WebSocket_Application.UseCases;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebSocket_API.Controllers;
[Route("[controller]")]
[ApiController]
public class ObsController : ControllerBase
{
    private readonly RecordingService _service;
    private readonly string _token = "123abc";

    public ObsController(RecordingService service)
    {
        _service = service;
    }

    [HttpGet("start")]
    public IActionResult Start([FromQuery] string t)
    {
        if (t != _token)
            return Unauthorized();

        var result = _service.Start();
        return Ok(result);
    }

    [HttpGet("stop")]
    public IActionResult Stop([FromQuery] string t)
    {
        if (t != _token)
            return Unauthorized();

        var result = _service.Stop();
        return Ok(result);
    }

    [HttpGet("scene")]
    public IActionResult ChangeScene([FromQuery] string name, [FromQuery] string t)
    {
        if (t != _token)
            return Unauthorized();

        _service.ChangeScene(name);
        return Ok($"Cena alterada para {name}");
    }
}
