using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface IUpdatableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    void Update(TModel model);
    Task UpdateAsync(TModel model);

    void UpdateRange(IEnumerable<TModel> models);
    Task UpdateRangeAsync(IEnumerable<TModel> models);
}
