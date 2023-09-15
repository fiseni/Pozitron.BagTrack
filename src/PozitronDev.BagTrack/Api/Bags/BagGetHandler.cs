using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Api.Bags;

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
                Date = x.Date,
            })
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.BagTagId, bagDto);

        return bagDto;
    }
}
