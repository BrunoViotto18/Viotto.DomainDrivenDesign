using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
	where TContext : DbContext
	where TModel : class, IEntity<TId>
{
    //! GetAll

    public virtual IQueryable<TModel> GetAll()
    {
        return Table;
    }

    public virtual IQueryable<TModel> GetAllNoTracking()
    {
        return GetAll()
            .AsNoTracking();
    }

    //! GetById

    public virtual TModel GetById(TId id)
    {
        return GetByIdAsync(id).Result;
    }

    public virtual async Task<TModel> GetByIdAsync(TId id)
    {
        return await GetAll()
            .FirstAsync(x => x.Id.Equals(id));
    }

    //! GetByIdNoTracking

    public virtual TModel GetByIdNoTracking(TId id)
    {
        return GetByIdNoTrackingAsync(id).Result;
    }

    public virtual async Task<TModel> GetByIdNoTrackingAsync(TId id)
    {
        return await GetAllNoTracking()
            .FirstAsync(x => x.Id.Equals(id));
    }

    //! GetByIds

    public virtual IQueryable<TModel> GetByIds(IEnumerable<TId> ids)
    {
        return GetAll()
            .Where(x => ids.Contains(x.Id));
    }

    public virtual IQueryable<TModel> GetByIdsNoTracking(IEnumerable<TId> ids)
    {
        return GetByIds(ids)
            .AsNoTracking();
    }
}
