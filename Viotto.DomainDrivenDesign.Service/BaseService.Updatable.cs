using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public void Update(TModel model)
    {
        Repository.Update(model);
        Repository.SaveChanges();
    }

    public async Task UpdateAsync(TModel model)
    {
        Repository.Update(model);
        await Repository.SaveChangesAsync();
    }

    public void UpdateRange(IEnumerable<TModel> models)
    {
        Repository.BulkUpdate(models);
        Repository.SaveChanges();
    }

    public async Task UpdateRangeAsync(IEnumerable<TModel> models)
    {
        Repository.BulkUpdate(models);
        await Repository.SaveChangesAsync();
    }
}
