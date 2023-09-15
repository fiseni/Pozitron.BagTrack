using System.Linq.Expressions;

namespace PozitronDev.BagTrack.Api.Bags;

public static class BagDtoMapper
{
    private static readonly Expression<Func<Bag, BagDto>> _expression = x => new BagDto
    {
        BagTagId = x.BagTagId,
        DeviceId = x.DeviceId,
        Date = DateOnly.FromDateTime(x.Date),
        Carousel = x.Carousel,
        Flight = x.Flight,
        Airline = x.Airline,
    };

    private static readonly Func<Bag, BagDto> _func = _expression.Compile();


    public static BagDto MapToBagDto(this Bag bag) => _func(bag);

    public static Expression<Func<Bag, BagDto>> Expression => _expression;
}
