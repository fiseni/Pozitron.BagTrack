using MediatR;

namespace PozitronDev.BagTrack.Contracts;

public record BagGetRequest : IRequest<BagDto>
{
    public DateOnly? Date { get; set; }
    public string? BagTagId { get; set; }
    public string? Carousel { get; set; }
    public string? Flight { get; set; }
    public string? Airline { get; set; }
}
