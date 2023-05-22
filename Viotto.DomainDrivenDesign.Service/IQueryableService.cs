using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Service;


public interface IQueryableService<TModel, TId>
    where TModel : IEntity<TId>
{
    IQueryable<TModel> GetAll();
    IQueryable<TModel> GetAllNoTracking();

    TModel GetById(TId id);
    Task<TModel> GetByIdAsync(TId id);

    TModel GetByIdNoTracking(TId id);
    Task<TModel> GetByIdNoTrackingAsync(TId id);

    IQueryable<TModel> GetByIds(IEnumerable<TId> ids);
    IQueryable<TModel> GetByIdsNoTracking(IEnumerable<TId> ids);
}
