using Microsoft.AspNetCore.Mvc;
using PozitronDev.BagTrack.Setup.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace PozitronDev.BagTrack.Api.Bags;

[ApiController]
public class BagTrackController : ControllerBase
{
    [ApiKey]
    [HttpGet("bagmanager/services/tracking/arrTracking")]
    [SwaggerOperation(Summary = "Get Bag information", Tags = new[] { "Arrival Tracking" })]
    public async Task<ActionResult<BagDto>> List(string bagTrackId, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new BagGetRequest(bagTrackId), cancellationToken));

    [ApiKey]
    [HttpPost("bagmanager/services/tracking/arrTracking")]
    [SwaggerOperation(Summary = "Add Bag information", Tags = new[] { "Arrival Tracking" })]
    public async Task<ActionResult<BagDto>> List(BagCreateDto bagCreateDto, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new BagCreateRequest(bagCreateDto), cancellationToken));
}
