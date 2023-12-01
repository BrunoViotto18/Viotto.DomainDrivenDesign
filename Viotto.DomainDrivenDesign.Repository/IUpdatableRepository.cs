using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface IUpdatableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    void Update(TModel model);
    void BulkUpdate(IEnumerable<TModel> models);
}
