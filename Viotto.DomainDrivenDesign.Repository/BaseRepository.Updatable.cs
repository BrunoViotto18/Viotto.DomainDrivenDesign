using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    public void Update(TModel model)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(TModel model)
    {
        throw new NotImplementedException();
    }

    public void UpdateRange(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateRangeAsync(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }
}
