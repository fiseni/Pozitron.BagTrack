using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PozitronDev.BagTrack.Setup.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace PozitronDev.BagTrack.Api.Bags;

[ApiController]
public class BagsController : ControllerBase
{
    [ApiKey]
    [AllowAnonymous]
    [HttpGet("bagmanager/services/tracking/arrTracking/{bagTagId}")]
    [SwaggerOperation(Summary = "Get Bag information by tag", Tags = new[] { "Bag Manager" })]
    public async Task<ActionResult<BagDto>> GetByTagId(string bagTagId, DateOnly? date, [FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        var request = new BagGetRequest
        {
            BagTagId = bagTagId,
            Date = date
        };
        return Ok(await mediator.Send(request, cancellationToken));
    }

    [ApiKey]
    [AllowAnonymous]
    [HttpGet("bagmanager/services/tracking/arrTracking")]
    [SwaggerOperation(Summary = "Get Bag information by filter", Tags = new[] { "Bag Manager" })]
    public async Task<ActionResult<PagedResponse<BagDto>>> Get([FromQuery] BagListRequest request, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(request, cancellationToken));

    [ApiKey]
    [AllowAnonymous]
    [HttpPost("bagmanager/services/tracking/arrTracking")]
    [SwaggerOperation(Summary = "Add Bag information", Tags = new[] { "Bag Manager" })]
    public async Task<ActionResult<BagDto>> Create(BagCreateRequest request, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(request, cancellationToken));

    [HttpGet("bags")]
    [SwaggerOperation(Summary = "Get Bag information by filter", Tags = new[] { "Bags" })]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<PagedResponse<BagDto>>> List([FromQuery] BagListRequest request, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(request, cancellationToken));
}
