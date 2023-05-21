using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    public void Delete(TModel model)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(TModel model)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(TId id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteByIdAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public void DeleteRange(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteRangeAsync(IEnumerable<TModel> models)
    {
        throw new NotImplementedException();
    }

    public void DeleteRangeById(IEnumerable<TId> ids)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteRangeByIdAsync(IEnumerable<TId> ids)
    {
        throw new NotImplementedException();
    }
}
