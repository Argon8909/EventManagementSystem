namespace EMS.Bll;

//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class EventGeneratorService : BackgroundService
{
    private readonly ILogger<EventGeneratorService> _logger;
    private readonly IEventProcessorClient _eventProcessorClient;
    private readonly Random _random;

    public EventGeneratorService(
        ILogger<EventGeneratorService> logger,
        IEventProcessorClient eventProcessorClient)
    {
        _logger = logger;
        _eventProcessorClient = eventProcessorClient;
        _random = new Random();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Генерация события
            var eventType = _random.Next(1, 5); // Замените на свои нужды
            var generatedEvent = new Event
            {
                Id = Guid.NewGuid(),
                Type = (EventTypeEnum)eventType,
                Time = DateTime.UtcNow
            };

            // Отправка события процессору
            await _eventProcessorClient.SendEventAsync(generatedEvent);

            _logger.LogInformation($"Сгенерировано событие: {generatedEvent.Id}, Тип: {generatedEvent.Type}, Время: {generatedEvent.Time}");

            // Пауза на 2 секунды
            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
        }
    }
}
