using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public IQueryable<TModel> GetAll()
    {
        return Repository.GetAll();
    }

    public IQueryable<TModel> GetAllNoTracking()
    {
        return Repository.GetAllNoTracking();
    }

    public IQueryable<TModel> GetById(TId id)
    {
        return Repository.GetById(id);
    }

    public IQueryable<TModel> GetByIdNoTracking(TId id)
    {
        return Repository.GetByIdNoTracking(id);
    }
}
