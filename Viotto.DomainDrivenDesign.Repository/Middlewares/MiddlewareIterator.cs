using OneOf;

namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public class MiddlewareIterator<TInput, TOutput> : IMiddlewareIterator<TInput, TOutput>
{
    private readonly OneOf<IMiddleware<TInput, TOutput>, Middleware<TInput, TOutput>>[] _middlewares;
    private readonly Func<TInput, Task<TOutput>> _function;
    private int CurrentIndex = -1;

    private OneOf<IMiddleware<TInput, TOutput>, Middleware<TInput, TOutput>> Current => _middlewares[CurrentIndex];


    public MiddlewareIterator(
        IEnumerable<OneOf<IMiddleware<TInput, TOutput>, Middleware<TInput, TOutput>>> middlewares,
        Func<TInput, Task<TOutput>> function)
    {
        _middlewares = middlewares.ToArray();
        _function = function;
    }


    private bool MoveNext()
        => ++CurrentIndex < _middlewares.Length;

    public async Task Next(IMiddlewareContext<TInput, TOutput> context)
    {
        if (MoveNext())
        {
            await Current.Match(
                _interface => _interface.Invoke(this, context),
                _delegate => _delegate(this, context)
            );
        }
        else
        {
            context.Output = await _function(context.Input);
        }
    }
}
