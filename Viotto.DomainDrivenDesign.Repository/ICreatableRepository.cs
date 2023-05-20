using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface ICreatableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    TModel Create(TModel model);
    Task<TModel> CreateAsync(TModel model);

    IEnumerable<TModel> CreateRange(IEnumerable<TModel> models);
    Task<IEnumerable<TModel>> CreateRangeAsync(IEnumerable<TModel> models);
}
