using Applicaton.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<Customer> _dbSet;

    public CustomerRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<Customer>();
    }

    public async Task<bool> IsPhoneNumberExistAsync(string phoneNumber)
    {
        return await _dbSet.AnyAsync(c => c.PhoneNumber == phoneNumber);
    }

    public async Task<bool> IsEmailExistAsync(string email)
    {
        return await _dbSet.AnyAsync(c => c.Email == email);

    }

    public async Task<Customer?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
    }
}


