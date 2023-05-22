using Microsoft.EntityFrameworkCore.Storage;

using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public abstract partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public TRepository Repository { get; init; }


    public BaseService(TRepository repository)
    {
        Repository = repository;
    }


    public IDbContextTransaction BeginTransaction()
        => Repository.BeginTransaction();

    public async Task<IDbContextTransaction> BeginTransactionAsync()
        => await Repository.BeginTransactionAsync();
}
