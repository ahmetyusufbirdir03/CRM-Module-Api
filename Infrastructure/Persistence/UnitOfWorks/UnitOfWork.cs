using Applicaton.Interfaces.Repositories;
using Applicaton.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext dbContext;
    private readonly IHttpContextAccessor httpContextAccessor;
    private ICustomerRepository _customerRepository;

    private IUserRepository _userRepository;

    public ICustomerRepository CustomerRepository =>
    _customerRepository ??= new CustomerRepository(dbContext);

    public IUserRepository UserRepository =>
    _userRepository ??= new UserRepository(dbContext);


    public UnitOfWork(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.httpContextAccessor = httpContextAccessor;
    }
    public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();

    IRepository<T> IUnitOfWork.GetGenericRepository<T>() => new GenericRepository<T>(dbContext, httpContextAccessor);
    public async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();

}
