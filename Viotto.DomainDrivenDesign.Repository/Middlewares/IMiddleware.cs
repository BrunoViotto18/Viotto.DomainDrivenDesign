namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public interface IMiddleware<TInput, TOutput>
{
    Task Invoke(IMiddlewareIterator<TInput, TOutput> iterator, IMiddlewareContext<TInput, TOutput> context);
}
