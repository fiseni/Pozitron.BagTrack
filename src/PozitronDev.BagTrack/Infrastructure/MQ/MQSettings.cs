namespace PozitronDev.BagTrack.Infrastructure.MQ;

public class MQSettings
{
    public required string HostName { get; set; }
    public required int Port { get; set; }
    public required string Channel { get; set; }
    public required string UserId { get; set; }
    public required string Password { get; set; }
    public required string QueueManagerName { get; set; }
    public required string Queue { get; set; }
}
