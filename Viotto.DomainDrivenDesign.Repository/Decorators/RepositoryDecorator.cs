using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.Decorators;

public abstract class RepositoryDecorator<TModel, TId> : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    protected DbContext Context { get; init; }
    protected IRepository<TModel, TId> Repository { get; init; }

    public RepositoryDecorator(DbContext context, IRepository<TModel, TId> repository)
    {
        Context = context;
        Repository = repository;
    }

    public virtual IDbContextTransaction BeginTransaction()
    {
        return Repository.BeginTransaction();
    }

    public virtual Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return Repository.BeginTransactionAsync();
    }

    public virtual void SaveChanges()
    {
        Repository.SaveChanges();
    }

    public virtual Task SaveChangesAsync()
    {
        return Repository.SaveChangesAsync();
    }

    public virtual IQueryable<TModel> GetAll()
    {
        return Repository.GetAll();
    }

    public virtual IQueryable<TModel> GetAllNoTracking()
    {
        return Repository.GetAllNoTracking();
    }

    public virtual IQueryable<TModel> GetById(TId id)
    {
        return Repository.GetById(id);
    }

    public virtual IQueryable<TModel> GetByIdNoTracking(TId id)
    {
        return Repository.GetByIdNoTracking(id);
    }

    public virtual void Insert(TModel model)
    {
        Repository.Insert(model);
    }

    public virtual void BulkInsert(IEnumerable<TModel> models)
    {
        Repository.BulkInsert(models);
    }

    public virtual void Update(TModel model)
    {
        Repository.Update(model);
    }

    public virtual void BulkUpdate(IEnumerable<TModel> models)
    {
        Repository.BulkUpdate(models);
    }

    public virtual void Remove(TModel model)
    {
        Repository.Remove(model);
    }

    public virtual void BulkRemove(IEnumerable<TModel> models)
    {
        Repository.BulkRemove(models);
    }

    public virtual void RemoveById(TId id)
    {
        Repository.RemoveById(id);
    }
    public virtual void BulkRemoveById(IEnumerable<TId> ids)
    {
        Repository.BulkRemoveById(ids);
    }
}
