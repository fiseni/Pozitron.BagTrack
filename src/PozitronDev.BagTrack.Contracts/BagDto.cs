namespace PozitronDev.BagTrack.Contracts;

public class BagDto
{
    public required string BagTagId { get; set; }
    public required string DeviceId { get; set; }
    public required DateTime Date { get; set; }
    public required string? Carousel { get; set; }
    public required string? AirlineIATA { get; set; }
    public required string? Flight { get; set; }
}
