using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Viotto.DomainDrivenDesign.Model;

namespace Viotto.DomainDrivenDesign.Repository.Decorators;

public class SoftDeleteDecorator<TModel, TId> : RepositoryDecorator<TModel, TId>
    where TModel : class, IEntity<TId>, new()
{
    private readonly Expression<Func<TModel, object?>> _deleteProperty;
    private readonly Expression<Func<TModel, bool>> _isNotDeleted;
    private readonly Action<TModel> _delete;

    public SoftDeleteDecorator(
        DbContext context,
        IRepository<TModel, TId> repository,
        Expression<Func<TModel, object?>> deletePropeprty,
        Expression<Func<TModel, bool>> isDeletedPredicate,
        Action<TModel> deleteAction)
        : base(context, repository)
    {
        _deleteProperty = deletePropeprty;
        var isNotDeletedExpression = Expression.Not(isDeletedPredicate.Body).Reduce();
        _isNotDeleted = Expression.Lambda<Func<TModel, bool>>(isNotDeletedExpression, isDeletedPredicate.Parameters.First());
        _delete = deleteAction;
    }

    public override IQueryable<TModel> GetAll()
    {
        return base.GetAll().Where(_isNotDeleted);
    }

    public override IQueryable<TModel> GetAllNoTracking()
    {
        return base.GetAllNoTracking().Where(_isNotDeleted);
    }

    public override IQueryable<TModel> GetById(TId id)
    {
        return base.GetById(id).Where(_isNotDeleted);
    }

    public override IQueryable<TModel> GetByIdNoTracking(TId id)
    {
        return base.GetByIdNoTracking(id).Where(_isNotDeleted);
    }

    public override void Remove(TModel model)
    {
        Context.Attach(model);
        _delete(model);
        Context.Entry(model).Property(_deleteProperty).IsModified = true;
    }

    public override void BulkRemove(IEnumerable<TModel> models)
    {
        Context.AttachRange(models);
        foreach (var model in models)
        {
            _delete(model);
            Context.Entry(model).Property(_deleteProperty).IsModified = true;
        }
    }

    public override void RemoveById(TId id)
    {
        var model = new TModel()
        {
            Id = id
        };
        Context.Attach(model);
        _delete(model);
        Context.Entry(model).Property(_deleteProperty).IsModified = true;
    }

    public override void BulkRemoveById(IEnumerable<TId> ids)
    {
        var models = ids
            .Select(id => new TModel
            {
                Id = id
            })
            .ToArray();
        Context.AttachRange(models);
        foreach (var model in models)
        {
            _delete(model);
            Context.Entry(model).Property(_deleteProperty).IsModified = true;
        }
    }
}
