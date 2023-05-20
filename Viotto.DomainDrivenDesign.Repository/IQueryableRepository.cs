using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface IQueryableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public IQueryable<TModel> GetAll();
    public IQueryable<TModel> GetAllNoTracking();

    public TModel GetById(TId id);
    public TModel GetByIdNoTracking(TId id);
    public Task<TModel> GetByIdAsync(TId id);
    public Task<TModel> GetByIdNoTrackingAsync(TId id);

    public IQueryable<TModel> GetByIds(IEnumerable<TId> ids);
    public IQueryable<TModel> GetByIdsNoTracking(IEnumerable<TId> ids);
}
