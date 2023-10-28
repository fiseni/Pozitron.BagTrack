namespace PozitronDev.BagTrack.Setup;

public class JobSettings
{
    public const string SECTION_NAME = "Jobs";

    public string? DashboardUsername { get; set; }
    public string? DashboardPassword { get; set; }

    public string? CleanBagsJob { get; set; }
    public string? CleanFlightsJob { get; set; }
    public string? CleanInboxMessagesJob { get; set; }
    public int CleanOlderThanDays { get; set; } = 30;
}
