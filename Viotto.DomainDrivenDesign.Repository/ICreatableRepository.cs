using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface ICreatableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    void Insert(TModel model);
    void BulkInsert(IEnumerable<TModel> models);
}
