using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Service;


public interface ICreatableService<TModel, TId>
    where TModel : IEntity<TId>
{
    void Create(TModel model);
    Task CreateAsync(TModel model);

    void CreateRange(IEnumerable<TModel> models);
    Task CreateRangeAsync(IEnumerable<TModel> models);
}
