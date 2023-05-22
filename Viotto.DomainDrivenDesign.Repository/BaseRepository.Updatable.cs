using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    protected virtual bool BaseUpdate(TModel model)
    {
        return true;
    }

    protected virtual bool BaseUpdateRange(IEnumerable<TModel> models)
    {
        foreach (var model in models)
        {
            if (!BaseUpdate(model))
                return false;
        }

        return true;
    }


    public virtual void Update(TModel model)
    {
        UpdateAsync(model).GetAwaiter().GetResult();
    }

    public virtual async Task UpdateAsync(TModel model)
    {
        if (!BaseUpdate(model))
            return;

        Context.Update(model);
        await Context.SaveChangesAsync();
    }

    public virtual void UpdateRange(IEnumerable<TModel> models)
    {
        UpdateRangeAsync(models).GetAwaiter().GetResult();
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TModel> models)
    {
        if (!BaseUpdateRange(models))
            return;

        Context.UpdateRange(models);
        await Context.SaveChangesAsync();
    }
}
