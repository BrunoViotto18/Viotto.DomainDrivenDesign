using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public void Delete(TModel model)
        => Repository.Delete(model);

    public async Task DeleteAsync(TModel model)
        => await Repository.DeleteAsync(model);

    public void DeleteById(TId id)
        => Repository.DeleteById(id);

    public async Task DeleteByIdAsync(TId id)
        => await Repository.DeleteByIdAsync(id);

    public void DeleteRange(IEnumerable<TModel> models)
        => Repository.DeleteRange(models);

    public async Task DeleteRangeAsync(IEnumerable<TModel> models)
        => await Repository.DeleteRangeAsync(models);

    public void DeleteRangeById(IEnumerable<TId> ids)
        => Repository.DeleteRangeById(ids);

    public async Task DeleteRangeByIdAsync(IEnumerable<TId> ids)
        => await Repository.DeleteRangeByIdAsync(ids);
}
