using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository.Options;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TModel, TId> : IRepository<TModel, TId>
    where TModel : class, IEntity<TId>, new()
{
    protected DbContext Context { get; init; }


    public BaseRepository(DbContext context) : this(context, new RepositoryBuilder<TModel>())
    {
    }

    public BaseRepository(DbContext context, IRepositoryBuilder<TModel> builder)
    {
        Context = context;
        OnInit(builder);
    }


    protected virtual void OnInit(IRepositoryBuilder<TModel> builder)
    {
    }


    public virtual IDbContextTransaction BeginTransaction()
    {
        return Context.Database.BeginTransaction();
    }

    public virtual Task<IDbContextTransaction> BeginTransactionAsync()
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
}
