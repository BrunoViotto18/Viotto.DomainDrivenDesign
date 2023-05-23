namespace Viotto.DomainDrivenDesign.Repository.Middleware;


public interface IMiddlewareContext
{
    Task Next();
}
