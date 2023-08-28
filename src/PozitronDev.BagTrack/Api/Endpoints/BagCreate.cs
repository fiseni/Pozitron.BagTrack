using MediatR;
using Microsoft.AspNetCore.Mvc;
using PozitronDev.BagTrack.Api.Models;
using PozitronDev.BagTrack.Domain;
using PozitronDev.BagTrack.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;

namespace PozitronDev.BagTrack.Api.Endpoints;

[ApiController]
public class BagCreate : ControllerBase
{
    [HttpPost("bagmanager/services/tracking/arrTracking")]
    [SwaggerOperation(Summary = "Add Bag information", Tags = new[] { "Arrival Tracking" })]
    public async Task<ActionResult<BagDto>> List(BagCreateDto bagCreateDto, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new BagCreateRequest(bagCreateDto), cancellationToken));
}

public record BagCreateRequest(BagCreateDto BagCreateDto) : IRequest<BagDto>;

public class ArrivalTrackingCreateHandler : IRequestHandler<BagCreateRequest, BagDto>
{
    private readonly BagTrackDbContext _dbContext;

    public ArrivalTrackingCreateHandler(BagTrackDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<BagDto> Handle(BagCreateRequest request, CancellationToken cancellationToken)
    {
        var create = request.BagCreateDto;

        var bag = new Bag(create.BagTrackId, create.DeviceId, create.IsResponseNeeded, create.JulianDate);

        _dbContext.Bags.Add(bag);
        await _dbContext.SaveChangesAsync();

        return new BagDto
        {
            BagTrackId = bag.BagTrackId,
            DeviceId = bag.DeviceId,
            JulianDate = bag.JulianDate,
        };
    }
}
