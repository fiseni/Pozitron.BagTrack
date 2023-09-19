namespace PozitronDev.BagTrack.Infrastructure.MQ;

public class MQSettings
{
    public const string SECTION_NAME = "MQ";

    public string HostName { get; set; } = default!;
    public int Port { get; set; }
    public string Channel { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string QueueManagerName { get; set; } = default!;
    public string InputQueue { get; set; } = default!;
}
