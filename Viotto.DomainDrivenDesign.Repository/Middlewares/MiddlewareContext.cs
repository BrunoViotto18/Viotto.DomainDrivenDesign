using OneOf;

namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public class MiddlewareContext<TInput, TOutput> : IMiddlewareContext<TInput, TOutput>
{
    private readonly OneOf<IMiddleware<TInput, TOutput>, Middleware<TInput, TOutput>>[] _middlewares;

    private int CurrentIndex = -1;
    private OneOf<IMiddleware<TInput, TOutput>, Middleware<TInput, TOutput>> Current => _middlewares[CurrentIndex];

    public TInput Input { get; }
    public Func<TInput, TOutput> Function { get; }


    public MiddlewareContext(
        IEnumerable<OneOf<IMiddleware<TInput, TOutput>, Middleware<TInput, TOutput>>> middlewares,
        Func<TInput, TOutput> function,
        TInput input)
    {
        _middlewares = middlewares.ToArray();
        Function = function;
        Input = input;
    }


    private bool MoveNext()
        => ++CurrentIndex < _middlewares.Length;

    public TOutput Next()
    {
        if (MoveNext())
            return Current.Match(
                _interface => _interface.Invoke(this),
                _delegate => _delegate(this)
            );
        else
            return Function(Input);
    }
}
