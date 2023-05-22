using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public void Update(TModel model)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(TModel model)
    {
        throw new NotImplementedException();
    }

    public void UpdateRange(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateRangeAsync(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }
}
