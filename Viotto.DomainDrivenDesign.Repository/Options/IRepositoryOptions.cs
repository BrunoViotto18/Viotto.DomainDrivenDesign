using OneOf;

namespace Viotto.DomainDrivenDesign.Repository.Options;

using Middlewares;


public interface IRepositoryOptions<TModel>
{
    IList<OneOf<IMiddleware<Null, OneOf<IQueryable<TModel>, TModel>>, Middleware<Null, OneOf<IQueryable<TModel>, TModel>>>> QueryMiddlewares { get; }
    IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> CreateMiddlewares { get; }
    IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> UpdateMiddlewares { get; }
    IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> DeleteMiddlewares { get; }
}
