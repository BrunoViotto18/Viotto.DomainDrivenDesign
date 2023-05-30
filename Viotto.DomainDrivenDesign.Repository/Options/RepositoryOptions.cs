using OneOf;

namespace Viotto.DomainDrivenDesign.Repository.Options;

using Middlewares;


internal class RepositoryOptions<TModel> : IRepositoryOptions<TModel>
{
    public IList<IMiddleware<Null, OneOf<IQueryable<TModel>, TModel>>> QueryMiddlewares { get; set; }

    public IList<IMiddleware<TModel, Task>> CreateMiddlewares { get; set; }

    public IList<IMiddleware<TModel, Task>> UpdateMiddlewares { get; set; }

    public IList<IMiddleware<TModel, Task>> DeleteMiddlewares { get; set; }
}
