using Microsoft.EntityFrameworkCore;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TModel, TId> : IQueryableRepository<TModel, TId>
    where TModel : class, IEntity<TId>, new()
{
    public IQueryable<TModel> GetAll()
    {
        return Context.Set<TModel>();
    }

    public IQueryable<TModel> GetAllNoTracking()
    {
        return GetAll().AsNoTracking();
    }

    public IQueryable<TModel> GetById(TId id)
    {
        return GetAll().Where(x => x.Id.Equals(id));
    }

    public IQueryable<TModel> GetByIdNoTracking(TId id)
    {
        return GetAllNoTracking().Where(x => x.Id.Equals(id));
    }
}
