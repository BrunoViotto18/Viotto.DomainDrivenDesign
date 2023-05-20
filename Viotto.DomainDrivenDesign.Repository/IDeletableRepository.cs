using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository;


public interface IDeletableRepository<TModel, TId>
    where TModel : IEntity<TId>
{
    void Delete(TModel model);
    Task DeleteAsync(TModel model);

    void DeleteById(TId id);
    Task DeleteByIdAsync(TId id);

    void DeleteRange(IEnumerable<TModel> models);
    Task DeleteRangeAsync(IEnumerable<TModel> models);

    void DeleteRangeById(IEnumerable<TId> ids);
    Task DeleteRangeByIdAsync(IEnumerable<TId> ids);
}
