using Bogus;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests;

using Contexts;
using Fixtures;
using FluentAssertions;
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


    [Fact]
    public void BeginTransaction_ShouldReturnAValidRepositoryTransaction_Always()
    {
        // Arrange
        var data1 = _testGenerator.Generate(10);
        var data2 = _testGenerator.Generate(20).Skip(10);

        // Act
        var transaction1 = _sut.BeginTransaction();
        _context.AddRange(data1);
        _context.SaveChanges();
        transaction1.Commit();

        var transaction2 = _sut.BeginTransaction();
        _context.AddRange(data2);
        _context.SaveChanges();
        transaction2.Rollback();

        // Assert
        _context.Test.Should().BeEquivalentTo(data1);
    }

    [Fact]
    public async Task BeginTransactionAsync_ShouldReturnAValidRepositoryTransaction_Always()
    {
        // Arrange
        var data1 = _testGenerator.Generate(10);
        var data2 = _testGenerator.Generate(20).Skip(10);

        // Act
        var transaction1 = await _sut.BeginTransactionAsync();
        _context.AddRange(data1);
        await _context.SaveChangesAsync();
        await transaction1.CommitAsync();

        var transaction2 = await _sut.BeginTransactionAsync();
        _context.AddRange(data2);
        await _context.SaveChangesAsync();
        await transaction2.RollbackAsync();

        // Assert
        _context.Test.Should().BeEquivalentTo(data1);
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
