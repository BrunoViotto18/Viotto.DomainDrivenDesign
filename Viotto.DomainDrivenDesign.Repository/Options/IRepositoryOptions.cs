using OneOf;

namespace Viotto.DomainDrivenDesign.Repository.Options;

using Middlewares;


public interface IRepositoryOptions<TModel>
{
    IList<IMiddleware<Null, OneOf<IQueryable<TModel>, TModel>>> QueryMiddlewares { get; }
    IList<IMiddleware<TModel, Task>> CreateMiddlewares { get; }
    IList<IMiddleware<TModel, Task>> UpdateMiddlewares { get; }
    IList<IMiddleware<TModel, Task>> DeleteMiddlewares { get; }
}
