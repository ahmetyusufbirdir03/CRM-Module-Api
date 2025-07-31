using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Applicaton.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<bool> IsPhoneNumberExistAsync(string uniqueKey);
    Task<bool> IsEmailExistAsync(string uniqueKey);

    Task<Customer?> GetByPhoneNumberAsync(string phoneNumber);
    Task<Customer?> GetByEmailAsync(string email);

}
