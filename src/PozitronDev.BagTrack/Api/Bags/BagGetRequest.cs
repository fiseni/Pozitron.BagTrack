namespace PozitronDev.BagTrack.Api.Bags;

public record BagGetRequest : IRequest<BagDto>
{
    public DateOnly? Date { get; set; }
    public string? BagTagId { get; set; }
    public string? Carousel { get; set; }
    public string? Flight { get; set; }
    public string? Airline { get; set; }
}
