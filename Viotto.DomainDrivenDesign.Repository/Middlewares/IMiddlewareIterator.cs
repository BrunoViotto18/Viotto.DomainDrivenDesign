namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public interface IMiddlewareIterator<TInput, TOutput>
{
    Task Next(IMiddlewareContext<TInput, TOutput> context);
}
