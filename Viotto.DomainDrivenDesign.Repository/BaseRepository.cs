using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract class BaseRepository<TModel, TId> : IRepository<TModel, TId>
    where TModel : class, IEntity<TId>, new()
{
    protected DbContext Context { get; init; }

    public BaseRepository(DbContext context)
    {
        Context = context;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return Context.Database.BeginTransaction();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return Context.Database.BeginTransactionAsync();
    }

    public void SaveChanges()
    {
        Context.BulkSaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return Context.BulkSaveChangesAsync();
    }

    public IQueryable<TModel> GetAll()
    {
        return Context.Set<TModel>();
    }

    public IQueryable<TModel> GetAllNoTracking()
    {
        return GetAll().AsNoTracking();
    }

    public IQueryable<TModel> GetById(TId id)
    {
        return GetAll().Where(x => x.Id.Equals(id));
    }

    public IQueryable<TModel> GetByIdNoTracking(TId id)
    {
        return GetAllNoTracking().Where(x => x.Id.Equals(id));
    }

    public void Insert(TModel model)
    {
        Context.Add(model);
    }

    public void BulkInsert(IEnumerable<TModel> models)
    {
        Context.AddRange(models);
    }

    public void Update(TModel model)
    {
        Context.Update(model);
    }

    public void BulkUpdate(IEnumerable<TModel> models)
    {
        Context.UpdateRange(models);
    }

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
