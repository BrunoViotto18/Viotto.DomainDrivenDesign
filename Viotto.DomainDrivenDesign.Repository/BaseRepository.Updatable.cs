﻿using Microsoft.EntityFrameworkCore;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    protected virtual Task<bool> BaseUpdate(TModel model)
    {
        return Task.FromResult(true);
    }

    protected virtual async Task<bool> BaseUpdateRange(IEnumerable<TModel> models)
    {
        foreach (var model in models)
        {
            if (!await BaseUpdate(model))
                return false;
        }

        return true;
    }


    //! Update

    public virtual void Update(TModel model)
    {
        UpdateAsync(model).GetAwaiter().GetResult();
    }

    public virtual async Task UpdateAsync(TModel model)
    {
        if (!await BaseUpdate(model))
            return;

        Context.Update(model);
        await Context.SaveChangesAsync();
    }

    //! UpdateRange

    public virtual void UpdateRange(IEnumerable<TModel> models)
    {
        UpdateRangeAsync(models).GetAwaiter().GetResult();
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TModel> models)
    {
        if (!await BaseUpdateRange(models))
            return;

        Context.UpdateRange(models);
        await Context.SaveChangesAsync();
    }
}
