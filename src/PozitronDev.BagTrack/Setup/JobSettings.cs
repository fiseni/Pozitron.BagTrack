namespace PozitronDev.BagTrack.Setup;

public class JobSettings
{
    public const string SECTION_NAME = "Jobs";

    public string? DashboardUsername { get; set; }
    public string? DashboardPassword { get; set; }

    public string? InputMQJob { get; set; }
}
