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
    public async Task<ActionResult<BagDto>> List([FromQuery] BagGetRequest request, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(request, cancellationToken));

    [ApiKey]
    [HttpPost("bagmanager/services/tracking/arrTracking")]
    [SwaggerOperation(Summary = "Add Bag information", Tags = new[] { "Arrival Tracking" })]
    public async Task<ActionResult<BagDto>> List(BagCreateRequest request, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(request, cancellationToken));
}
