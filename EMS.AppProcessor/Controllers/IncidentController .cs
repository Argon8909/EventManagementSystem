using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WorkerService;

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
    
    // Логику процессора дописать не успел. Успел написать только генератор событий.
    
}