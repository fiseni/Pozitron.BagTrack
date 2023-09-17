using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Api.Bags;

public class BagListHandler : IRequestHandler<BagListRequest, List<BagDto>>
{
    private readonly BagTrackDbContext _dbContext;

    public BagListHandler(BagTrackDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<List<BagDto>> Handle(BagListRequest request, CancellationToken cancellationToken)
    {
        var date = request.Date is null
            ? DateTime.UtcNow.Date
            : request.Date.Value.ToDateTime(TimeOnly.MinValue);

        var query = _dbContext.Bags.Where(x => x.Date == date);

        if (request.BagTagId is not null)
        {
            query = query.Where(x => x.BagTagId == request.BagTagId);
        }
        
        if (request.Carousel is not null)
        {
            query = query.Where(x => x.Carousel == request.Carousel);
        }
        
        if (request.Flight is not null)
        {
            query = query.Where(x => x.Flight == request.Flight);
        }
        
        if (request.Airline is not null)
        {
            query = query.Where(x => x.Airline == request.Airline);
        }

        var result = await query
            .Select(BagDtoMapper.Expression)
            .ToListAsync(cancellationToken);

        return result;
    }
}
