using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Service;


public interface IQueryableService<TModel, TId>
    where TModel : IEntity<TId>
{
    IQueryable<TModel> GetAll();
    IQueryable<TModel> GetAllNoTracking();

    IQueryable<TModel> GetById(TId id);
    IQueryable<TModel> GetByIdNoTracking(TId id);
}
