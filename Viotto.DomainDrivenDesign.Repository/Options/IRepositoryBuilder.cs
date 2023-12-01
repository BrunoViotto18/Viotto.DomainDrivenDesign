using System.Linq.Expressions;

namespace Viotto.DomainDrivenDesign.Repository.Options;
public interface IRepositoryBuilder<TModel>
{
    IRepositoryOptions<TModel> Build();

    IRepositoryBuilder<TModel> AddSoftDelete(Expression<Func<TModel, bool>> isDeleted, Action<TModel> deleteAction);
}
