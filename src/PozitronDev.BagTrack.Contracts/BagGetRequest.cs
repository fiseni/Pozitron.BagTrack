using MediatR;

namespace PozitronDev.BagTrack.Contracts;

public record BagGetRequest : IRequest<BagDto>
{
    public required string BagTagId { get; set; }
    public DateOnly? Date { get; set; }
}
