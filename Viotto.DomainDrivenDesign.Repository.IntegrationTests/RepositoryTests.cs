using Bogus;
using Bogus.DataSets;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Viotto.DomainDrivenDesign.Repository.IntegrationTests;

//[Collection(nameof(TestCollection))]
public partial class RepositoryTests : IAsyncLifetime, IClassFixture<TestSetup<MsSqlContainer>>
{
    private const int _seed = TestSetup<MsSqlContainer>._seed;
    private readonly Random _random;
    private readonly DbContext _context;
    private readonly Func<Task> _respawnAsync;
    private readonly Faker<TestModel> _dataGenerator;
    private readonly IRepository<TestModel, Guid> _sut;

    public RepositoryTests(TestSetup<MsSqlContainer> setup)
    {
        _random = new Random(_seed);
        _context = setup.DbContext;
        _respawnAsync = setup.RespawnAsync;
        _dataGenerator = setup.FakeData;
        _sut = new TestRepository(_context);
    }

    [Fact]
    public async Task BeginTransaction_ShouldCreateAValidTransaction()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);

        // Act
        using var transaction = _sut.BeginTransaction();
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        await transaction.RollbackAsync();

        // Assert
        (await _context.Set<TestModel>().AnyAsync()).Should().BeFalse();
    }

    [Fact]
    public async Task BeginTransactionAsync_ShouldCreateAValidTransaction()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);

        // Act
        using var transaction = await _sut.BeginTransactionAsync();
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        await transaction.RollbackAsync();

        // Assert
        (await _context.Set<TestModel>().AnyAsync()).Should().BeFalse();
    }

    [Fact]
    public void SaveChanges_ShouldSaveTheChangesToTheDatabase()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        _context.AddRange(data);

        // Act
        _sut.SaveChanges();

        // Assert
        _context.Set<TestModel>().Should().BeEquivalentTo(data);
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldSaveTheChangesToTheDatabase()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(data);

        // Act
        await _sut.SaveChangesAsync();

        // Assert
        _context.Set<TestModel>().Should().BeEquivalentTo(data);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllTableObjects()
    {
        // Arrange
        var expectedData = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(expectedData);
        await _context.SaveChangesAsync();

        // Act
        var output = _sut.GetAll();

        // Assert
        output.Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public async Task GetAll_ShouldKeepTrackOfReturnedObjects()
    {
        // Arrange
        var expectedData = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(expectedData);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        _sut.GetAll().ToArray();

        // Assert
        _context.ChangeTracker.Entries<TestModel>().Select(x => x.Entity).Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public async Task GetAllNoTracking_ShouldReturnAllTableObjects()
    {
        // Arrange
        var expectedData = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(expectedData);
        await _context.SaveChangesAsync();

        // Act
        var output = _sut.GetAllNoTracking();

        // Assert
        output.Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public async Task GetAllNoTracking_ShouldNotKeepTrackOfReturnedObjects()
    {
        // Arrange
        var expectedData = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(expectedData);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        await _sut.GetAllNoTracking().ToArrayAsync();

        // Assert
        _context.ChangeTracker.Entries<TestModel>().Any().Should().BeFalse();
    }

    [Fact]
    public async Task GetById_ShouldReturnObjectWithSameId()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        var expectedObject = data.Skip(3).First();

        // Act
        var output = await _sut.GetById(expectedObject.Id).SingleAsync();

        // Assert
        output.Should().BeEquivalentTo(expectedObject);
    }

    [Fact]
    public async Task GetById_ShouldKeepTrackOfReturnedObject()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var expectedObject = data.Skip(3).First();

        // Act
        await _sut.GetById(expectedObject.Id).SingleAsync();

        // Assert
        _context.ChangeTracker.Entries<TestModel>().Single().Entity.Should().BeEquivalentTo(expectedObject);
    }

    [Fact]
    public async Task GetByIdNoTracking_ShouldReturnObjectWithSameId()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        var expectedObject = data.Skip(3).First();

        // Act
        var output = await _sut.GetByIdNoTracking(expectedObject.Id).SingleAsync();

        // Assert
        output.Should().BeEquivalentTo(expectedObject);
    }

    [Fact]
    public async Task GetByIdNoTracking_ShouldNotKeepTrackOfReturnedObject()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var expectedObject = data.Skip(3).First();

        // Act
        await _sut.GetByIdNoTracking(expectedObject.Id).SingleAsync();

        // Assert
        _context.ChangeTracker.Entries<TestModel>().Any().Should().BeFalse();
    }

    [Fact]
    public async Task Insert_ShouldInsertObjectInDatabase_WhenSaveChangesIsCalled()
    {
        // Arrange
        var data = _dataGenerator.Generate(1).First();

        // Act
        _sut.Insert(data);
        await _context.SaveChangesAsync();

        // Assert
        (await _context.Set<TestModel>().SingleAsync()).Should().BeEquivalentTo(data);
    }

    [Fact]
    public async Task BulkInsert_ShouldInsertMultipleObjectsInDatabase_WhenSaveChangesIsCalled()
    {
        // Arrange
        var expectedData = _dataGenerator.Generate(10);

        // Act
        _sut.BulkInsert(expectedData);
        await _context.SaveChangesAsync();

        // Assert
        _context.Set<TestModel>().Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public async Task Update_ShouldUpdateTheObjectInDatabase_WhenSaveChangesIsCalled()
    {
        // Arrange
        var model = new TestModel
        {
            Id = Guid(_seed),
            Name = "Alexandre",
            BirthDate = DateTime.Parse("2002-03-05"),
            LuckyNumber = 5
        };
        await _context.AddAsync(model);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        model.Id = Guid(_seed);
        model.Name = "Bruno";
        model.BirthDate = DateTime.Parse("2003-06-17");
        model.LuckyNumber = 27;

        // Act
        _sut.Update(model);
        await _context.SaveChangesAsync();

        // Assert
        (await _context.Set<TestModel>().SingleAsync()).Should().BeEquivalentTo(model);
    }

    [Fact]
    public async Task BulkUpdate_ShouldUpdateMultipleObjectsInDatabase_WhenSaveChangesIsCalled()
    {
        // Arrange
        var models = Enumerable.Range(0, 10)
            .Select((x, i) => new TestModel
            {
                Id = Guid(_seed + i),
                Name = $"Alexandre {i}",
                BirthDate = DateTime.Parse("2002-03-05").AddDays(i),
                LuckyNumber = 5 + i
            });
        await _context.AddRangeAsync(models);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        models = models.Select((x, i) => new TestModel
        {
            Id = Guid(_seed + i),
            Name = $"Bruno {i}",
            BirthDate = DateTime.Parse("2003-06-17").AddDays(i),
            LuckyNumber = 27 + i
        });

        // Act
        _sut.BulkUpdate(models);
        await _context.SaveChangesAsync();

        // Assert
        _context.Set<TestModel>().Should().BeEquivalentTo(models);
    }

    [Fact]
    public async Task Remove_ShouldRemoveObjectInDatabase_WhenSaveChangesIsCalled()
    {
        // Arrange
        var model = _dataGenerator.Generate(1).First();
        await _context.AddAsync(model);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        _sut.Remove(model);
        await _context.SaveChangesAsync();

        // Assert
        (await _context.Set<TestModel>().AnyAsync()).Should().BeFalse();
    }

    [Fact]
    public async Task BulkRemove_ShouldRemoveMultipleObjectsInDatabase_WhenSaveChangesIsCalled()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        _sut.BulkRemove(data);
        await _context.SaveChangesAsync();

        // Assert
        (await _context.Set<TestModel>().AnyAsync()).Should().BeFalse();
    }

    [Fact]
    public async Task RemoveById_ShouldRemoveObjectByIdInDatabase_WhenSaveChangesIsCalled()
    {
        // Arrange
        var model = _dataGenerator.Generate(1).First();
        await _context.AddAsync(model);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        _sut.RemoveById(model.Id);
        await _context.SaveChangesAsync();

        // Assert
        (await _context.Set<TestModel>().AnyAsync()).Should().BeFalse();
    }

    [Fact]
    public async Task BulkRemoveById_ShouldRemoveMultipleObjectsByIdInDatabase_WhenSaveChangesIsCalled()
    {
        // Arrange
        var data = _dataGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        _sut.BulkRemoveById(data.Select(x => x.Id));
        await _context.SaveChangesAsync();

        // Assert
        (await _context.Set<TestModel>().AnyAsync()).Should().BeFalse();
    }

    private static Guid Guid(int seed)
    {
        var random = new Random(_seed * seed + seed);
        var bytes = new byte[16];
        random.NextBytes(bytes);

        return new Guid(bytes);
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
