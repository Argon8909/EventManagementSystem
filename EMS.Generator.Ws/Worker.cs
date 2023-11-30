using System.Text;
using Newtonsoft.Json;

namespace WorkerService;

public class Worker : BackgroundService, IEventGeneratorService
{
    private readonly ILogger<Worker> _logger;
    private readonly Random _random;


    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _random = new Random();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await GenerateEvent();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Ошибка при отправке запроса: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromMilliseconds(_random.Next(1, 2000)));
        }
    }

    // Ваш метод GenerateEvent
    public async Task GenerateEvent()
    {
        var eventType = (EventTypeEnum)_random.Next(1, 4);
        var generatedEvent = new Event
        {
            Id = Guid.NewGuid(),
            Type = eventType,
            Time = DateTime.UtcNow
        };
        _logger.LogInformation(
            $"Сгенерировано событие: {generatedEvent.Id}, Тип: {generatedEvent.Type}, Время: {generatedEvent.Time}");

        var content = JsonConvert.SerializeObject(generatedEvent);
        Console.WriteLine("content: " + content);

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7022/");

            try
            {
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/Incident", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: " + result);
                    _logger.LogInformation("Result: " + result);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    _logger.LogInformation($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Ошибка при отправке запроса: {ex.Message}");
                throw;
            }
        }
    }
}

/*
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await GenerateEvent();
            await Task.Delay(TimeSpan.FromMilliseconds(_random.Next(1, 2000)), stoppingToken);
        }
    }

    public async Task GenerateEvent()
    {
        var @eventType = (EventTypeEnum)_random.Next(1, 4);
        var generatedEvent = new Event
        {
            Id = Guid.NewGuid(),
            Type = eventType,
            Time = DateTime.UtcNow
        };
        _logger.LogInformation(
            $"Сгенерировано событие: {generatedEvent.Id}, Тип: {generatedEvent.Type}, Время: {generatedEvent.Time}");

        var content = new StringContent(JsonConvert.SerializeObject(generatedEvent), Encoding.UTF8, "application/json");
        Console.WriteLine("content: " + content);
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7022/");

            var response = await client.PostAsync("api/Incident", content);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Result: " + result);
                _logger.LogInformation("Result: " + result);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                _logger.LogInformation($"Error: {response.StatusCode}");
            }
        }
    }
    */