using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    public void Create(TModel model)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(TModel model)
    {
        throw new NotImplementedException();
    }

    public void CreateRange(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public async Task CreateRangeAsync(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }
}
