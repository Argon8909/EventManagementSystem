using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using EMS.Core.Enums;
using WorkerService.Generator;

namespace EMS.App.Generator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly ILogger<EventController> _logger;
    private readonly IEventGeneratorService _generatorService;

    public EventController(ILogger<EventController> logger, IEventGeneratorService generatorService)
    {
        _logger = logger;
        _generatorService = generatorService;
    }

    [HttpPost]
    public IActionResult GenerateEventManually()
    {
        _generatorService.GenerateEvent();
        _logger.LogInformation($"Произошло ручное создание события");
        return Ok("Произошло ручное создание события");
    }
}