using PozitronDev.BagTrack.Domain.Contracts;
using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.BagTrack.Api.Bags;

public record BagCreateRequest(BagCreateDto BagCreateDto) : IRequest<BagDto>;

public class BagCreateHandler : IRequestHandler<BagCreateRequest, BagDto>
{
    private readonly IDateTime _dateTime;
    private readonly IDeviceCache _deviceCache;
    private readonly BagTrackDbContext _dbContext;

    public BagCreateHandler(
        IDateTime dateTime,
        IDeviceCache deviceCache,
        BagTrackDbContext dbContext)
    {
        _dateTime = dateTime;
        _deviceCache = deviceCache;
        _dbContext = dbContext;
    }

    public async Task<BagDto> Handle(BagCreateRequest request, CancellationToken cancellationToken)
    {
        var createDto = request.BagCreateDto;

        var bag = new Bag(
            _dateTime,
            _deviceCache,
            createDto.BagTrackId,
            createDto.DeviceId,
            createDto.IsResponseNeeded,
            createDto.JulianDate
        );

        _dbContext.Bags.Add(bag);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return bag.MapToBagDto();
    }
}
