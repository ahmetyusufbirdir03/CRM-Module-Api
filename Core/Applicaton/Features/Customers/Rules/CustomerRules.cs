using Applicaton.DTOs;
using Applicaton.Interfaces.UnitOfWorks;
using Applicaton.Rules;
using Domain.Entities;

namespace Applicaton.Features.Customers.Rules;

public class CustomerRules : BaseRules<Customer>
{

    public CustomerRules(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public override Task<bool> IsIdExistAsync(int id)
    {
        return base.IsIdExistAsync(id);
    }
    public override Task<Customer> GetIfIdExistAsync(int id)
    {
        return base.GetIfIdExistAsync(id);
    }
    
}
