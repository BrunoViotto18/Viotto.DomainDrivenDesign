using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;

public abstract class Repository<TModel, TId> : IRepository<TModel, TId>
    where TModel : class, IEntity<TId>, new()
{
    private readonly IRepository<TModel, TId> _repository;

    public Repository(RepositoryBuilder<TModel, TId> repositoryBuilder)
    {
        OnRepositoryBuild(repositoryBuilder);
        _repository = repositoryBuilder.Build();
    }

    public Repository(DbContext context)
        : this (new RepositoryBuilder<TModel, TId>(context))
    {
    }

    protected virtual void OnRepositoryBuild(RepositoryBuilder<TModel, TId> builder)
    {
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _repository.BeginTransaction();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _repository.BeginTransactionAsync();
    }

    public void SaveChanges()
    {
        _repository.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return _repository.SaveChangesAsync();
    }

    public IQueryable<TModel> GetAll()
    {
        return _repository.GetAll();
    }

    public IQueryable<TModel> GetAllNoTracking()
    {
        return _repository.GetAllNoTracking();
    }

    public IQueryable<TModel> GetById(TId id)
    {
        return _repository.GetById(id);
    }

    public IQueryable<TModel> GetByIdNoTracking(TId id)
    {
        return _repository.GetByIdNoTracking(id);
    }

    public void Insert(TModel model)
    {
        _repository.Insert(model);
    }

    public void BulkInsert(IEnumerable<TModel> models)
    {
        _repository.BulkInsert(models);
    }

    public void Update(TModel model)
    {
        _repository.Update(model);
    }

    public void BulkUpdate(IEnumerable<TModel> models)
    {
        _repository.BulkUpdate(models);
    }

    public void Remove(TModel model)
    {
        _repository.Remove(model);
    }

    public void BulkRemove(IEnumerable<TModel> models)
    {
        _repository.BulkRemove(models);
    }

    public void RemoveById(TId id)
    {
        _repository.RemoveById(id);
    }

    public void BulkRemoveById(IEnumerable<TId> ids)
    {
        _repository.BulkRemoveById(ids);
    }
}
