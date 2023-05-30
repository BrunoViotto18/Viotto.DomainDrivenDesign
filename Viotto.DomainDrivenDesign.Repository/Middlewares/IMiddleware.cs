namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public interface IMiddleware<TInput, TOutput>
{
    Task Invoke(IMiddlewareIterator<TInput, TOutput> iterator, IMiddlewareContext<TInput, TOutput> context);
}

public delegate Task Middleware<TInput, TOutput>(
    IMiddlewareIterator<TInput, TOutput> iterator,
    IMiddlewareContext<TInput, TOutput> context
);
