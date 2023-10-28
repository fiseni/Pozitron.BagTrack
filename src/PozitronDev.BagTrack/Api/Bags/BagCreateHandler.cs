using Microsoft.EntityFrameworkCore;

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

        var utcNow = _dateTime.UtcNow;

        var flightInfo = airlineIATA is null
            ? await GetFlightInfoFromRecentBag(carousel, utcNow, cancellationToken)
            : await GetFlightInfo(airlineIATA, carousel, utcNow, cancellationToken);

        var bag = new Bag(
            utcNow,
            request.BagTagId,
            request.DeviceId,
            carousel,
            flightInfo,
            request.IsResponseNeeded,
            request.JulianDate
        );

        _dbContext.Bags.Add(bag);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return bag.MapToBagDto();
    }

    private async Task<IEnumerable<FlightInfo>> GetFlightInfo(string? airlineIATA, string? carousel, DateTime utcNow, CancellationToken cancellationToken)
    {
        var flights = await _dbContext.Flights
            .Where(x => x.Start != null && x.Stop != null && x.Start <= utcNow && x.Stop >= utcNow)
            .Where(x => x.AirlineIATA == airlineIATA)
            .Where(x => x.ActiveCarousel == carousel)
            .Select(x => new
            {
                x.AirlineIATA,
                x.Number,
                x.Agent,
                x.Stop,
                x.FirstBag,
                x.LastBag
            })
            .ToListAsync(cancellationToken);

        if (flights.Count > 1)
        {
            flights = flights.Where(x => x.FirstBag <= utcNow && x.Stop >= utcNow && x.LastBag == null).ToList();
        }

        var result = flights.Count > 0
            ? flights.Take(4).Select(x => new FlightInfo(x.AirlineIATA, x.Number, x.Agent))
            : await GetFlightInfoFromRecentBag(carousel, utcNow, cancellationToken);

        return result;
    }

    private async Task<List<FlightInfo>> GetFlightInfoFromRecentBag(string? carousel, DateTime utcNow, CancellationToken cancellationToken)
    {
        var date = utcNow.AddMinutes(-5);

        var flightInfo = await _dbContext.Bags
            .Where(x => x.Carousel == carousel)
            .Where(x => x.Date > date)
            .OrderByDescending(x => x.Date)
            .Take(1)
            .Select(x => new FlightInfo(x.AirlineIATA, x.Flight, x.Agent))
            .ToListAsync(cancellationToken);

        return flightInfo;
    }
}
