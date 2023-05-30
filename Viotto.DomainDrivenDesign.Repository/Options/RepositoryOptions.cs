using OneOf;

namespace Viotto.DomainDrivenDesign.Repository.Options;

using Middlewares;


internal class RepositoryOptions<TModel> : IRepositoryOptions<TModel>
{
    public IList<OneOf<IMiddleware<Null, OneOf<IQueryable<TModel>, TModel>>, Middleware<Null, OneOf<IQueryable<TModel>, TModel>>>> QueryMiddlewares { get; set; }

    public IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> CreateMiddlewares { get; set; }

    public IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> UpdateMiddlewares { get; set; }

    public IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> DeleteMiddlewares { get; set; }
}
