using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    public virtual void Delete(TModel model)
    {
        throw new NotImplementedException();
    }

    public virtual async Task DeleteAsync(TModel model)
    {
        throw new NotImplementedException();
    }

    public virtual void DeleteById(TId id)
    {
        throw new NotImplementedException();
    }

    public virtual async Task DeleteByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public virtual void DeleteRange(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public virtual void DeleteRangeById(IEnumerable<TId> ids)
    {
        throw new NotImplementedException();
    }

    public virtual async Task DeleteRangeByIdAsync(IEnumerable<TId> ids)
    {
        throw new NotImplementedException();
    }
}
