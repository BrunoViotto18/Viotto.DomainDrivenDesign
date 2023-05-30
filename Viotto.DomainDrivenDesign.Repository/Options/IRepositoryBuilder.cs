using System.Linq.Expressions;
using OneOf;

namespace Viotto.DomainDrivenDesign.Repository.Options;

using Middlewares;


public interface IRepositoryBuilder<TModel>
{
    IRepositoryOptions<TModel> Build();

    IRepositoryBuilder<TModel> AddSoftDelete(Expression<Func<TModel, bool>> isDeleted, Action<TModel> deleteAction);
}
