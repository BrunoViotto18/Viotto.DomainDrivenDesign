namespace Viotto.DomainDrivenDesign.Repository.Middlewares;


public class SoftDeleteMiddleware<TInput> : IMiddleware<TInput, Task>
{
    private Action<TInput> SoftDelete { get; init; }


    public SoftDeleteMiddleware(Action<TInput> softDelete)
    {
        SoftDelete = softDelete;
    }


    public async Task Invoke(IMiddlewareContext<TInput, Task> context)
    {
        SoftDelete(context.Input);

        await context.Next();
    }
}
