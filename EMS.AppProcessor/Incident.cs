namespace EMS.App;

public class Incident
{
    public Guid Id { get; set; }
    public IncidentTypeEnum Type { get; set; }
    public DateTime Time { get; set; }
}