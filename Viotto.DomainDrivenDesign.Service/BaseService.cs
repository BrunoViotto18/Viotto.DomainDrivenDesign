using Microsoft.EntityFrameworkCore.Storage;
using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository;

namespace Viotto.DomainDrivenDesign.Service;


public partial class BaseService<TRepository, TModel, TId> : IService<TModel, TId>
    where TRepository : IRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    public IDbContextTransaction BeginTransaction()
    {
        throw new NotImplementedException();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        throw new NotImplementedException();
    }
}
