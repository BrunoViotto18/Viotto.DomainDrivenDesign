using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface IDeletableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    void Remove(TModel model);
    void BulkRemove(IEnumerable<TModel> models);

    void RemoveById(TId id);
    void BulkRemoveById(IEnumerable<TId> ids);
}
