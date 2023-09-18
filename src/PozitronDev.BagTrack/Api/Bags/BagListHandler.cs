using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Api.Bags;

public class BagListHandler : IRequestHandler<BagListRequest, PagedResponse<BagDto>>
{
    private readonly BagTrackDbContext _dbContext;

    public BagListHandler(BagTrackDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<PagedResponse<BagDto>> Handle(BagListRequest request, CancellationToken cancellationToken)
    {
        var date = request.Date is null
            ? DateTime.UtcNow.Date
            : request.Date.Value.Date;

        var nextDay = date.AddDays(1);

        var query = _dbContext.Bags.Where(x => x.Date >= date && x.Date < nextDay);

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

        if (request.AirlineIATA is not null)
        {
            query = query.Where(x => x.AirlineIATA == request.AirlineIATA);
        }

        var count = await query.CountAsync(cancellationToken);
        var pagination = new Pagination(count, request);

        var data = await query
            .OrderBy(x => x.BagTagId)
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(BagDtoMapper.Expression)
            .ToListAsync(cancellationToken);

        return new PagedResponse<BagDto>(data, pagination);
    }
}
