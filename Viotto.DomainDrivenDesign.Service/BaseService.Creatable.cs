using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public void Create(TModel model)
    {
        Repository.Insert(model);
        Repository.SaveChanges();
    }

    public async Task CreateAsync(TModel model)
    {
        Repository.Insert(model);
        await Repository.SaveChangesAsync();
    }

    public void CreateRange(IEnumerable<TModel> models)
    {
        Repository.BulkInsert(models);
        Repository.SaveChanges();
    }

    public async Task CreateRangeAsync(IEnumerable<TModel> models)
    {
        Repository.BulkInsert(models);
        await Repository.SaveChangesAsync();
    }
}
