using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;

// TODO (Bruno Viotto):
// ! Change class visibility to internal !
internal sealed class BaseRepository<TModel, TId> : IRepository<TModel, TId>
    where TModel : class, IEntity<TId>, new()
{
    private readonly DbContext _context;

    public BaseRepository(DbContext context)
    {
        _context = context;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public void SaveChanges()
    {
        _context.BulkSaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return _context.BulkSaveChangesAsync();
    }

    public IQueryable<TModel> GetAll()
    {
        return _context.Set<TModel>();
    }

    public IQueryable<TModel> GetAllNoTracking()
    {
        return _context.Set<TModel>()
            .AsNoTracking();
    }

    public IQueryable<TModel> GetById(TId id)
    {
        return _context.Set<TModel>()
            .Where(x => x.Id.Equals(id));
    }

    public IQueryable<TModel> GetByIdNoTracking(TId id)
    {
        return _context.Set<TModel>()
            .AsNoTracking()
            .Where(x => x.Id.Equals(id));
    }

    public void Insert(TModel model)
    {
        _context.Add(model);
    }

    public void BulkInsert(IEnumerable<TModel> models)
    {
        _context.AddRange(models);
    }

    public void Update(TModel model)
    {
        _context.Update(model);
    }

    public void BulkUpdate(IEnumerable<TModel> models)
    {
        _context.UpdateRange(models);
    }

    public void Remove(TModel model)
    {
        _context.Remove(model);
    }

    public void BulkRemove(IEnumerable<TModel> models)
    {
        _context.RemoveRange(models);
    }

    public void RemoveById(TId id)
    {
        var model = new TModel { Id = id };

        _context.Remove(model);
    }

    public void BulkRemoveById(IEnumerable<TId> ids)
    {
        var models = ids.Select(id => new TModel { Id = id });

        _context.RemoveRange(models);
    }
}
