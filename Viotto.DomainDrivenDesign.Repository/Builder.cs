using System.Linq.Expressions;
using OneOf;

namespace Viotto.DomainDrivenDesign.Repository;

using Middlewares;


public class Builder<TModel> : IBuilder<TModel>
{
    public IList<OneOf<IMiddleware<IQueryable<TModel>>, Middleware<IQueryable<TModel>>>> QueryMiddlewares { get; }
    public IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> CreateMiddlewares { get; }
    public IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> UpdateMiddlewares { get; }
    public IList<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>> DeleteMiddlewares { get; }


    public Builder()
    {
        QueryMiddlewares = new List<OneOf<IMiddleware<IQueryable<TModel>>, Middleware<IQueryable<TModel>>>>();
        CreateMiddlewares = new List<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>>();
        UpdateMiddlewares = new List<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>>();
        DeleteMiddlewares = new List<OneOf<IMiddleware<TModel, Task>, Middleware<TModel, Task>>>();
    }


    public IBuilder<TModel> AddSoftDelete(Expression<Func<TModel, bool>> isDeleted, Action<TModel> deleteAction)
    {
        var softDelete = new SoftDeleteMiddleware<TModel>(deleteAction);
        var querySoftDelete = new IgnoreSoftDeleteMiddleware<TModel>(isDeleted);

        DeleteMiddlewares.Add(softDelete);
        QueryMiddlewares.Add(querySoftDelete);

        return this;
    }
}
