using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface IQueryableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    IQueryable<TModel> GetAll();
    IQueryable<TModel> GetAllNoTracking();

    IQueryable<TModel> GetById(TId id);
    IQueryable<TModel> GetByIdNoTracking(TId id);
}
