using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Service;


public interface IUpdatableService<TModel, TId>
    where TModel : IEntity<TId>
{
    void Update(TModel model);
    Task UpdateAsync(TModel model);

    void UpdateRange(IEnumerable<TModel> models);
    Task UpdateRangeAsync(IEnumerable<TModel> models);
}
