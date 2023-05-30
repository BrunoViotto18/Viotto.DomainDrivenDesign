using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository.Options;

namespace Viotto.DomainDrivenDesign.Repository;


public abstract partial class BaseRepository<TContext, TModel, TId> : IRepository<TModel, TId>
    where TContext : DbContext
    where TModel : class, IEntity<TId>
{
    protected abstract DbSet<TModel> Table { get; }
    protected TContext Context { get; init; }


    public BaseRepository(TContext context) : this(context, new RepositoryBuilder<TModel>())
    {
    }

    public BaseRepository(TContext context, IRepositoryBuilder<TModel> builder)
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

    public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await Context.Database.BeginTransactionAsync();
    }
}
