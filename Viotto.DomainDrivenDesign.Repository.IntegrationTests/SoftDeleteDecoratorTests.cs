using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Viotto.DomainDrivenDesign.Repository.IntegrationTests;

public class SoftDeleteDecoratorTests : IAsyncLifetime, IClassFixture<SoftDeleteSetup<MsSqlContainer>>, IClassFixture<DateTimeOffsetProvider>
{
    private const int _seed = SoftDeleteSetup<MsSqlContainer>._seed;
    private readonly Random _random;
    private readonly DbContext _context;
    private readonly Func<Task> _respawnAsync;
    private readonly Faker<SoftDeleteModel> _dataGenerator;
    private readonly IRepository<SoftDeleteModel, Guid> _sut;
    private readonly DateTimeOffsetProvider _dateTimeOffsetProvider;

    public SoftDeleteDecoratorTests(SoftDeleteSetup<MsSqlContainer> setup, DateTimeOffsetProvider dateTimeOffsetProvider)
    {
        _dateTimeOffsetProvider = dateTimeOffsetProvider;
        _random = new Random(_seed);
        _context = setup.DbContext;
        _dataGenerator = setup.FakeData;
        _respawnAsync = setup.RespawnAsync;
        _sut = new SoftDeleteRepository(_context, _dateTimeOffsetProvider);
    }

    [Fact]
    public async Task GetAll_ShouldReturnNotDeletedObjects()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        data = data
            .Select((x, i) =>
            {
                if (i / 5 > 0)
                {
                    x.Deleted = DateTime.Parse("2023-12-01");
                }
                return x;
            })
            .ToList();
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();

        // Act
        var output = _sut.GetAll();

        // Assert
        output.Should().BeEquivalentTo(data.Where(x => x.Deleted is null));
    }

    [Fact]
    public async Task GetAllNoTracking_ShouldReturnNotDeletedObjects()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        data = data
            .Select((x, i) =>
            {
                if (i / 5 > 0)
                {
                    x.Deleted = DateTime.Parse("2023-12-01");
                }
                return x;
            })
            .ToList();
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();

        // Act
        var output = _sut.GetAllNoTracking();

        // Assert
        output.Should().BeEquivalentTo(data.Where(x => x.Deleted is null));
    }

    [Fact]
    public async Task GetById_ShouldReturnNotDeletedObjectById()
    {
        // Arrange
        var data = _dataGenerator.Generate();
        data.Deleted = DateTime.Parse("2023-12-02");
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();

        // Act
        var output = _sut.GetById(data.Id);

        // Assert
        output.Any().Should().BeFalse();
    }

    [Fact]
    public async Task GetByIdNoTracking_ShouldReturnNotDeletedObjectById()
    {
        // Arrange
        var data = _dataGenerator.Generate();
        data.Deleted = DateTime.Parse("2023-12-02");
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();

        // Act
        var output = _sut.GetByIdNoTracking(data.Id);

        // Assert
        output.Any().Should().BeFalse();
    }

    [Fact]
    public async Task Remove_ShouldUpdateObjectAsRemoved_WhenSaveChangesIsCalled()
    {
        // Arrange
        var data = _dataGenerator.Generate();
        await _context.AddAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        _sut.Remove(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Assert
        (await _context.Set<SoftDeleteModel>().SingleAsync()).Deleted.Should().NotBe(null);
    }

    [Fact]
    public async Task BulkRemove_ShouldUpdateObjecstAsRemoved_WhenSaveChangesIsCalled()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var expectedOutput = data.Select(x =>
        {
            x.Deleted = _dateTimeOffsetProvider.Now;
            return x;
        });

        // Act
        _sut.BulkRemove(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Assert
        _context.Set<SoftDeleteModel>().Should().BeEquivalentTo(expectedOutput);
    }

    [Fact]
    public async Task RemoveById_ShouldUpdateObjectAsRemovedById_WhenSaveChangesIsCalled()
    {
        // Arrange
        var data = _dataGenerator.Generate();
        await _context.AddAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        _sut.RemoveById(data.Id);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Assert
        (await _context.Set<SoftDeleteModel>().SingleAsync()).Deleted.Should().NotBe(null);
    }

    [Fact]
    public async Task BulkRemoveById_ShouldUpdateObjecstAsRemovedById_WhenSaveChangesIsCalled()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var expectedOutput = data.Select(x =>
        {
            x.Deleted = _dateTimeOffsetProvider.Now;
            return x;
        });

        // Act
        _sut.BulkRemoveById(data.Select(x => x.Id));
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Assert
        _context.Set<SoftDeleteModel>().Should().BeEquivalentTo(expectedOutput);
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        _context.ChangeTracker.Clear();
        await _respawnAsync();
    }
}
