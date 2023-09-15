using Microsoft.AspNetCore.Mvc;
using PozitronDev.BagTrack.Api.Bags;
using PozitronDev.BagTrack.Setup.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace PozitronDev.BagTrack.Api.Configuration;


[ApiController]
public class ConfigurationController : ControllerBase
{
    [ApiKey]
    [HttpPost("configuration/reload")]
    [SwaggerOperation(Summary = "Reload app configuration", Tags = new[] { "Configuration" })]
    public async Task<ActionResult> List([FromServices] IMediator mediator, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new ConfigurationReloadRequest(), cancellationToken));
}
