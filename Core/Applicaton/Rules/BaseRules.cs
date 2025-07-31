using Applicaton.Interfaces.Repositories;
using Applicaton.Interfaces.UnitOfWorks;
using CrmModuleApi.Shared.Bases;

namespace Applicaton.Rules;

public abstract class BaseRules<T> : IBaseRules<T> where T : class, new()
{
    protected readonly IUnitOfWork unitOfWork;
    protected readonly IRepository<T> repository;

    protected BaseRules(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
        repository = unitOfWork.GetGenericRepository<T>();
    }

    public virtual async Task<T> GetIfIdExistAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity!; 
    }

    public virtual async Task<bool> IsIdExistAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity != null;
    }

}