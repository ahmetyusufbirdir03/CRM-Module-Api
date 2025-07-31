using Applicaton.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<AppUser> _dbSet;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<AppUser>();
    }
    public async Task<AppUser?> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
}
