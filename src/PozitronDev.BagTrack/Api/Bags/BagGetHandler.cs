using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Api.Bags;

public class BagGetHandler : IRequestHandler<BagGetRequest, BagDto>
{
    private readonly BagTrackDbContext _dbContext;
    private readonly IDateTime _dateTime;

    public BagGetHandler(BagTrackDbContext dbContext, IDateTime dateTime)
    {
        _dbContext = dbContext;
        _dateTime = dateTime;
    }

    public async Task<BagDto> Handle(BagGetRequest request, CancellationToken cancellationToken)
    {
        var date = request.Date is null
            ? _dateTime.UtcNow.Date
            : request.Date.Value.Date;

        var nextDay = date.AddDays(1);

        var result = await _dbContext.Bags
            .Where(x => x.Date >= date && x.Date < nextDay)
            .Where(x => x.BagTagId == request.BagTagId)
            .Select(BagDtoMapper.Expression)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.BagTagId, result);

        return result;
    }
}
