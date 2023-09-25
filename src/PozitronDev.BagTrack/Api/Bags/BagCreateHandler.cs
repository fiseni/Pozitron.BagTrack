﻿using Microsoft.EntityFrameworkCore;

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

        var flights = await _dbContext.Flights
            .Where(x => x.Start != null && x.Stop != null && x.Start <= utcNow && x.Stop >= utcNow)
            .Where(x => x.AirlineIATA == airlineIATA)
            .Where(x => x.ActiveCarousel == carousel)
            .Take(4)
            .Select(x => x.Number)
            .ToArrayAsync();

        var bag = new Bag(
            utcNow,
            request.BagTagId,
            request.DeviceId,
            carousel,
            airlineIATA,
            flights,
            request.IsResponseNeeded,
            request.JulianDate
        );

        _dbContext.Bags.Add(bag);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return bag.MapToBagDto();
    }
}
