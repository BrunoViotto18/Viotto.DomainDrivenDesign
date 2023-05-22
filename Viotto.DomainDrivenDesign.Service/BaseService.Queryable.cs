using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public IQueryable<TModel> GetAll()
        => Repository.GetAll();

    public IQueryable<TModel> GetAllNoTracking()
        => Repository.GetAllNoTracking();

    public TModel GetById(TId id)
        => Repository.GetById(id);

    public async Task<TModel> GetByIdAsync(TId id)
        => await Repository.GetByIdAsync(id);

    public TModel GetByIdNoTracking(TId id)
        => Repository.GetByIdNoTracking(id);

    public async Task<TModel> GetByIdNoTrackingAsync(TId id)
        => await Repository.GetByIdNoTrackingAsync(id);

    public IQueryable<TModel> GetByIds(IEnumerable<TId> ids)
        => Repository.GetByIds(ids);

    public IQueryable<TModel> GetByIdsNoTracking(IEnumerable<TId> ids)
        => GetByIdsNoTracking(ids);
}
