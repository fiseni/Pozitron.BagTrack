using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Api.Bags;

public class BagGetHandler : IRequestHandler<BagGetRequest, BagDto>
{
    private readonly BagTrackDbContext _dbContext;

    public BagGetHandler(BagTrackDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<BagDto> Handle(BagGetRequest request, CancellationToken cancellationToken)
    {
        var date = request.Date is null
            ? DateTime.UtcNow.Date
            : request.Date.Value.ToDateTime(TimeOnly.MinValue);

        var result = await _dbContext.Bags
            .Where(x => x.Date == date)
            .Where(x => x.BagTagId == request.BagTagId)
            .Select(BagDtoMapper.Expression)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.BagTagId, result);

        return result;
    }
}
