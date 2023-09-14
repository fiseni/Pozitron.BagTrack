using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Api.Handlers;

public record BagGetRequest(string BagTagId) : IRequest<BagDto>;

public class BagGetHandler : IRequestHandler<BagGetRequest, BagDto>
{
    private readonly BagTrackDbContext _dbContext;

    public BagGetHandler(BagTrackDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<BagDto> Handle(BagGetRequest request, CancellationToken cancellationToken)
    {
        var bagDto = await _dbContext.Bags
            .Where(b => b.BagTrackId == request.BagTagId)
            .Select(x => new BagDto
            {
                BagTrackId = x.BagTrackId,
                DeviceId = x.DeviceId,
                JulianDate = x.JulianDate,
            })
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.BagTagId, bagDto);

        return bagDto;
    }
}
