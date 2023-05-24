using System.Linq.Expressions;

namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public class IgnoreSoftDeleteMiddleware<TModel> : IMiddleware<IQueryable<TModel>>
{
    public Expression<Func<TModel, bool>> WhereSoftDeleted { get; init; }


    public IgnoreSoftDeleteMiddleware(Expression<Func<TModel, bool>> whereSoftDeleted)
    {
        WhereSoftDeleted = whereSoftDeleted;
    }


    public IQueryable<TModel> Invoke(IMiddlewareContext<IQueryable<TModel>> context)
    {
        var result = context.Next();

        return result.Where(WhereSoftDeleted);
    }
}
