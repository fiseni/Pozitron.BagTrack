using Microsoft.AspNetCore.Mvc;
using PozitronDev.BagTrack.Api.Handlers;
using Swashbuckle.AspNetCore.Annotations;

namespace PozitronDev.BagTrack.Api;

[ApiController]
public class BagTrackController : ControllerBase
{
    [HttpGet("bagmanager/services/tracking/arrTracking")]
    [SwaggerOperation(Summary = "Get Bag information", Tags = new[] { "Arrival Tracking" })]
    public async Task<ActionResult<BagDto>> List(string bagTrackId, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new BagGetRequest(bagTrackId), cancellationToken));

    [HttpPost("bagmanager/services/tracking/arrTracking")]
    [SwaggerOperation(Summary = "Add Bag information", Tags = new[] { "Arrival Tracking" })]
    public async Task<ActionResult<BagDto>> List(BagCreateDto bagCreateDto, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new BagCreateRequest(bagCreateDto), cancellationToken));
}
