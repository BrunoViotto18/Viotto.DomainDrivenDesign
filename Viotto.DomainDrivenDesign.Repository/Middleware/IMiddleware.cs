namespace Viotto.DomainDrivenDesign.Repository.Middleware;


public interface IMiddleware
{
    public Task Invoke(IMiddlewareContext context);
}
