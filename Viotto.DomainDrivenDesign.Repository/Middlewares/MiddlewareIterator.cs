namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public class MiddlewareIterator<TInput, TOutput> : IMiddlewareIterator<TInput, TOutput>
{
    private readonly IMiddleware<TInput, TOutput>[] _middlewares;
    private readonly Func<TInput, Task<TOutput>> _function;
    private int CurrentIndex = -1;

    private IMiddleware<TInput, TOutput> Current => _middlewares[CurrentIndex];


    public MiddlewareIterator(
        IEnumerable<IMiddleware<TInput, TOutput>> middlewares,
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
            await Current.Invoke(this, context);
        }
        else
        {
            context.Output = await _function(context.Input);
        }
    }
}
