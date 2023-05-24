namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public interface IMiddlewareContext<TOutput>
{
    Func<TOutput> Function { get; }

    TOutput Next();
}

public interface IMiddlewareContext<TInput, TOutput>
{
    TInput Input { get; }
    Func<TInput, TOutput> Function { get; }

    TOutput Next();
}
