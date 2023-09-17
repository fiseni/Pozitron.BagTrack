namespace PozitronDev.BagTrack.Contracts;

public class BagDto
{
    public required string BagTagId { get; set; }
    public required string DeviceId { get; set; }
    public required DateOnly Date { get; set; }
    public required string? Carousel { get; set; }
    public required string? Flight { get; set; }
    public required string? Airline { get; set; }
}
