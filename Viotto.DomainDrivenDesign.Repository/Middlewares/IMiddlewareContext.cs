namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public interface IMiddlewareContext<TInput, TOutput>
{
    TInput Input { get; set; }
    TOutput Output { get; set; }
    Func<TInput, TOutput> Function { get; }
}
