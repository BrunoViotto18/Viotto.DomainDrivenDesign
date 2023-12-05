using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Viotto.DomainDrivenDesign.Repository.Decorators;

namespace Viotto.DomainDrivenDesign.Repository.IntegrationTests;

public class SoftDeleteRepository : Repository<SoftDeleteModel, Guid>
{
    private readonly DateTimeOffsetProvider _dateTimeOffsetProvider;

    public SoftDeleteRepository(
        RepositoryBuilder<SoftDeleteModel, Guid> repositoryBuilder,
        DateTimeOffsetProvider dateTimeOffsetProvider)
        : base(repositoryBuilder)
    {
        _dateTimeOffsetProvider = dateTimeOffsetProvider;
    }

    public SoftDeleteRepository(DbContext context,
        DateTimeOffsetProvider dateTimeOffsetProvider)
        : base(context)
    {
        _dateTimeOffsetProvider = dateTimeOffsetProvider;
    }

    protected override void OnRepositoryBuild(RepositoryBuilder<SoftDeleteModel, Guid> builder)
    {
        base.OnRepositoryBuild(builder);
        builder.AddSoftDelete(
            x => x.Deleted,
            x => x.Deleted != null,
            x => x.Deleted = _dateTimeOffsetProvider.Now);
    }
}
