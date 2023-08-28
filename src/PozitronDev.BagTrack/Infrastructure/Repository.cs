using AutoMapper;
using PozitronDev.Extensions.Data;
using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.CommissionPayment.Infrastructure;

public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
{
    public Repository(BagTrackDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }
}
