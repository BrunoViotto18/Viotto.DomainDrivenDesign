using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public void Delete(TModel model)
    {
        Repository.Remove(model);
        Repository.SaveChanges();
    }

    public async Task DeleteAsync(TModel model)
    {
        Repository.Remove(model);
        await Repository.SaveChangesAsync();
    }

    public void DeleteById(TId id)
    {
        Repository.RemoveById(id);
        Repository.SaveChanges();
    }

    public async Task DeleteByIdAsync(TId id)
    {
        Repository.RemoveById(id);
        await Repository.SaveChangesAsync();
    }

    public void DeleteRange(IEnumerable<TModel> models)
    {
        Repository.BulkRemove(models);
        Repository.SaveChanges();
    }

    public async Task DeleteRangeAsync(IEnumerable<TModel> models)
    {
        Repository.BulkRemove(models);
        await Repository.SaveChangesAsync();
    }

    public void DeleteRangeById(IEnumerable<TId> ids)
    {
        Repository.BulkRemoveById(ids);
        Repository.SaveChanges();
    }

    public async Task DeleteRangeByIdAsync(IEnumerable<TId> ids)
    {
        Repository.BulkRemoveById(ids);
        await Repository.SaveChangesAsync();
    }
}
