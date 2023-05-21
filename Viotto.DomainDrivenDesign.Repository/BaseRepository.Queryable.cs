using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
	where TContext : DbContext
	where TModel : class, IEntity<TId>
{
    //! GetAll

    public IQueryable<TModel> GetAll()
    {
        return Table;
    }

    public IQueryable<TModel> GetAllNoTracking()
    {
        return GetAll()
            .AsNoTracking();
    }

    //! GetById

    public TModel GetById(TId id)
    {
        return GetByIdAsync(id).Result;
    }

    public async Task<TModel> GetByIdAsync(TId id)
    {
        return await GetAll()
            .FirstAsync(x => x.Id.Equals(id));
    }

    //! GetByIdNoTracking

    public TModel GetByIdNoTracking(TId id)
    {
        return GetByIdNoTrackingAsync(id).Result;
    }

    public async Task<TModel> GetByIdNoTrackingAsync(TId id)
    {
        return await GetAllNoTracking()
            .FirstAsync(x => x.Id.Equals(id));
    }

    //! GetByIds

    public IQueryable<TModel> GetByIds(IEnumerable<TId> ids)
    {
        return GetAll()
            .Where(x => ids.Contains(x.Id));
    }

    public IQueryable<TModel> GetByIdsNoTracking(IEnumerable<TId> ids)
    {
        return GetByIds(ids)
            .AsNoTracking();
    }
}
