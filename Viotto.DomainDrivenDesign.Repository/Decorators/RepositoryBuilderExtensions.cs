using System.Linq.Expressions;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.Decorators;

public static class RepositoryBuilderExtensions
{
    public static RepositoryBuilder<TModel, TId> AddSoftDelete<TModel, TId>(
        this RepositoryBuilder<TModel, TId> builder,
        Expression<Func<TModel, object?>> deleteProperty,
        Expression<Func<TModel, bool>> isDeletedPrdicate,
        Action<TModel> deleteAction)
        where TModel : class, IEntity<TId>, new()
    {
        return builder.AddDecorator<SoftDeleteDecorator<TModel, TId>>(deleteProperty, isDeletedPrdicate, deleteAction);
    }
}
