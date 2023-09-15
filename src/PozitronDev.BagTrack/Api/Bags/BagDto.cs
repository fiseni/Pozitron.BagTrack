namespace PozitronDev.BagTrack.Api.Bags;

public class BagDto
{
    public required string BagTrackId { get; set; }
    public required string DeviceId { get; set; }
    public required DateTime Date { get; set; }
}
