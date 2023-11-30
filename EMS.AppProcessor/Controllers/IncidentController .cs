using EMS.App.Generator;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EMS.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentController : Controller
{
    [HttpPost]
    public IActionResult Incident([FromBody] Event receivedEvent)
    {
        Console.WriteLine("Принято событие: " + JsonConvert.SerializeObject(receivedEvent));
        return Ok("Произошёл запрос к процессору!");
    }
}