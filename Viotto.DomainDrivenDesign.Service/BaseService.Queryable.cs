using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public IQueryable<TModel> GetAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TModel> GetAllNoTracking()
    {
        throw new NotImplementedException();
    }

    public TModel GetById(TId id)
    {
        throw new NotImplementedException();
    }

    public Task<TModel> GetByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public TModel GetByIdNoTracking(TId id)
    {
        throw new NotImplementedException();
    }

    public Task<TModel> GetByIdNoTrackingAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TModel> GetByIds(IEnumerable<TId> ids)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TModel> GetByIdsNoTracking(IEnumerable<TId> ids)
    {
        throw new NotImplementedException();
    }
}
