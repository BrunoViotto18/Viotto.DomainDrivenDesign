using System.Data;

using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface IRepository<TModel, TId>
    : IQueryableRepository<TModel, TId>,
        ICreatableRepository<TModel, TId>,
        IUpdatableRepository<TModel, TId>,
        IDeletableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    IDbTransaction BeginTransaction();
    Task<IDbTransaction> BeginTransactionAsync();
}
