namespace WorkerService;

public class Event
{
    public Guid Id { get; set; }
    public EventTypeEnum Type { get; set; }
    public DateTime Time { get; set; }
}