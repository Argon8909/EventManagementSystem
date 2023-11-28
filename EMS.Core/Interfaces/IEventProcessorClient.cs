namespace EMS.Core.Interfaces;

public interface IEventProcessorClient
{
    Task SendEventAsync(Event generatedEvent);
}