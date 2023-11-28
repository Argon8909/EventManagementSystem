namespace EMS.Core.Models;

public class Incident
{
    public Guid Id { get; set; }
    public EventTypeEnum Type { get; set; }
    public DateTime Time { get; set; }
}