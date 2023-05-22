using Microsoft.EntityFrameworkCore;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    protected virtual bool BaseCreate(TModel model)
    {
        return true;
    }

    protected virtual bool BaseCreateRange(IEnumerable<TModel> models)
    {
        foreach (var model in models)
        {
            if (!BaseCreate(model))
                return false;
        }

        return true;
    }


    public virtual void Create(TModel model)
    {
        CreateAsync(model).GetAwaiter().GetResult();
    }

    public virtual async Task CreateAsync(TModel model)
    {
        if (!BaseCreate(model))
            return;

        await Context.AddAsync(model);
        await Context.SaveChangesAsync();
    }

    public virtual void CreateRange(IEnumerable<TModel> models)
    {
        CreateRangeAsync(models).GetAwaiter().GetResult();
    }

    public virtual async Task CreateRangeAsync(IEnumerable<TModel> models)
    {
        if (!BaseCreateRange(models))
            return;

        await Context.AddRangeAsync(models);
        await Context.SaveChangesAsync();
    }
}
