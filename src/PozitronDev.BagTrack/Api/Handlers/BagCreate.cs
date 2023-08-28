namespace PozitronDev.BagTrack.Api.Handlers;

public record BagCreateRequest(BagCreateDto BagCreateDto) : IRequest<BagDto>;

public class BagCreateHandler : IRequestHandler<BagCreateRequest, BagDto>
{
    private readonly BagTrackDbContext _dbContext;

    public BagCreateHandler(BagTrackDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<BagDto> Handle(BagCreateRequest request, CancellationToken cancellationToken)
    {
        var create = request.BagCreateDto;

        var bag = new Bag(create.BagTrackId, create.DeviceId, create.IsResponseNeeded, create.JulianDate);

        _dbContext.Bags.Add(bag);
        await _dbContext.SaveChangesAsync();

        return new BagDto
        {
            BagTrackId = bag.BagTrackId,
            DeviceId = bag.DeviceId,
            JulianDate = bag.JulianDate,
        };
    }
}
