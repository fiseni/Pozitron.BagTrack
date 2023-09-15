using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.BagTrack.Api.Bags;

public record BagCreateRequest(BagCreateDto BagCreateDto) : IRequest<BagDto>;

public class BagCreateHandler : IRequestHandler<BagCreateRequest, BagDto>
{
    private readonly IDateTime _dateTime;
    private readonly BagTrackDbContext _dbContext;

    public BagCreateHandler(IDateTime dateTime, BagTrackDbContext dbContext)
    {
        _dateTime = dateTime;
        _dbContext = dbContext;
    }

    public async Task<BagDto> Handle(BagCreateRequest request, CancellationToken cancellationToken)
    {
        var createDto = request.BagCreateDto;

        var bag = new Bag(
            _dateTime,
            createDto.BagTrackId,
            createDto.DeviceId,
            createDto.IsResponseNeeded,
            createDto.JulianDate
        );

        _dbContext.Bags.Add(bag);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new BagDto
        {
            BagTrackId = bag.BagTrackId,
            DeviceId = bag.DeviceId,
            Date = bag.Date,
        };
    }
}
