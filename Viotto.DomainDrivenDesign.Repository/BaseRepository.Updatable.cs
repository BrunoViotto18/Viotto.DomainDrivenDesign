using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    public virtual void Update(TModel model)
    {
        throw new NotImplementedException();
    }

    public virtual async Task UpdateAsync(TModel model)
    {
        throw new NotImplementedException();
    }

    public virtual void UpdateRange(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }
}
