using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Model;
using Viotto.DomainDrivenDesign.Repository.Decorators;

namespace Viotto.DomainDrivenDesign.Repository;

public class RepositoryBuilder<TModel, TId>
    where TModel : class, IEntity<TId>, new()
{
    private readonly DbContext _context;
    private IRepository<TModel, TId> _repository;

    public RepositoryBuilder(DbContext context)
    {
        _context = context;
        _repository = new BaseRepository<TModel, TId>(_context);
    }

    public RepositoryBuilder<TModel, TId> AddDecorator<TDecorator>()
        where TDecorator : RepositoryDecorator<TModel, TId>
    {
        if (typeof(TDecorator).IsAbstract)
        {
            throw new ArgumentException(
                $"The class \"{typeof(TDecorator).Name}\" is an abstract class and cannot be used as a generic type to the {nameof(AddDecorator)} method");
        }

        _repository = (IRepository<TModel, TId>)Activator.CreateInstance(typeof(TDecorator), _context, _repository)!;

        return this;
    }

    public RepositoryBuilder<TModel, TId> AddDecorator<TDecorator>(params object[] parameters)
        where TDecorator : RepositoryDecorator<TModel, TId>
    {
        if (typeof(TDecorator).IsAbstract)
        {
            throw new ArgumentException(
                $"The class \"{typeof(TDecorator).Name}\" is an abstract class and cannot be used as a generic type to the {nameof(AddDecorator)} method");
        }

        _repository = (IRepository<TModel, TId>)Activator
            .CreateInstance(typeof(TDecorator), parameters
                .Prepend(_repository)
                .Prepend(_context)
                .ToArray())!;

        return this;
    }

    public IRepository<TModel, TId> Build()
    {
        return _repository;
    }
}
