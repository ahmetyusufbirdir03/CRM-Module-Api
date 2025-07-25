using Applicaton.Interfaces.Repositories;
using Applicaton.Interfaces.UnitOfWorks;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext dbContext;
    private ICustomerRepository _customerRepository;

    public ICustomerRepository CustomerRepository =>
    _customerRepository ??= new CustomerRepository(dbContext);

    public UnitOfWork(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();

    IRepository<T> IUnitOfWork.GetGenericRepository<T>() => new GenericRepository<T>(dbContext);
    public async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();

}
