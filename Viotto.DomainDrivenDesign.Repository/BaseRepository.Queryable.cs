using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
	where TContext : DbContext
	where TModel : class, IEntity<TId>
{
    //! GetAll

    public IQueryable<TModel> GetAll()
        => Table;

    public IQueryable<TModel> GetAllNoTracking()
        => GetAll()
            .AsNoTracking();

    //! GetById

    public TModel GetById(TId id)
        => GetByIdAsync(id).Result;

    public async Task<TModel> GetByIdAsync(TId id)
        => await GetAll()
            .FirstAsync(x => x.Id.Equals(id));

    //! GetByIdNoTracking

    public TModel GetByIdNoTracking(TId id)
        => GetByIdNoTrackingAsync(id).Result;

    public async Task<TModel> GetByIdNoTrackingAsync(TId id)
        => await GetAllNoTracking()
            .FirstAsync(x => x.Id.Equals(id));

    //! GetByIds

    public IQueryable<TModel> GetByIds(IEnumerable<TId> ids)
        => GetAll()
            .Where(x => ids.Contains(x.Id));

    public IQueryable<TModel> GetByIdsNoTracking(IEnumerable<TId> ids)
        => GetByIds(ids)
            .AsNoTracking();
}
