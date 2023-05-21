using Microsoft.EntityFrameworkCore;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
	where TContext : DbContext
	where TModel : class, IEntity<TId>
{
    public IQueryable<TModel> GetAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TModel> GetAllNoTracking()
    {
        throw new NotImplementedException();
    }

    public TModel GetById(TId id)
    {
        throw new NotImplementedException();
    }

    public async Task<TModel> GetByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public TModel GetByIdNoTracking(TId id)
    {
        throw new NotImplementedException();
    }

    public async Task<TModel> GetByIdNoTrackingAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TModel> GetByIds(IEnumerable<TId> ids)
    {
        throw new NotImplementedException();
    }

    public IQueryable<TModel> GetByIdsNoTracking(IEnumerable<TId> ids)
    {
        throw new NotImplementedException();
    }
}
