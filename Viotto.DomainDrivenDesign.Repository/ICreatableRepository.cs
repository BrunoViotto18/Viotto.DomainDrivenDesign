using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface ICreatableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    void Create(TModel model);
    Task CreateAsync(TModel model);

    void CreateRange(IEnumerable<TModel> models);
    Task CreateRangeAsync(IEnumerable<TModel> models);
}
