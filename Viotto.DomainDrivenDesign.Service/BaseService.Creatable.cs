using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public void Create(TModel model)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(TModel model)
    {
        throw new NotImplementedException();
    }

    public void CreateRange(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public Task CreateRangeAsync(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }
}
