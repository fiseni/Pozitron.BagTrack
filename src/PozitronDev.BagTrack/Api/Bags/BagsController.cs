using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PozitronDev.BagTrack.Api.Bags;

[ApiController]
public class BagsController : ControllerBase
{
    [HttpGet("bags")]
    [SwaggerOperation(Summary = "Get Bag information", Tags = new[] { "Bags" })]
    public async Task<ActionResult<BagDto>> List([FromQuery] BagListRequest request, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(request, cancellationToken));
}
