using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public void Create(TModel model)
        => Repository.Create(model);

    public async Task CreateAsync(TModel model)
        => await Repository.CreateAsync(model);

    public void CreateRange(IEnumerable<TModel> models)
        => Repository.CreateRange(models);

    public async Task CreateRangeAsync(IEnumerable<TModel> models)
        => await Repository.CreateRangeAsync(models);
}
