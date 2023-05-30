namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public class MiddlewareContext<TInput, TOutput> : IMiddlewareContext<TInput, TOutput>
{
    public TInput Input { get; set; }
    public TOutput Output { get; set; }

    public Func<TInput, TOutput> Function { get; }
}
