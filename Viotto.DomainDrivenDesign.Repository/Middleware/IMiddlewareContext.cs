namespace Viotto.DomainDrivenDesign.Repository.Middleware;


public interface IMiddlewareContext
{
    public Task Next();
}
