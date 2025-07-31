using Domain.Entities;

namespace Applicaton.Interfaces.Repositories;

public interface IUserRepository
{
    Task<AppUser> GetUserByPhoneNumberAsync(string phoneNumber);
}
