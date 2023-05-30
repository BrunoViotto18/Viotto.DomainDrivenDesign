using System.Linq.Expressions;
using OneOf;

namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public class IgnoreSoftDeleteMiddleware<TModel> : IMiddleware<Null, OneOf<IQueryable<TModel>, TModel>>
{
    public Expression<Func<TModel, bool>> WhereSoftDeleted { get; init; }


    public IgnoreSoftDeleteMiddleware(Expression<Func<TModel, bool>> whereSoftDeleted)
    {
        WhereSoftDeleted = whereSoftDeleted;
    }


    public async Task Invoke(
        IMiddlewareIterator<Null, OneOf<IQueryable<TModel>, TModel>> iterator,
        IMiddlewareContext<Null, OneOf<IQueryable<TModel>, TModel>> context
    )
    {
        await iterator.Next(context);

        context.Output = context.Output.Match(
            query => OneOf<IQueryable<TModel>, TModel>.FromT0(query.Where(WhereSoftDeleted)),
            model => model
        );
    }
}
