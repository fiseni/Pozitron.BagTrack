using MediatR;
using PozitronDev.SharedKernel.Data;

namespace PozitronDev.BagTrack.Contracts;

public record BagListRequest : BaseFilter, IRequest<PagedResponse<BagDto>>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? BagTagId { get; set; }
    public string? Carousel { get; set; }
    public string? AirlineIATA { get; set; }
    public string? Flight { get; set; }
}
