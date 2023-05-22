using Microsoft.EntityFrameworkCore.Storage;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Service;


public interface IService<TModel, TId>
    : IQueryableService<TModel, TId>,
        ICreatableService<TModel, TId>,
        IUpdatableService<TModel, TId>,
        IDeletableService<TModel, TId>
    where TModel : IEntity<TId>
{
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync();
}
