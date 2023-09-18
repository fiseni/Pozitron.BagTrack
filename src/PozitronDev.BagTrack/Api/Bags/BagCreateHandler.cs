namespace PozitronDev.BagTrack.Api.Bags;

public class BagCreateHandler : IRequestHandler<BagCreateRequest, BagDto>
{
    private readonly IDateTime _dateTime;
    private readonly IDataCache _dataCache;
    private readonly BagTrackDbContext _dbContext;

    public BagCreateHandler(
        IDateTime dateTime,
        IDataCache dataCache,
        BagTrackDbContext dbContext)
    {
        _dateTime = dateTime;
        _dataCache = dataCache;
        _dbContext = dbContext;
    }

    public async Task<BagDto> Handle(BagCreateRequest request, CancellationToken cancellationToken)
    {
        var airlineBagCode = request.BagTagId[0..4];
        var airlineIATA = _dataCache.GetAirlineIATA(airlineBagCode);

        var carousel = _dataCache.GetCarousel(request.DeviceId);

        var bag = new Bag(
            _dateTime,
            request.BagTagId,
            request.DeviceId,
            carousel,
            airlineIATA,
            null,
            request.IsResponseNeeded,
            request.JulianDate
        );

        _dbContext.Bags.Add(bag);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return bag.MapToBagDto();
    }
}
