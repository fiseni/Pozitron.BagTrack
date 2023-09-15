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
            ? DateTime.UtcNow 
            : request.Date.Value.ToDateTime(TimeOnly.MinValue);

        var query = _dbContext.Bags.Where(x => x.Date == date);

        var key = string.Empty;

        if (request.BagTrackId is not null)
        {
            query = query.Where(x => x.BagTrackId == request.BagTrackId);
            key = request.BagTrackId;
        }
        else if (request.Carousel is not null)
        {
            query = query.Where(x => x.Carousel == request.Carousel);
            key = request.Carousel;
        }
        else if (request.Flight is not null)
        {
            query = query.Where(x => x.Flight == request.Flight);
            key = request.Flight;
        }
        else if (request.Airline is not null)
        {
            query = query.Where(x => x.Airline == request.Airline);
            key = request.Airline;
        }

        var result = await query
            .Select(BagDtoMapper.Expression)
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(key, result);

        return result;
    }
}
