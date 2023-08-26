namespace PozitronDev.BagTrack.Api.Models;

public class BagDto
{
    public required string BagTrackId { get; set; }
    public required string DeviceId { get; set; }
    public required string? IsResponseNeeded { get; set; }
    public required string? JulianDate { get; set; }
}
