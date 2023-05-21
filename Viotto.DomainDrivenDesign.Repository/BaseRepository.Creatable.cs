using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    public virtual void Create(TModel model)
    {
        throw new NotImplementedException();
    }

    public virtual async Task CreateAsync(TModel model)
    {
        throw new NotImplementedException();
    }

    public virtual void CreateRange(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public virtual async Task CreateRangeAsync(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }
}
