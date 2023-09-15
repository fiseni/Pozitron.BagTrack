namespace PozitronDev.BagTrack.Api.Bags;

public class BagDto
{
    public required string BagTrackId { get; set; }
    public required string DeviceId { get; set; }
    public required DateTime Date { get; set; }
    public required string? Carousel { get; set; }
    public required string? Flight { get; set; }
    public required string? Airline { get; set; }
}
