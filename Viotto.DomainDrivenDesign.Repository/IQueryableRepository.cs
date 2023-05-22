using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface IQueryableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    IQueryable<TModel> GetAll();
    IQueryable<TModel> GetAllNoTracking();

    TModel GetById(TId id);
    Task<TModel> GetByIdAsync(TId id);

    TModel GetByIdNoTracking(TId id);
    Task<TModel> GetByIdNoTrackingAsync(TId id);

    IQueryable<TModel> GetRangeById(IEnumerable<TId> ids);
    IQueryable<TModel> GetRangeByIdNoTracking(IEnumerable<TId> ids);
}
