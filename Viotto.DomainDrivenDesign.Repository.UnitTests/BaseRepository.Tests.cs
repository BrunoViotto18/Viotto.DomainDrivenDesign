using Bogus;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests;

using Contexts;
using Fixtures;
using Models;
using Repositories;


[Collection(nameof(TestSetupCollection))]
public partial class BaseRepositoryTests : IAsyncLifetime
{
    private readonly Faker<Test> _testGenerator;
    private readonly TestContext _context;
    private readonly Func<Task> _respawn;
    private readonly TestRepository _sut;


    public BaseRepositoryTests(TestSetup testSetup)
    {
        _testGenerator = testSetup.TestGenerator;
        _context = testSetup.TestContext;
        _respawn = testSetup.RespawnDatabase;

        _sut = new TestRepository(_context);
    }


    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        _context.ChangeTracker.Clear();
        await _respawn();
    }
}
