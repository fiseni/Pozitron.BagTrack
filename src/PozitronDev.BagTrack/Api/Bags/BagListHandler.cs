using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Api.Bags;

public class BagListHandler : IRequestHandler<BagListRequest, PagedResponse<BagDto>>
{
    private readonly BagTrackDbContext _dbContext;
    private readonly IDateTime _dateTime;

    public BagListHandler(BagTrackDbContext dbContext, IDateTime dateTime)
    {
        _dbContext = dbContext;
        _dateTime = dateTime;
    }

    public async Task<PagedResponse<BagDto>> Handle(BagListRequest request, CancellationToken cancellationToken)
    {
        var fromDate = request.FromDate is null
            ? _dateTime.UtcNow.Date
            : request.FromDate.Value;

        var toDate = request.ToDate is null
            ? fromDate.Date.AddDays(1).AddSeconds(-1)
            : request.ToDate.Value;

        var query = _dbContext.Bags.Where(x => x.Date >= fromDate && x.Date <= toDate);

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
            query = query.Where(x => x.Flight != null && x.Flight.Contains(request.Flight));
        }

        if (request.AirlineIATA is not null)
        {
            query = query.Where(x => x.AirlineIATA == request.AirlineIATA);
        }

        var count = await query.CountAsync(cancellationToken);
        var pagination = new Pagination(count, request);

        var data = await query
            .OrderByDescending(x => x.Date)
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(BagDtoMapper.Expression)
            .ToListAsync(cancellationToken);

        return new PagedResponse<BagDto>(data, pagination);
    }
}
