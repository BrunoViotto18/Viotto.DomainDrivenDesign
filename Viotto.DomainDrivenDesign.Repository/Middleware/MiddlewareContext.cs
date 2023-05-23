using OneOf;

namespace Viotto.DomainDrivenDesign.Repository.Middleware;


public class MiddlewareContext<TInput> : IMiddlewareContext
{
    private readonly OneOf<IMiddleware, Middleware>[] _middlewares;
    private readonly Func<TInput, Task> _function;
    private readonly TInput _input;

    private int CurrentIndex = -1;
    private OneOf<IMiddleware, Middleware> Current => _middlewares[CurrentIndex];

    public MiddlewareContext(IEnumerable<OneOf<IMiddleware, Middleware>> middlewares, Func<TInput, Task> function, TInput input)
    {
        _middlewares = middlewares.ToArray();
        _function = function;
        _input = input;
    }

    private bool MoveNext()
        => ++CurrentIndex < _middlewares.Length;

    public async Task Next()
    {
        if (MoveNext())
            await Current.Match(
                _interface => _interface.Invoke(this),
                _delegate => _delegate(this)
            );
        else
            await _function(_input);
    }
}
