namespace Viotto.DomainDrivenDesign.Repository.UnitTests.Repositories;

using Contexts;
using Microsoft.EntityFrameworkCore;
using Viotto.DomainDrivenDesign.Repository.UnitTests.Models;

public class TestRepository : BaseRepository<TestContext, Test, long>
{
    protected override DbSet<Test> Table => Context.Test;

    public TestRepository(TestContext context) : base(context)
    {
    }
}
