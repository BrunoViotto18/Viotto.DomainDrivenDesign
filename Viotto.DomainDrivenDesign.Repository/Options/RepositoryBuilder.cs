using System.Linq.Expressions;
using OneOf;

namespace Viotto.DomainDrivenDesign.Repository.Options;

using Middlewares;


public class RepositoryBuilder<TModel> : IRepositoryBuilder<TModel>
{
    protected IRepositoryOptions<TModel> Options { get; init; }


    public RepositoryBuilder()
    {
        Options = new RepositoryOptions<TModel>();
    }


    public IRepositoryBuilder<TModel> AddMiddleware(IMiddleware<TModel, Task> middleware)
    {

        return this;
    }

    public IRepositoryBuilder<TModel> AddSoftDelete(Expression<Func<TModel, bool>> isDeleted, Action<TModel> deleteAction)
    {
        var softDelete = new SoftDeleteMiddleware<TModel>(deleteAction);
        var querySoftDelete = new IgnoreSoftDeleteMiddleware<TModel>(isDeleted);

        Options.DeleteMiddlewares.Add(softDelete);
        Options.QueryMiddlewares.Add(querySoftDelete);

        return this;
    }

    public IRepositoryOptions<TModel> Build()
    {
        return Options;
    }
}
