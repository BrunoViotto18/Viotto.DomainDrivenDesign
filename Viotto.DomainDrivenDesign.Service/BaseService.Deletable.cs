using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public void Delete(TModel model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TModel model)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(TId id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public void DeleteRange(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public void DeleteRangeById(IEnumerable<TId> ids)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeByIdAsync(IEnumerable<TId> ids)
    {
        throw new NotImplementedException();
    }
}
