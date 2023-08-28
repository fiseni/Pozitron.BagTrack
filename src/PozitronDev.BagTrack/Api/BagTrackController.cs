using Microsoft.AspNetCore.Mvc;
using PozitronDev.BagTrack.Api.Handlers;
using Swashbuckle.AspNetCore.Annotations;

namespace PozitronDev.BagTrack.Api;

[ApiController]
public class BagTrackController : ControllerBase
{
    [HttpPost("bagmanager/services/tracking/arrTracking")]
    [SwaggerOperation(Summary = "Add Bag information", Tags = new[] { "Arrival Tracking" })]
    public async Task<ActionResult<BagDto>> List(BagCreateDto bagCreateDto, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new BagCreateRequest(bagCreateDto), cancellationToken));
}
