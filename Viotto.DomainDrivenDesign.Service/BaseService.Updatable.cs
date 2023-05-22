using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public void Update(TModel model)
        => Repository.Update(model);

    public async Task UpdateAsync(TModel model)
        => await Repository.UpdateAsync(model);

    public void UpdateRange(IEnumerable<TModel> models)
        => Repository.UpdateRange(models);

    public async Task UpdateRangeAsync(IEnumerable<TModel> models)
        => await Repository.UpdateRangeAsync(models);
}
