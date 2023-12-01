using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TModel, TId> : IUpdatableRepository<TModel, TId>
    where TModel : class, IEntity<TId>, new()
{
    public void Update(TModel model)
    {
        Context.Update(model);
    }

    public void BulkUpdate(IEnumerable<TModel> models)
    {
        Context.UpdateRange(models);
    }
}
