﻿using Applicaton.Interfaces.Repositories;

namespace Applicaton.Interfaces.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<T> GetGenericRepository<T>() where T : class, new();
    ICustomerRepository CustomerRepository { get; }
    Task<int> SaveChangesAsync();
}
