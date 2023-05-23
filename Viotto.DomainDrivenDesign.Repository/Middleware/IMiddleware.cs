namespace Viotto.DomainDrivenDesign.Repository.Middleware;


public interface IMiddleware
{
    Task Invoke(IMiddlewareContext context);
}

public delegate Task Middleware(IMiddlewareContext context);
