namespace CrmModuleApi.Shared.Bases;

public interface IBaseRules<T> where T : class
{
    Task<T> GetIfIdExistAsync(int id);
    Task<bool> IsIdExistAsync(int id);
}
