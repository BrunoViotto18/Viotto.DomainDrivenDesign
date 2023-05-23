namespace Viotto.DomainDrivenDesign.Repository.Middleware;


public class MiddlewareContext<TInput> : IMiddlewareContext
{
    private readonly IMiddleware[] _middlewares;
    private readonly Func<TInput, Task> _function;
    private readonly TInput _input;

    private int CurrentIndex = -1;
    private IMiddleware Current => _middlewares[CurrentIndex];

    public MiddlewareContext(IEnumerable<IMiddleware> middlewares, Func<TInput, Task> function, TInput input)
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
            await Current.Invoke(this);
        else
            await _function(_input);
    }
}
