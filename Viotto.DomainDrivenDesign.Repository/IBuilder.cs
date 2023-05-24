using System.Linq.Expressions;
using OneOf;

namespace Viotto.DomainDrivenDesign.Repository;

using Middlewares;


public interface IBuilder<TModel>
{
    public IList<OneOf<IMiddleware<IQueryable<TModel>>, Middleware<IQueryable<TModel>>>> QueryMiddlewares { get; }
    public IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> CreateMiddlewares { get; }
    public IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> UpdateMiddlewares { get; }
    public IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> DeleteMiddlewares { get; }

    IBuilder<TModel> AddSoftDelete(Expression<Func<TModel, bool>> isDeleted, Action<TModel> deleteAction);
}
