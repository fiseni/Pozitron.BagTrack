using AutoMapper;
using PozitronDev.BagTrack.Infrastructure;
using PozitronDev.Extensions.Data;
using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.CommissionPayment.Infrastructure;

public class ReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class
{
    public ReadRepository(BagTrackDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }
}
