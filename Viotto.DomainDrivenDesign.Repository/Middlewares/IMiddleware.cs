namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public interface IMiddleware<TInput, TOutput>
{
    TOutput Invoke(IMiddlewareContext<TInput, TOutput> context);
}

public interface IMiddleware<TOutput>
{
    TOutput Invoke(IMiddlewareContext<TOutput> context);
}

public delegate TOutput Middleware<TInput, TOutput>(IMiddlewareContext<TInput, TOutput> context);
public delegate TOutput Middleware<TOutput>(IMiddlewareContext<TOutput> context);
