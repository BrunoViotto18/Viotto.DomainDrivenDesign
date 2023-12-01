using Microsoft.EntityFrameworkCore;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TModel, TId> : IDeletableRepository<TModel, TId>
    where TModel : class, IEntity<TId>, new()
{
    public void Remove(TModel model)
    {
        Context.Remove(model);
    }

    public void BulkRemove(IEnumerable<TModel> models)
    {
        Context.RemoveRange(models);
    }

    public void RemoveById(TId id)
    {
        var model = new TModel { Id = id };

        Remove(model);
    }

    public void BulkRemoveById(IEnumerable<TId> ids)
    {
        var models = ids.Select(id => new TModel { Id = id });

        BulkRemove(models);
    }
}
