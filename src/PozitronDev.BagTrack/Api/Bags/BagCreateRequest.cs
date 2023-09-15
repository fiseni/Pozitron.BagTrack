namespace PozitronDev.BagTrack.Api.Bags;

public record BagCreateRequest : IRequest<BagDto>
{
    public required string BagTrackId { get; set; }
    public required string DeviceId { get; set; }
    public string? IsResponseNeeded { get; set; }
    public string? JulianDate { get; set; }
}

