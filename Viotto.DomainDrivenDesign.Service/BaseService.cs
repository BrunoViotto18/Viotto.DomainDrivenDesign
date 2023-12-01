using Microsoft.EntityFrameworkCore.Storage;

using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    protected TRepository Repository { get; init; }


    public BaseService(TRepository repository)
    {
        Repository = repository;
    }


    public IDbContextTransaction BeginTransaction()
    {
        return Repository.BeginTransaction();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return Repository.BeginTransactionAsync();
    }
}
