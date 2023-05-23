using Microsoft.EntityFrameworkCore;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    protected virtual Task<bool> BaseCreate(TModel model)
    {
        return Task.FromResult(true);
    }

    protected virtual async Task<bool> BaseCreateRange(IEnumerable<TModel> models)
    {
        foreach (var model in models)
        {
            if (!await BaseCreate(model))
                return false;
        }

        return true;
    }


    //! Create

    public virtual void Create(TModel model)
    {
        CreateAsync(model).GetAwaiter().GetResult();
    }

    public virtual async Task CreateAsync(TModel model)
    {
        if (!await BaseCreate(model))
            return;

        await Context.AddAsync(model);
        await Context.SaveChangesAsync();
    }

    //! CreateRange

    public virtual void CreateRange(IEnumerable<TModel> models)
    {
        CreateRangeAsync(models).GetAwaiter().GetResult();
    }

    public virtual async Task CreateRangeAsync(IEnumerable<TModel> models)
    {
        if (!await BaseCreateRange(models))
            return;

        await Context.AddRangeAsync(models);
        await Context.SaveChangesAsync();
    }
}
