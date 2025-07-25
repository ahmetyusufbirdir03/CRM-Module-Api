using Domain.Entities;

namespace Applicaton.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetByPhoneNumberAsync(string phoneNumber);
}
