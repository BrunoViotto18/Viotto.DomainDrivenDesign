using Microsoft.EntityFrameworkCore;

namespace Viotto.DomainDrivenDesign.Repository.IntegrationTests;

internal class TestRepository : BaseRepository<TestModel, Guid>
{
    public TestRepository(DbContext context) : base(context)
    {
    }
}
