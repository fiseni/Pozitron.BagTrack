using AutoMapper;
using PozitronDev.Extensions.Data;

namespace PozitronDev.CommissionPayment.Infrastructure;

public class ReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class
{
    public ReadRepository(BagTrackDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }
}
