using Microsoft.EntityFrameworkCore;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TModel, TId> : ICreatableRepository<TModel, TId>
    where TModel : class, IEntity<TId>, new()
{
    public void Insert(TModel model)
    {
        Context.Add(model);
    }

    public void BulkInsert(IEnumerable<TModel> models)
    {
        Context.AddRange(models);
    }
}
