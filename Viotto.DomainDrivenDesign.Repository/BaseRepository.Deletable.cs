using Microsoft.EntityFrameworkCore;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    protected virtual Task<bool> BaseDelete(TModel model)
    {
        return Task.FromResult(true);
    }

    protected virtual async Task<bool> BaseDeleteRange(IEnumerable<TModel> models)
    {
        foreach (var model in models)
        {
            if (!await BaseDelete(model))
                return false;
        }

        return true;
    }


    //! Delete

    public virtual void Delete(TModel model)
    {
        DeleteAsync(model).GetAwaiter().GetResult();
    }

    public virtual async Task DeleteAsync(TModel model)
    {
        if (!await BaseDelete(model))
            return;

        if (!GetAllNoTracking().Any(x => x.Id.Equals(model.Id)))
        {
            throw new InvalidOperationException();
        }

        Context.Remove(model);
        await Context.SaveChangesAsync();
    }

    //! DeleteById

    public virtual void DeleteById(TId id)
    {
        DeleteByIdAsync(id).GetAwaiter().GetResult();
    }

    public virtual async Task DeleteByIdAsync(TId id)
    {
        var model = await GetByIdNoTrackingAsync(id);

        await DeleteAsync(model);
    }

    //! DeleteRange

    public virtual void DeleteRange(IEnumerable<TModel> models)
    {
        DeleteRangeAsync(models).GetAwaiter().GetResult();
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TModel> models)
    {
        if (!await BaseDeleteRange(models))
            return;

        var ids = models.Select(x => x.Id);

        if (GetAllNoTracking().Where(x => ids.Contains(x.Id)).Count() != ids.Count())
        {
            throw new InvalidOperationException();
        }

        Context.RemoveRange(models);
        await Context.SaveChangesAsync();
    }

    //! DeleteRangeById

    public virtual void DeleteRangeById(IEnumerable<TId> ids)
    {
        DeleteRangeByIdAsync(ids).GetAwaiter().GetResult();
    }

    public virtual async Task DeleteRangeByIdAsync(IEnumerable<TId> ids)
    {
        var models = GetRangeByIdNoTracking(ids);

        if (GetAllNoTracking().Where(x => ids.Contains(x.Id)).Count() != ids.Count())
        {
            throw new InvalidOperationException();
        }

        await DeleteRangeAsync(models);
    }
}
